using System.Data.SQLite;
using CarRentalSystem.Models;

namespace CarRentalSystem.Data.Repositories
{
    /// <summary>
    /// Repository for managing Customer data access operations.
    /// </summary>
    public class CustomerRepository
    {
        private readonly DatabaseManager _dbManager;

        public CustomerRepository()
        {
            _dbManager = DatabaseManager.Instance;
        }

        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        public List<Customer> GetAll()
        {
            var customers = new List<Customer>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Customers ORDER BY Name";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customers.Add(MapToCustomer(reader));
            }

            return customers;
        }

        /// <summary>
        /// Retrieves a customer by ID.
        /// </summary>
        public Customer? GetById(int customerId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Customers WHERE CustomerId = @CustomerId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerId", customerId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToCustomer(reader);
            }

            return null;
        }

        /// <summary>
        /// Searches customers by name or license number.
        /// </summary>
        public List<Customer> Search(string searchTerm)
        {
            var customers = new List<Customer>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                SELECT * FROM Customers
                WHERE Name LIKE @SearchTerm OR LicenseNumber LIKE @SearchTerm
                ORDER BY Name";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(MapToCustomer(reader));
            }

            return customers;
        }

        /// <summary>
        /// Inserts a new customer into the database.
        /// </summary>
        public int Insert(Customer customer)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                INSERT INTO Customers (Name, ContactInfo, LicenseNumber, Email, DateCreated)
                VALUES (@Name, @ContactInfo, @LicenseNumber, @Email, @DateCreated);
                SELECT last_insert_rowid();";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@ContactInfo", customer.ContactInfo);
            command.Parameters.AddWithValue("@LicenseNumber", customer.LicenseNumber);
            command.Parameters.AddWithValue("@Email", customer.Email ?? string.Empty);
            command.Parameters.AddWithValue("@DateCreated", customer.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));

            return Convert.ToInt32(command.ExecuteScalar());
        }

        /// <summary>
        /// Updates an existing customer in the database.
        /// </summary>
        public void Update(Customer customer)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                UPDATE Customers
                SET Name = @Name, ContactInfo = @ContactInfo, LicenseNumber = @LicenseNumber, Email = @Email
                WHERE CustomerId = @CustomerId";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@ContactInfo", customer.ContactInfo);
            command.Parameters.AddWithValue("@LicenseNumber", customer.LicenseNumber);
            command.Parameters.AddWithValue("@Email", customer.Email ?? string.Empty);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes a customer from the database.
        /// </summary>
        public void Delete(int customerId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerId", customerId);

            command.ExecuteNonQuery();
        }

        private Customer MapToCustomer(SQLiteDataReader reader)
        {
            return new Customer
            {
                CustomerId = reader.GetInt32(0),
                Name = reader.GetString(1),
                ContactInfo = reader.GetString(2),
                LicenseNumber = reader.GetString(3),
                Email = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                DateCreated = DateTime.Parse(reader.GetString(5))
            };
        }
    }
}
