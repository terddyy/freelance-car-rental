namespace CarRentalSystem.Models
{
    /// <summary>
    /// Represents a vehicle reservation.
    /// </summary>
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled, Completed
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Navigation properties (loaded separately)
        public Customer? Customer { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
