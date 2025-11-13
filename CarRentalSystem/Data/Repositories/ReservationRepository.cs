using System.Data.SQLite;
using CarRentalSystem.Models;

namespace CarRentalSystem.Data.Repositories
{
    /// <summary>
    /// Repository for managing Reservation data access operations.
    /// </summary>
    public class ReservationRepository
    {
        private readonly DatabaseManager _dbManager;

        public ReservationRepository()
        {
            _dbManager = DatabaseManager.Instance;
        }

        /// <summary>
        /// Retrieves all reservations from the database.
        /// </summary>
        public List<Reservation> GetAll()
        {
            var reservations = new List<Reservation>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Reservations ORDER BY StartDate DESC";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                reservations.Add(MapToReservation(reader));
            }

            return reservations;
        }

        /// <summary>
        /// Retrieves a reservation by ID.
        /// </summary>
        public Reservation? GetById(int reservationId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Reservations WHERE ReservationId = @ReservationId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ReservationId", reservationId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToReservation(reader);
            }

            return null;
        }

        /// <summary>
        /// Gets reservations by customer ID.
        /// </summary>
        public List<Reservation> GetByCustomerId(int customerId)
        {
            var reservations = new List<Reservation>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Reservations WHERE CustomerId = @CustomerId ORDER BY StartDate DESC";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerId", customerId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                reservations.Add(MapToReservation(reader));
            }

            return reservations;
        }

        /// <summary>
        /// Gets active reservations (Pending or Confirmed).
        /// </summary>
        public List<Reservation> GetActive()
        {
            var reservations = new List<Reservation>();

            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                SELECT * FROM Reservations
                WHERE Status IN ('Pending', 'Confirmed')
                ORDER BY StartDate";

            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                reservations.Add(MapToReservation(reader));
            }

            return reservations;
        }

        /// <summary>
        /// Inserts a new reservation into the database.
        /// </summary>
        public int Insert(Reservation reservation)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                INSERT INTO Reservations (CustomerId, VehicleId, StartDate, EndDate, Status, DateCreated)
                VALUES (@CustomerId, @VehicleId, @StartDate, @EndDate, @Status, @DateCreated);
                SELECT last_insert_rowid();";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerId", reservation.CustomerId);
            command.Parameters.AddWithValue("@VehicleId", reservation.VehicleId);
            command.Parameters.AddWithValue("@StartDate", reservation.StartDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@EndDate", reservation.EndDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Status", reservation.Status);
            command.Parameters.AddWithValue("@DateCreated", reservation.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));

            return Convert.ToInt32(command.ExecuteScalar());
        }

        /// <summary>
        /// Updates an existing reservation in the database.
        /// </summary>
        public void Update(Reservation reservation)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = @"
                UPDATE Reservations
                SET CustomerId = @CustomerId, VehicleId = @VehicleId, StartDate = @StartDate,
                    EndDate = @EndDate, Status = @Status
                WHERE ReservationId = @ReservationId";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ReservationId", reservation.ReservationId);
            command.Parameters.AddWithValue("@CustomerId", reservation.CustomerId);
            command.Parameters.AddWithValue("@VehicleId", reservation.VehicleId);
            command.Parameters.AddWithValue("@StartDate", reservation.StartDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@EndDate", reservation.EndDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Status", reservation.Status);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes a reservation from the database.
        /// </summary>
        public void Delete(int reservationId)
        {
            using var connection = _dbManager.GetConnection();
            connection.Open();

            string query = "DELETE FROM Reservations WHERE ReservationId = @ReservationId";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ReservationId", reservationId);

            command.ExecuteNonQuery();
        }

        private Reservation MapToReservation(SQLiteDataReader reader)
        {
            return new Reservation
            {
                ReservationId = reader.GetInt32(0),
                CustomerId = reader.GetInt32(1),
                VehicleId = reader.GetInt32(2),
                StartDate = DateTime.Parse(reader.GetString(3)),
                EndDate = DateTime.Parse(reader.GetString(4)),
                Status = reader.GetString(5),
                DateCreated = DateTime.Parse(reader.GetString(6))
            };
        }
    }
}
