using System.Text;
using CarRentalSystem.Models;

namespace CarRentalSystem.Business
{
    /// <summary>
    /// Generates receipts and rental tickets/agreements.
    /// </summary>
    public class DocumentGenerator
    {
        /// <summary>
        /// Generates a rental ticket/agreement for checkout.
        /// </summary>
        public string GenerateRentalTicket(Rental rental, Reservation reservation, Customer customer, Vehicle vehicle)
        {
            var sb = new StringBuilder();
            sb.AppendLine("========================================");
            sb.AppendLine("         RENTAL TICKET/AGREEMENT        ");
            sb.AppendLine("========================================");
            sb.AppendLine();
            sb.AppendLine($"Rental ID: {rental.RentalId}");
            sb.AppendLine($"Reservation ID: {reservation.ReservationId}");
            sb.AppendLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine();
            sb.AppendLine("CUSTOMER INFORMATION");
            sb.AppendLine($"Name: {customer.Name}");
            sb.AppendLine($"License Number: {customer.LicenseNumber}");
            sb.AppendLine($"Contact: {customer.ContactInfo}");
            sb.AppendLine();
            sb.AppendLine("VEHICLE INFORMATION");
            sb.AppendLine($"Vehicle: {vehicle.Make} {vehicle.Model}");
            sb.AppendLine($"Registration: {vehicle.RegistrationPlate}");
            sb.AppendLine($"Type: {vehicle.Type}");
            sb.AppendLine($"Location: {vehicle.Location}");
            sb.AppendLine($"Daily Rate: ${vehicle.DailyRate:F2}");
            sb.AppendLine();
            sb.AppendLine("RENTAL PERIOD");
            sb.AppendLine($"Start Date: {reservation.StartDate:yyyy-MM-dd}");
            sb.AppendLine($"End Date: {reservation.EndDate:yyyy-MM-dd}");
            sb.AppendLine($"Check-Out Time: {rental.CheckOutTime:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine();
            sb.AppendLine("FINANCIAL INFORMATION");
            sb.AppendLine($"Deposit Paid: ${rental.DepositPaid:F2}");
            sb.AppendLine();
            sb.AppendLine("========================================");
            sb.AppendLine("Customer Signature: _________________");
            sb.AppendLine();
            sb.AppendLine("Please return the vehicle by the end date");
            sb.AppendLine("to avoid late penalties (50% per day).");
            sb.AppendLine("========================================");

            return sb.ToString();
        }

        /// <summary>
        /// Generates a payment receipt.
        /// </summary>
        public string GeneratePaymentReceipt(Payment payment, Rental rental, Reservation reservation, Customer customer, Vehicle vehicle)
        {
            var sb = new StringBuilder();
            sb.AppendLine("========================================");
            sb.AppendLine("          PAYMENT RECEIPT               ");
            sb.AppendLine("========================================");
            sb.AppendLine();
            sb.AppendLine($"Receipt ID: {payment.PaymentId}");
            sb.AppendLine($"Transaction Date: {payment.TransactionDate:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine();
            sb.AppendLine("CUSTOMER INFORMATION");
            sb.AppendLine($"Name: {customer.Name}");
            sb.AppendLine($"License Number: {customer.LicenseNumber}");
            sb.AppendLine();
            sb.AppendLine("RENTAL INFORMATION");
            sb.AppendLine($"Rental ID: {rental.RentalId}");
            sb.AppendLine($"Vehicle: {vehicle.Make} {vehicle.Model} ({vehicle.RegistrationPlate})");
            sb.AppendLine($"Check-Out: {rental.CheckOutTime:yyyy-MM-dd HH:mm:ss}");
            if (rental.CheckInTime.HasValue)
            {
                sb.AppendLine($"Check-In: {rental.CheckInTime.Value:yyyy-MM-dd HH:mm:ss}");
            }
            sb.AppendLine();
            sb.AppendLine("FINANCIAL SUMMARY");
            sb.AppendLine($"Rental Fee: ${rental.TotalFee:F2}");
            sb.AppendLine($"Late Penalties: ${rental.Penalties:F2}");
            sb.AppendLine($"Deposit Applied: -${rental.DepositPaid:F2}");
            sb.AppendLine($"Total Due: ${(rental.TotalFee + rental.Penalties - rental.DepositPaid):F2}");
            sb.AppendLine();
            sb.AppendLine("PAYMENT DETAILS");
            sb.AppendLine($"Amount Paid: ${payment.Amount:F2}");
            sb.AppendLine($"Payment Method: {payment.PaymentType}");
            if (!string.IsNullOrEmpty(payment.Notes))
            {
                sb.AppendLine($"Notes: {payment.Notes}");
            }
            sb.AppendLine();
            sb.AppendLine("========================================");
            sb.AppendLine("     Thank you for your business!      ");
            sb.AppendLine("========================================");

            return sb.ToString();
        }
    }
}
