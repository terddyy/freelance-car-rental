using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Business
{
    /// <summary>
    /// Business logic for managing rental operations.
    /// </summary>
    public class RentalManager
    {
        private readonly RentalRepository _rentalRepo;
        private readonly ReservationRepository _reservationRepo;
        private readonly VehicleRepository _vehicleRepo;
        private readonly CustomerRepository _customerRepo;

        public RentalManager()
        {
            _rentalRepo = new RentalRepository();
            _reservationRepo = new ReservationRepository();
            _vehicleRepo = new VehicleRepository();
            _customerRepo = new CustomerRepository();
        }

        /// <summary>
        /// Checks out a vehicle for a reservation (creates a rental).
        /// </summary>
        public Rental CheckOutVehicle(int reservationId, decimal depositPaid)
        {
            var reservation = _reservationRepo.GetById(reservationId);
            if (reservation == null)
            {
                throw new InvalidOperationException("Reservation not found.");
            }

            if (reservation.Status != "Confirmed")
            {
                throw new InvalidOperationException("Only confirmed reservations can be checked out.");
            }

            // Check if already checked out
            var existingRental = _rentalRepo.GetByReservationId(reservationId);
            if (existingRental != null)
            {
                throw new InvalidOperationException("This reservation has already been checked out.");
            }

            // Create rental
            var rental = new Rental
            {
                ReservationId = reservationId,
                CheckOutTime = DateTime.Now,
                DepositPaid = depositPaid,
                TotalFee = 0,
                Penalties = 0,
                IsPaid = false
            };

            rental.RentalId = _rentalRepo.Insert(rental);

            // Update vehicle status
            var vehicle = _vehicleRepo.GetById(reservation.VehicleId);
            if (vehicle != null)
            {
                vehicle.Status = "Rented";
                _vehicleRepo.Update(vehicle);
            }

            return rental;
        }

        /// <summary>
        /// Checks in a vehicle (completes the rental).
        /// </summary>
        public Rental CheckInVehicle(int rentalId)
        {
            var rental = _rentalRepo.GetById(rentalId);
            if (rental == null)
            {
                throw new InvalidOperationException("Rental not found.");
            }

            if (rental.CheckInTime != null)
            {
                throw new InvalidOperationException("This rental has already been checked in.");
            }

            var reservation = _reservationRepo.GetById(rental.ReservationId);
            if (reservation == null)
            {
                throw new InvalidOperationException("Associated reservation not found.");
            }

            var vehicle = _vehicleRepo.GetById(reservation.VehicleId);
            if (vehicle == null)
            {
                throw new InvalidOperationException("Associated vehicle not found.");
            }

            // Set check-in time
            rental.CheckInTime = DateTime.Now;

            // Calculate total fee and penalties
            CalculateRentalFees(rental, reservation, vehicle);

            // Update rental
            _rentalRepo.Update(rental);

            // Update vehicle status
            vehicle.Status = "Available";
            _vehicleRepo.Update(vehicle);

            // Update reservation status
            reservation.Status = "Completed";
            _reservationRepo.Update(reservation);

            return rental;
        }

        /// <summary>
        /// Calculates rental fees including penalties for late returns.
        /// </summary>
        private void CalculateRentalFees(Rental rental, Reservation reservation, Vehicle vehicle)
        {
            if (rental.CheckInTime == null || rental.CheckOutTime == null)
            {
                return;
            }

            // Calculate actual rental days
            TimeSpan actualDuration = rental.CheckInTime.Value - rental.CheckOutTime;
            int actualDays = (int)Math.Ceiling(actualDuration.TotalDays);
            if (actualDays < 1) actualDays = 1;

            // Calculate base fee
            rental.TotalFee = actualDays * vehicle.DailyRate;

            // Calculate late penalties
            if (rental.CheckInTime.Value.Date > reservation.EndDate.Date)
            {
                TimeSpan lateDuration = rental.CheckInTime.Value.Date - reservation.EndDate.Date;
                int lateDays = (int)lateDuration.TotalDays;

                // Late penalty: 50% surcharge per late day
                rental.Penalties = lateDays * vehicle.DailyRate * 0.5m;
            }
        }

        /// <summary>
        /// Gets rental details with full navigation properties loaded.
        /// </summary>
        public Rental? GetRentalWithDetails(int rentalId)
        {
            var rental = _rentalRepo.GetById(rentalId);
            if (rental == null) return null;

            rental.Reservation = _reservationRepo.GetById(rental.ReservationId);
            if (rental.Reservation != null)
            {
                rental.Reservation.Customer = _customerRepo.GetById(rental.Reservation.CustomerId);
                rental.Reservation.Vehicle = _vehicleRepo.GetById(rental.Reservation.VehicleId);
            }

            return rental;
        }

        /// <summary>
        /// Gets all active rentals with details.
        /// </summary>
        public List<Rental> GetActiveRentalsWithDetails()
        {
            var rentals = _rentalRepo.GetActive();
            foreach (var rental in rentals)
            {
                rental.Reservation = _reservationRepo.GetById(rental.ReservationId);
                if (rental.Reservation != null)
                {
                    rental.Reservation.Customer = _customerRepo.GetById(rental.Reservation.CustomerId);
                    rental.Reservation.Vehicle = _vehicleRepo.GetById(rental.Reservation.VehicleId);
                }
            }
            return rentals;
        }

        /// <summary>
        /// Gets all unpaid rentals with details.
        /// </summary>
        public List<Rental> GetUnpaidRentalsWithDetails()
        {
            var rentals = _rentalRepo.GetUnpaid();
            foreach (var rental in rentals)
            {
                rental.Reservation = _reservationRepo.GetById(rental.ReservationId);
                if (rental.Reservation != null)
                {
                    rental.Reservation.Customer = _customerRepo.GetById(rental.Reservation.CustomerId);
                    rental.Reservation.Vehicle = _vehicleRepo.GetById(rental.Reservation.VehicleId);
                }
            }
            return rentals;
        }
    }
}
