namespace CarRentalSystem.Models
{
    /// <summary>
    /// Represents an active rental (checked-out vehicle).
    /// </summary>
    public class Rental
    {
        public int RentalId { get; set; }
        public int ReservationId { get; set; }
        public DateTime CheckOutTime { get; set; }
        public DateTime? CheckInTime { get; set; }
        public decimal TotalFee { get; set; }
        public decimal DepositPaid { get; set; }
        public decimal Penalties { get; set; }
        public bool IsPaid { get; set; }

        // Navigation properties
        public Reservation? Reservation { get; set; }
    }
}
