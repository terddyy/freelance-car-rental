using System.Data.SQLite;
using CarRentalSystem.Models;

namespace CarRentalSystem.Data.Repositories
{
    /// <summary>
    /// Repository for managing Rental data access operations.
    /// </summary>
    public class RentalRepository
    {
        private readonly DatabaseManager _dbManager;

        public RentalRepository()
        {
            _dbManager = DatabaseManager.Instance;
        }

        /// <summary>
        /// Retrieves all rentals from the database.
        /// </summary>
        public List<Rental> GetAll()
        {
            var rentals = new List<Rental>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Rentals ORDER BY CheckOutTime DESC";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                rentals.Add(MapToRental(reader));
            }

            return rentals;
        }

        /// <summary>
        /// Retrieves a rental by ID.
        /// </summary>
        public Rental? GetById(int rentalId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Rentals WHERE RentalId = @RentalId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@RentalId", rentalId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToRental(reader);
            }

            return null;
        }

        /// <summary>
        /// Gets rental by reservation ID.
        /// </summary>
        public Rental? GetByReservationId(int reservationId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Rentals WHERE ReservationId = @ReservationId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ReservationId", reservationId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToRental(reader);
            }

            return null;
        }

        /// <summary>
        /// Gets active rentals (not checked in yet).
        /// </summary>
        public List<Rental> GetActive()
        {
            var rentals = new List<Rental>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Rentals WHERE CheckInTime IS NULL ORDER BY CheckOutTime DESC";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                rentals.Add(MapToRental(reader));
            }

            return rentals;
        }

        /// <summary>
        /// Gets unpaid rentals.
        /// </summary>
        public List<Rental> GetUnpaid()
        {
            var rentals = new List<Rental>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Rentals WHERE IsPaid = 0 ORDER BY CheckOutTime DESC";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                rentals.Add(MapToRental(reader));
            }

            return rentals;
        }

        /// <summary>
        /// Inserts a new rental into the database.
        /// </summary>
        public int Insert(Rental rental)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                INSERT INTO Rentals (ReservationId, CheckOutTime, CheckInTime, TotalFee, DepositPaid, Penalties, IsPaid)
                VALUES (@ReservationId, @CheckOutTime, @CheckInTime, @TotalFee, @DepositPaid, @Penalties, @IsPaid);
                SELECT last_insert_rowid();";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ReservationId", rental.ReservationId);
            command.Parameters.AddWithValue("@CheckOutTime", rental.CheckOutTime.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@CheckInTime", rental.CheckInTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@TotalFee", rental.TotalFee);
            command.Parameters.AddWithValue("@DepositPaid", rental.DepositPaid);
            command.Parameters.AddWithValue("@Penalties", rental.Penalties);
            command.Parameters.AddWithValue("@IsPaid", rental.IsPaid ? 1 : 0);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        /// <summary>
        /// Updates an existing rental in the database.
        /// </summary>
        public void Update(Rental rental)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                UPDATE Rentals
                SET ReservationId = @ReservationId, CheckOutTime = @CheckOutTime, CheckInTime = @CheckInTime,
                    TotalFee = @TotalFee, DepositPaid = @DepositPaid, Penalties = @Penalties, IsPaid = @IsPaid
                WHERE RentalId = @RentalId";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@RentalId", rental.RentalId);
            command.Parameters.AddWithValue("@ReservationId", rental.ReservationId);
            command.Parameters.AddWithValue("@CheckOutTime", rental.CheckOutTime.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@CheckInTime", rental.CheckInTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@TotalFee", rental.TotalFee);
            command.Parameters.AddWithValue("@DepositPaid", rental.DepositPaid);
            command.Parameters.AddWithValue("@Penalties", rental.Penalties);
            command.Parameters.AddWithValue("@IsPaid", rental.IsPaid ? 1 : 0);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes a rental from the database.
        /// </summary>
        public void Delete(int rentalId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "DELETE FROM Rentals WHERE RentalId = @RentalId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@RentalId", rentalId);

            command.ExecuteNonQuery();
        }

        private Rental MapToRental(SQLiteDataReader reader)
        {
            return new Rental
            {
                RentalId = reader.GetInt32(0),
                ReservationId = reader.GetInt32(1),
                CheckOutTime = DateTime.Parse(reader.GetString(2)),
                CheckInTime = reader.IsDBNull(3) ? null : DateTime.Parse(reader.GetString(3)),
                TotalFee = reader.GetDecimal(4),
                DepositPaid = reader.GetDecimal(5),
                Penalties = reader.GetDecimal(6),
                IsPaid = reader.GetInt32(7) == 1
            };
        }
    }
}
