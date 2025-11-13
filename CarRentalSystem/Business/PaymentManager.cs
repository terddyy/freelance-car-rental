using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Business
{
    /// <summary>
    /// Business logic for processing payments.
    /// </summary>
    public class PaymentManager
    {
        private readonly PaymentRepository _paymentRepo;
        private readonly RentalRepository _rentalRepo;

        public PaymentManager()
        {
            _paymentRepo = new PaymentRepository();
            _rentalRepo = new RentalRepository();
        }

        /// <summary>
        /// Processes a payment for a rental.
        /// </summary>
        public Payment ProcessPayment(int rentalId, decimal amount, string paymentType, string? notes = null)
        {
            var rental = _rentalRepo.GetById(rentalId);
            if (rental == null)
            {
                throw new InvalidOperationException("Rental not found.");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero.");
            }

            var payment = new Payment
            {
                RentalId = rentalId,
                Amount = amount,
                PaymentType = paymentType,
                TransactionDate = DateTime.Now,
                Notes = notes
            };

            payment.PaymentId = _paymentRepo.Insert(payment);

            // Check if rental is fully paid
            var totalPaid = GetTotalPaidForRental(rentalId);
            var totalDue = rental.TotalFee + rental.Penalties - rental.DepositPaid;

            if (totalPaid >= totalDue)
            {
                rental.IsPaid = true;
                _rentalRepo.Update(rental);
            }

            return payment;
        }

        /// <summary>
        /// Gets the total amount paid for a rental.
        /// </summary>
        public decimal GetTotalPaidForRental(int rentalId)
        {
            var payments = _paymentRepo.GetByRentalId(rentalId);
            return payments.Sum(p => p.Amount);
        }

        /// <summary>
        /// Gets the outstanding balance for a rental.
        /// </summary>
        public decimal GetOutstandingBalance(int rentalId)
        {
            var rental = _rentalRepo.GetById(rentalId);
            if (rental == null) return 0;

            var totalDue = rental.TotalFee + rental.Penalties - rental.DepositPaid;
            var totalPaid = GetTotalPaidForRental(rentalId);

            return Math.Max(0, totalDue - totalPaid);
        }

        /// <summary>
        /// Gets payment history for a rental.
        /// </summary>
        public List<Payment> GetPaymentHistory(int rentalId)
        {
            return _paymentRepo.GetByRentalId(rentalId);
        }

        /// <summary>
        /// Gets all available payment types.
        /// </summary>
        public List<string> GetPaymentTypes()
        {
            return new List<string> { "Cash", "Card", "E-Wallet" };
        }
    }
}
