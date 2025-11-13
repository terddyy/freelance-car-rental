Here is a complete prompt based on your technical requirements (C#, WinForms, SQLite, Minimalistic) and the functional requirements from the image you provided.

✍️ Complete Application Prompt
"Develop a Car Rental Reservation System desktop application.

1. Core Requirements & Technology
Language: C#

Framework: WinForms

Database: SQLite (for all local data persistence)

Design: The UI must be minimalistic, clean, and intuitive.

2. Core Functionality
Based on the requirements, the system must support the following functions:

A. Account & Vehicle Management

Vehicle Accounts: Ability to add, edit, and manage the fleet of vehicles (e.g., car details, type, location).

Customer Accounts: (Implied by "Add Vehicle Account") Ability to create and manage customer profiles.

B. Reservation & Scheduling

Vehicle Reservation:

Allow an operator to search for available vehicles by date, type, or location.

Create new reservation requests.

Manage the status of reservations (e.g., confirmations, cancellations).

Vehicle Scheduling: Provide a visual calendar or list view to see which vehicles are booked or available.

C. Rental Operations

Check-Out / Check-In: Implement a module to record the "Time out" (check-out) and "Time in" (check-in) of vehicles.

D. Billing & Payments

Rental Tracking & Fees:

Automatically compute rental fees based on the rental duration.

Calculate and apply deposits.

Automatically compute penalties for late returns.

Payment Processing:

Accept multiple payment types (e.g., cash, card, e-wallet) and record the transaction.

Record payment history for each customer or rental.

Documentation:

Generate receipts for payments.

Generate a rental ticket (agreement) at the time of check-out.

3. Data Model (SQLite)
The database should be structured to include, at a minimum, the following tables:

Vehicles: (VehicleID, Make, Model, Type, Location, RegistrationPlate, DailyRate, Status)

Customers: (CustomerID, Name, ContactInfo, LicenseNumber)

Reservations: (ReservationID, CustomerID, VehicleID, StartDate, EndDate, Status [Confirmed, Cancelled])

Rentals: (RentalID, ReservationID, CheckOutTime, CheckInTime, TotalFee, DepositPaid, Penalties, IsPaid)

Payments: (PaymentID, RentalID, Amount, PaymentType, TransactionDate)

4. Design & UX (Minimalistic)
Layout: The interface should be uncluttered. A tabbed interface (e.g., "Dashboard," "Reservations," "Vehicles," "Customers") or a simple main menu is recommended.

Workflows: Prioritize the main user flow:

Search for a vehicle.

Create a reservation.

Convert reservation to an active rental (check-out).

Process the return (check-in) and calculate the final bill.

Process payment.

Controls: Use standard WinForms controls. Avoid custom, heavy styling. Focus on clarity and ease of use."