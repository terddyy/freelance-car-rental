cd Car-rental-system
dotnet restore
dotnet build
dotnet run --project CarRentalSystem/CarRentalSystem.csproj




# Car Rental Reservation System

A standalone desktop application for managing vehicle rentals, built with C#, WinForms, and SQLite.

## Features

### Fleet & Account Management
- **Vehicle Management**: Add, edit, delete vehicles with details (make, model, type, location, registration, daily rate, status)
- **Customer Management**: Maintain customer records with contact info, license numbers, and email

### Reservation & Scheduling
- **Search Availability**: Find available vehicles by date range, type, and location
- **Create Reservations**: Book vehicles for customers with start/end dates
- **Manage Reservations**: Confirm, cancel, or view reservation details

### Rental Operations
- **Check-Out**: Record vehicle check-out with deposit payment and generate rental ticket/agreement
- **Check-In**: Process vehicle returns with automatic fee calculation including late penalties (50% per day)

### Billing & Payment
- **Fee Calculation**: Automatic computation of rental fees based on daily rates and duration
- **Late Penalties**: Automatic penalty calculation for late returns
- **Payment Processing**: Accept multiple payment types (Cash, Card, E-Wallet)
- **Payment History**: Track all payments against rentals
- **Receipt Generation**: Generate and save payment receipts

## Technical Stack

- **Language**: C# (.NET 8)
- **Framework**: Windows Forms
- **Database**: SQLite
- **Architecture**: Clean separation with Repository Pattern and Business Logic Layer

## Project Structure

```
CarRentalSystem/
├── Models/                 # Data models (Vehicle, Customer, Reservation, Rental, Payment)
├── Data/
│   ├── DatabaseManager.cs  # SQLite database initialization and connection
│   └── Repositories/       # Data access layer (Repository Pattern)
├── Business/               # Business logic layer
│   ├── RentalManager.cs    # Rental operations logic
│   ├── PaymentManager.cs   # Payment processing logic
│   ├── ReservationManager.cs # Reservation logic
│   └── DocumentGenerator.cs  # Receipt and ticket generation
├── Forms/                  # WinForms UI components
│   ├── MainForm.cs         # Main application shell
│   ├── VehicleManagementControl.cs
│   ├── CustomerManagementControl.cs
│   ├── ReservationManagementControl.cs
│   ├── RentalManagementControl.cs
│   └── PaymentManagementControl.cs
└── Program.cs             # Application entry point
```

## How to Build and Run

### Prerequisites
- .NET 8 SDK or later
- Windows operating system
- Visual Studio 2022 (recommended) or Visual Studio Code

### Using Visual Studio
1. Open `CarRentalSystem.sln` in Visual Studio
2. Restore NuGet packages (should happen automatically)
3. Press F5 to build and run

### Using Command Line
```bash
# Navigate to the solution directory
cd Car-rental-system

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run --project CarRentalSystem/CarRentalSystem.csproj
```

## Database

The application automatically creates an SQLite database on first run at:
```
%APPDATA%\CarRentalSystem\CarRental.db
```

The database includes the following tables:
- **Vehicles**: Fleet inventory
- **Customers**: Customer records
- **Reservations**: Booking records
- **Rentals**: Active and completed rentals
- **Payments**: Payment transactions

## Usage Workflow

1. **Add Vehicles**: Navigate to Vehicles menu and add your fleet
2. **Add Customers**: Navigate to Customers menu and register customers
3. **Create Reservation**: 
   - Go to Reservations menu
   - Click "New Reservation"
   - Select customer, date range, and search for available vehicles
   - Create the reservation
4. **Confirm Reservation**: Select a pending reservation and click "Confirm"
5. **Check-Out Vehicle**: 
   - Select a confirmed reservation
   - Click "Check-Out"
   - Enter deposit amount
   - Generate and review rental ticket
6. **Check-In Vehicle**: 
   - Go to Rentals menu
   - Select an active rental
   - Click "Check-In Vehicle"
   - Review calculated fees and penalties
7. **Process Payment**: 
   - Go to Payments menu
   - Select an unpaid rental
   - Click "Process Payment"
   - Enter amount and payment method
   - Generate receipt if needed

## Key Business Rules

- **Late Penalties**: 50% of daily rate per day late
- **Deposit**: Applied against final balance
- **Payment Types**: Cash, Card, E-Wallet supported
- **Vehicle Status**: Available, Rented, Maintenance
- **Reservation Status**: Pending, Confirmed, Cancelled, Completed

## Architecture Highlights

### Separation of Concerns
- **Models**: Plain data objects
- **Repositories**: Handle all database operations with parameterized queries
- **Business Managers**: Contain business logic and orchestrate operations
- **Forms**: UI layer with minimal logic

### Security
- All SQL queries use parameterized statements to prevent SQL injection
- Database path secured in user's AppData folder

### Maintainability
- Clean code with comprehensive comments
- Object-oriented design
- Single responsibility principle
- Easy to extend with new features

## License

This is a demonstration project for educational purposes.
