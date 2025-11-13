namespace CarRentalSystem.Models
{
    /// <summary>
    /// Represents a payment transaction.
    /// </summary>
    public class Payment
    {
        public int PaymentId { get; set; }
        public int RentalId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; } = string.Empty; // Cash, Card, E-Wallet
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string? Notes { get; set; }

        // Navigation properties
        public Rental? Rental { get; set; }
    }
}
