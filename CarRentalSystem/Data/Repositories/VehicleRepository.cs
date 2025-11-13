using System.Data.SQLite;
using CarRentalSystem.Models;

namespace CarRentalSystem.Data.Repositories
{
    /// <summary>
    /// Repository for managing Vehicle data access operations.
    /// </summary>
    public class VehicleRepository
    {
        private readonly DatabaseManager _dbManager;

        public VehicleRepository()
        {
            _dbManager = DatabaseManager.Instance;
        }

        /// <summary>
        /// Retrieves all vehicles from the database.
        /// </summary>
        public List<Vehicle> GetAll()
        {
            var vehicles = new List<Vehicle>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Vehicles ORDER BY Make, Model";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                vehicles.Add(MapToVehicle(reader));
            }

            return vehicles;
        }

        /// <summary>
        /// Retrieves a vehicle by ID.
        /// </summary>
        public Vehicle? GetById(int vehicleId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Vehicles WHERE VehicleId = @VehicleId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@VehicleId", vehicleId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToVehicle(reader);
            }

            return null;
        }

        /// <summary>
        /// Searches for available vehicles based on criteria.
        /// </summary>
        public List<Vehicle> SearchAvailable(DateTime startDate, DateTime endDate, string? type = null, string? location = null)
        {
            var vehicles = new List<Vehicle>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                SELECT v.* FROM Vehicles v
                WHERE v.Status = 'Available'
                AND v.VehicleId NOT IN (
                    SELECT r.VehicleId FROM Reservations r
                    WHERE r.Status IN ('Confirmed', 'Pending')
                    AND (
                        (r.StartDate <= @EndDate AND r.EndDate >= @StartDate)
                    )
                )";

            if (!string.IsNullOrEmpty(type))
            {
                query += " AND v.Type = @Type";
            }

            if (!string.IsNullOrEmpty(location))
            {
                query += " AND v.Location = @Location";
            }

            query += " ORDER BY v.Make, v.Model";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

            if (!string.IsNullOrEmpty(type))
            {
                command.Parameters.AddWithValue("@Type", type);
            }

            if (!string.IsNullOrEmpty(location))
            {
                command.Parameters.AddWithValue("@Location", location);
            }

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                vehicles.Add(MapToVehicle(reader));
            }

            return vehicles;
        }

        /// <summary>
        /// Inserts a new vehicle into the database.
        /// </summary>
        public int Insert(Vehicle vehicle)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                INSERT INTO Vehicles (Make, Model, Type, Color, Transmission, GasType, SeatCapacity, Location, RegistrationPlate, DailyRate, Status, ImagePath)
                VALUES (@Make, @Model, @Type, @Color, @Transmission, @GasType, @SeatCapacity, @Location, @RegistrationPlate, @DailyRate, @Status, @ImagePath);
                SELECT last_insert_rowid();";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Make", vehicle.Make);
            command.Parameters.AddWithValue("@Model", vehicle.Model);
            command.Parameters.AddWithValue("@Type", vehicle.Type);
            command.Parameters.AddWithValue("@Color", vehicle.Color ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Transmission", vehicle.Transmission ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@GasType", vehicle.GasType ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@SeatCapacity", vehicle.SeatCapacity);
            command.Parameters.AddWithValue("@Location", vehicle.Location);
            command.Parameters.AddWithValue("@RegistrationPlate", vehicle.RegistrationPlate);
            command.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
            command.Parameters.AddWithValue("@Status", vehicle.Status);
            command.Parameters.AddWithValue("@ImagePath", vehicle.ImagePath ?? (object)DBNull.Value);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        /// <summary>
        /// Updates an existing vehicle in the database.
        /// </summary>
        public void Update(Vehicle vehicle)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                UPDATE Vehicles
                SET Make = @Make, Model = @Model, Type = @Type, Color = @Color, Transmission = @Transmission,
                    GasType = @GasType, SeatCapacity = @SeatCapacity, Location = @Location,
                    RegistrationPlate = @RegistrationPlate, DailyRate = @DailyRate, Status = @Status, ImagePath = @ImagePath
                WHERE VehicleId = @VehicleId";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
            command.Parameters.AddWithValue("@Make", vehicle.Make);
            command.Parameters.AddWithValue("@Model", vehicle.Model);
            command.Parameters.AddWithValue("@Type", vehicle.Type);
            command.Parameters.AddWithValue("@Color", vehicle.Color ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Transmission", vehicle.Transmission ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@GasType", vehicle.GasType ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@SeatCapacity", vehicle.SeatCapacity);
            command.Parameters.AddWithValue("@Location", vehicle.Location);
            command.Parameters.AddWithValue("@RegistrationPlate", vehicle.RegistrationPlate);
            command.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
            command.Parameters.AddWithValue("@Status", vehicle.Status);
            command.Parameters.AddWithValue("@ImagePath", vehicle.ImagePath ?? (object)DBNull.Value);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes a vehicle from the database.
        /// </summary>
        public void Delete(int vehicleId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "DELETE FROM Vehicles WHERE VehicleId = @VehicleId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@VehicleId", vehicleId);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets distinct vehicle types for filtering.
        /// </summary>
        public List<string> GetDistinctTypes()
        {
            var types = new List<string>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT DISTINCT Type FROM Vehicles ORDER BY Type";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                types.Add(reader.GetString(0));
            }

            return types;
        }

        /// <summary>
        /// Gets distinct locations for filtering.
        /// </summary>
        public List<string> GetDistinctLocations()
        {
            var locations = new List<string>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT DISTINCT Location FROM Vehicles ORDER BY Location";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                locations.Add(reader.GetString(0));
            }

            return locations;
        }

        private Vehicle MapToVehicle(SQLiteDataReader reader)
        {
            return new Vehicle
            {
                VehicleId = reader.GetInt32(reader.GetOrdinal("VehicleId")),
                Make = reader.GetString(reader.GetOrdinal("Make")),
                Model = reader.GetString(reader.GetOrdinal("Model")),
                Type = reader.GetString(reader.GetOrdinal("Type")),
                Color = reader.IsDBNull(reader.GetOrdinal("Color")) ? string.Empty : reader.GetString(reader.GetOrdinal("Color")),
                Transmission = reader.IsDBNull(reader.GetOrdinal("Transmission")) ? string.Empty : reader.GetString(reader.GetOrdinal("Transmission")),
                GasType = reader.IsDBNull(reader.GetOrdinal("GasType")) ? string.Empty : reader.GetString(reader.GetOrdinal("GasType")),
                SeatCapacity = reader.IsDBNull(reader.GetOrdinal("SeatCapacity")) ? 0 : reader.GetInt32(reader.GetOrdinal("SeatCapacity")),
                Location = reader.GetString(reader.GetOrdinal("Location")),
                RegistrationPlate = reader.GetString(reader.GetOrdinal("RegistrationPlate")),
                DailyRate = reader.GetDecimal(reader.GetOrdinal("DailyRate")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"))
            };
        }
    }
}
