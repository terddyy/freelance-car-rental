using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Business
{
    /// <summary>
    /// Business logic for managing reservations.
    /// </summary>
    public class ReservationManager
    {
        private readonly ReservationRepository _reservationRepo;
        private readonly VehicleRepository _vehicleRepo;
        private readonly CustomerRepository _customerRepo;

        public ReservationManager()
        {
            _reservationRepo = new ReservationRepository();
            _vehicleRepo = new VehicleRepository();
            _customerRepo = new CustomerRepository();
        }

        /// <summary>
        /// Creates a new reservation.
        /// </summary>
        public Reservation CreateReservation(int customerId, int vehicleId, DateTime startDate, DateTime endDate)
        {
            // Validate dates
            if (startDate >= endDate)
            {
                throw new ArgumentException("End date must be after start date.");
            }

            if (startDate.Date < DateTime.Today)
            {
                throw new ArgumentException("Start date cannot be in the past.");
            }

            // Check if vehicle is available
            var availableVehicles = _vehicleRepo.SearchAvailable(startDate, endDate);
            if (!availableVehicles.Any(v => v.VehicleId == vehicleId))
            {
                throw new InvalidOperationException("Vehicle is not available for the selected dates.");
            }

            var reservation = new Reservation
            {
                CustomerId = customerId,
                VehicleId = vehicleId,
                StartDate = startDate,
                EndDate = endDate,
                Status = "Pending",
                DateCreated = DateTime.Now
            };

            reservation.ReservationId = _reservationRepo.Insert(reservation);
            return reservation;
        }

        /// <summary>
        /// Confirms a reservation.
        /// </summary>
        public void ConfirmReservation(int reservationId)
        {
            var reservation = _reservationRepo.GetById(reservationId);
            if (reservation == null)
            {
                throw new InvalidOperationException("Reservation not found.");
            }

            if (reservation.Status != "Pending")
            {
                throw new InvalidOperationException("Only pending reservations can be confirmed.");
            }

            reservation.Status = "Confirmed";
            _reservationRepo.Update(reservation);
        }

        /// <summary>
        /// Cancels a reservation.
        /// </summary>
        public void CancelReservation(int reservationId)
        {
            var reservation = _reservationRepo.GetById(reservationId);
            if (reservation == null)
            {
                throw new InvalidOperationException("Reservation not found.");
            }

            if (reservation.Status == "Completed")
            {
                throw new InvalidOperationException("Completed reservations cannot be cancelled.");
            }

            reservation.Status = "Cancelled";
            _reservationRepo.Update(reservation);
        }

        /// <summary>
        /// Gets reservation with full details.
        /// </summary>
        public Reservation? GetReservationWithDetails(int reservationId)
        {
            var reservation = _reservationRepo.GetById(reservationId);
            if (reservation == null) return null;

            reservation.Customer = _customerRepo.GetById(reservation.CustomerId);
            reservation.Vehicle = _vehicleRepo.GetById(reservation.VehicleId);

            return reservation;
        }

        /// <summary>
        /// Gets all reservations with details.
        /// </summary>
        public List<Reservation> GetAllReservationsWithDetails()
        {
            var reservations = _reservationRepo.GetAll();
            foreach (var reservation in reservations)
            {
                reservation.Customer = _customerRepo.GetById(reservation.CustomerId);
                reservation.Vehicle = _vehicleRepo.GetById(reservation.VehicleId);
            }
            return reservations;
        }

        /// <summary>
        /// Gets active reservations with details.
        /// </summary>
        public List<Reservation> GetActiveReservationsWithDetails()
        {
            var reservations = _reservationRepo.GetActive();
            foreach (var reservation in reservations)
            {
                reservation.Customer = _customerRepo.GetById(reservation.CustomerId);
                reservation.Vehicle = _vehicleRepo.GetById(reservation.VehicleId);
            }
            return reservations;
        }

        /// <summary>
        /// Searches for available vehicles.
        /// </summary>
        public List<Vehicle> SearchAvailableVehicles(DateTime startDate, DateTime endDate, string? type = null, string? location = null)
        {
            return _vehicleRepo.SearchAvailable(startDate, endDate, type, location);
        }
    }
}
