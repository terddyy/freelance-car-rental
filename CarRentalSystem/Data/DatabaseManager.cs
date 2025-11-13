using System.Data.SQLite;

namespace CarRentalSystem.Data
{
    /// <summary>
    /// Manages the SQLite database connection and initialization.
    /// </summary>
    public class DatabaseManager
    {
        private readonly string _connectionString;
        private static DatabaseManager? _instance;
        private static readonly object _lock = new object();

        private DatabaseManager(string dbPath)
        {
            _connectionString = $"Data Source={dbPath};Version=3;";
        }

        /// <summary>
        /// Gets the singleton instance of the DatabaseManager.
        /// </summary>
        public static DatabaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            string appDataPath = Path.Combine(
                                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                "CarRentalSystem");
                            
                            if (!Directory.Exists(appDataPath))
                            {
                                Directory.CreateDirectory(appDataPath);
                            }

                            string dbPath = Path.Combine(appDataPath, "CarRental.db");
                            _instance = new DatabaseManager(dbPath);
                            _instance.InitializeDatabase();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        /// <summary>
        /// Initializes the database schema if it doesn't exist.
        /// </summary>
        private void InitializeDatabase()
        {
            using var connection = GetConnection();
            connection.Open();

            string createTablesScript = @"
                CREATE TABLE IF NOT EXISTS Vehicles (
                    VehicleId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Make TEXT NOT NULL,
                    Model TEXT NOT NULL,
                    Type TEXT NOT NULL,
                    Color TEXT,
                    Transmission TEXT,
                    GasType TEXT,
                    SeatCapacity INTEGER,
                    Location TEXT NOT NULL,
                    RegistrationPlate TEXT NOT NULL UNIQUE,
                    DailyRate REAL NOT NULL,
                    Status TEXT NOT NULL DEFAULT 'Available',
                    ImagePath TEXT
                );

                CREATE TABLE IF NOT EXISTS Customers (
                    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    ContactInfo TEXT NOT NULL,
                    LicenseNumber TEXT NOT NULL UNIQUE,
                    Email TEXT,
                    DateCreated TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Reservations (
                    ReservationId INTEGER PRIMARY KEY AUTOINCREMENT,
                    CustomerId INTEGER NOT NULL,
                    VehicleId INTEGER NOT NULL,
                    StartDate TEXT NOT NULL,
                    EndDate TEXT NOT NULL,
                    Status TEXT NOT NULL DEFAULT 'Pending',
                    DateCreated TEXT NOT NULL,
                    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
                    FOREIGN KEY (VehicleId) REFERENCES Vehicles(VehicleId)
                );

                CREATE TABLE IF NOT EXISTS Rentals (
                    RentalId INTEGER PRIMARY KEY AUTOINCREMENT,
                    ReservationId INTEGER NOT NULL,
                    CheckOutTime TEXT NOT NULL,
                    CheckInTime TEXT,
                    TotalFee REAL NOT NULL DEFAULT 0,
                    DepositPaid REAL NOT NULL DEFAULT 0,
                    Penalties REAL NOT NULL DEFAULT 0,
                    IsPaid INTEGER NOT NULL DEFAULT 0,
                    FOREIGN KEY (ReservationId) REFERENCES Reservations(ReservationId)
                );

                CREATE TABLE IF NOT EXISTS Payments (
                    PaymentId INTEGER PRIMARY KEY AUTOINCREMENT,
                    RentalId INTEGER NOT NULL,
                    Amount REAL NOT NULL,
                    PaymentType TEXT NOT NULL,
                    TransactionDate TEXT NOT NULL,
                    Notes TEXT,
                    FOREIGN KEY (RentalId) REFERENCES Rentals(RentalId)
                );

                CREATE INDEX IF NOT EXISTS idx_reservations_dates ON Reservations(StartDate, EndDate);
                CREATE INDEX IF NOT EXISTS idx_reservations_vehicle ON Reservations(VehicleId);
                CREATE INDEX IF NOT EXISTS idx_rentals_reservation ON Rentals(ReservationId);
            ";

            using var command = new SQLiteCommand(createTablesScript, connection);
            command.ExecuteNonQuery();
            
            // Run migrations to add any missing columns
            RunMigrations(connection);
            
            // Seed sample data if database is empty
            SeedSampleData(connection);
        }

        /// <summary>
        /// Runs database migrations to add missing columns to existing tables.
        /// </summary>
        private void RunMigrations(SQLiteConnection connection)
        {
            // Add missing columns to Vehicles table if they don't exist
            AddColumnIfNotExists(connection, "Vehicles", "Color", "TEXT");
            AddColumnIfNotExists(connection, "Vehicles", "Transmission", "TEXT");
            AddColumnIfNotExists(connection, "Vehicles", "GasType", "TEXT");
            AddColumnIfNotExists(connection, "Vehicles", "SeatCapacity", "INTEGER");
            AddColumnIfNotExists(connection, "Vehicles", "ImagePath", "TEXT");
        }

        /// <summary>
        /// Adds a column to a table if it doesn't already exist.
        /// </summary>
        private void AddColumnIfNotExists(SQLiteConnection connection, string tableName, string columnName, string columnType)
        {
            try
            {
                // Check if column exists
                string checkQuery = $"PRAGMA table_info({tableName})";
                using var checkCmd = new SQLiteCommand(checkQuery, connection);
                using var reader = checkCmd.ExecuteReader();
                
                bool columnExists = false;
                while (reader.Read())
                {
                    string existingColumn = reader.GetString(1); // Column name is at index 1
                    if (existingColumn.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        columnExists = true;
                        break;
                    }
                }
                reader.Close();
                
                // Add column if it doesn't exist
                if (!columnExists)
                {
                    string alterQuery = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType}";
                    using var alterCmd = new SQLiteCommand(alterQuery, connection);
                    alterCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log or handle migration errors
                Console.WriteLine($"Migration error for {tableName}.{columnName}: {ex.Message}");
            }
        }

        private void SeedSampleData(SQLiteConnection connection)
        {
            // Check if vehicles already exist
            using var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM Vehicles", connection);
            long count = (long)checkCmd.ExecuteScalar();
            
            if (count > 0) return; // Already has data
            
            // Get the car images path
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
            string imagesPath = Path.Combine(solutionPath, "car-rental-images", "cars");
            
            string insertVehicles = @"
                INSERT INTO Vehicles (Make, Model, Type, Color, Transmission, GasType, SeatCapacity, Location, RegistrationPlate, DailyRate, Status, ImagePath)
                VALUES 
                    ('Toyota', 'Fortuner', 'SUV', 'Grey', 'Automatic', 'Diesel', 7, 'Manila', 'ABC-1234', 1500, 'Available', @img1),
                    ('Nissan', 'Terra', 'SUV', 'Brown', 'Manual', 'Gas', 7, 'Manila', 'XYZ-5678', 2000, 'Available', @img2),
                    ('Mitsubishi', 'Montero Sport', 'SUV', 'White', 'Automatic', 'Gas', 7, 'Quezon City', 'DEF-9012', 2500, 'Available', @img3),
                    ('Ford', 'Everest Sport', 'SUV', 'Blue', 'Automatic', 'Gas', 7, 'Makati', 'GHI-3456', 3500, 'Available', null);
            ";
            
            using var insertCmd = new SQLiteCommand(insertVehicles, connection);
            insertCmd.Parameters.AddWithValue("@img1", Path.Combine(imagesPath, "fortuner.png"));
            insertCmd.Parameters.AddWithValue("@img2", Path.Combine(imagesPath, "nissan-tera.png"));
            insertCmd.Parameters.AddWithValue("@img3", Path.Combine(imagesPath, "jaero-sport.png"));
            insertCmd.ExecuteNonQuery();
            
            // Add a sample customer
            string insertCustomer = @"
                INSERT INTO Customers (Name, ContactInfo, LicenseNumber, Email, DateCreated)
                VALUES ('John Doe', '09171234567', 'N01-12-345678', 'john.doe@email.com', @date);
            ";
            
            using var customerCmd = new SQLiteCommand(insertCustomer, connection);
            customerCmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            customerCmd.ExecuteNonQuery();
        }
    }
}
