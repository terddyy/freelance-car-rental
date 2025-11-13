using System.Data.SQLite;
using CarRentalSystem.Models;

namespace CarRentalSystem.Data.Repositories
{
    /// <summary>
    /// Repository for managing Payment data access operations.
    /// </summary>
    public class PaymentRepository
    {
        private readonly DatabaseManager _dbManager;

        public PaymentRepository()
        {
            _dbManager = DatabaseManager.Instance;
        }

        /// <summary>
        /// Retrieves all payments from the database.
        /// </summary>
        public List<Payment> GetAll()
        {
            var payments = new List<Payment>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Payments ORDER BY TransactionDate DESC";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                payments.Add(MapToPayment(reader));
            }

            return payments;
        }

        /// <summary>
        /// Retrieves a payment by ID.
        /// </summary>
        public Payment? GetById(int paymentId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Payments WHERE PaymentId = @PaymentId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@PaymentId", paymentId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToPayment(reader);
            }

            return null;
        }

        /// <summary>
        /// Gets all payments for a specific rental.
        /// </summary>
        public List<Payment> GetByRentalId(int rentalId)
        {
            var payments = new List<Payment>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Payments WHERE RentalId = @RentalId ORDER BY TransactionDate";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@RentalId", rentalId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                payments.Add(MapToPayment(reader));
            }

            return payments;
        }

        /// <summary>
        /// Inserts a new payment into the database.
        /// </summary>
        public int Insert(Payment payment)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                INSERT INTO Payments (RentalId, Amount, PaymentType, TransactionDate, Notes)
                VALUES (@RentalId, @Amount, @PaymentType, @TransactionDate, @Notes);
                SELECT last_insert_rowid();";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@RentalId", payment.RentalId);
            command.Parameters.AddWithValue("@Amount", payment.Amount);
            command.Parameters.AddWithValue("@PaymentType", payment.PaymentType);
            command.Parameters.AddWithValue("@TransactionDate", payment.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@Notes", payment.Notes ?? string.Empty);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        /// <summary>
        /// Updates an existing payment in the database.
        /// </summary>
        public void Update(Payment payment)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                UPDATE Payments
                SET RentalId = @RentalId, Amount = @Amount, PaymentType = @PaymentType,
                    TransactionDate = @TransactionDate, Notes = @Notes
                WHERE PaymentId = @PaymentId";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@PaymentId", payment.PaymentId);
            command.Parameters.AddWithValue("@RentalId", payment.RentalId);
            command.Parameters.AddWithValue("@Amount", payment.Amount);
            command.Parameters.AddWithValue("@PaymentType", payment.PaymentType);
            command.Parameters.AddWithValue("@TransactionDate", payment.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@Notes", payment.Notes ?? string.Empty);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes a payment from the database.
        /// </summary>
        public void Delete(int paymentId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "DELETE FROM Payments WHERE PaymentId = @PaymentId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@PaymentId", paymentId);

            command.ExecuteNonQuery();
        }

        private Payment MapToPayment(SQLiteDataReader reader)
        {
            return new Payment
            {
                PaymentId = reader.GetInt32(0),
                RentalId = reader.GetInt32(1),
                Amount = reader.GetDecimal(2),
                PaymentType = reader.GetString(3),
                TransactionDate = DateTime.Parse(reader.GetString(4)),
                Notes = reader.IsDBNull(5) ? null : reader.GetString(5)
            };
        }
    }
}
