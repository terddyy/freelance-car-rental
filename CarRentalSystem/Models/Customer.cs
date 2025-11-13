namespace CarRentalSystem.Models
{
    /// <summary>
    /// Represents a customer in the rental system.
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string DisplayName => $"{Name} ({LicenseNumber})";
    }
}
