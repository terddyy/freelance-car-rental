namespace CarRentalSystem.Models
{
    /// <summary>
    /// Represents a vehicle in the rental fleet.
    /// </summary>
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public string GasType { get; set; } = string.Empty;
        public int SeatCapacity { get; set; }
        public string Location { get; set; } = string.Empty;
        public string RegistrationPlate { get; set; } = string.Empty;
        public decimal DailyRate { get; set; }
        public string Status { get; set; } = "Available"; // Available, Rented, Maintenance
        public string? ImagePath { get; set; }

        public string DisplayName => $"{Make} {Model} ({RegistrationPlate})";
    }
}
