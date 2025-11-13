# Car Rental System - AI Agent Instructions

## Project Overview
This is a standalone C# WinForms desktop application for managing vehicle rentals. Built with .NET 8, SQLite database, and follows clean architecture principles with strict separation of concerns.

## Architecture

### Three-Layer Architecture
1. **Data Layer** (`Data/`): SQLite database access via Repository Pattern
   - `DatabaseManager.cs`: Singleton for database initialization and connection management
   - `Repositories/`: One repository per entity (Vehicle, Customer, Reservation, Rental, Payment)
   - All SQL uses parameterized queries for security
   
2. **Business Layer** (`Business/`): Business logic and orchestration
   - `RentalManager.cs`: Check-out/check-in operations, fee calculations
   - `PaymentManager.cs`: Payment processing, balance calculations
   - `ReservationManager.cs`: Reservation creation, vehicle availability search
   - `DocumentGenerator.cs`: Receipt and rental ticket generation

3. **Presentation Layer** (`Forms/`): WinForms UI
   - `MainForm.cs`: Main shell with menu navigation
   - Management controls: Vehicle, Customer, Reservation, Rental, Payment
   - Dialog forms for CRUD operations and transactions

### Key Design Patterns
- **Repository Pattern**: All database access abstracted through repositories
- **Singleton**: `DatabaseManager.Instance` ensures single database connection source
- **Clean Separation**: UI never contains SQL or business logic; Business layer never references WinForms

## Database Schema
SQLite database auto-created at `%APPDATA%\CarRentalSystem\CarRental.db`

Tables: Vehicles, Customers, Reservations, Rentals, Payments (see `DatabaseManager.InitializeDatabase()` for schema)

## Critical Business Rules
- **Late Penalties**: 50% of daily rate per late day (see `RentalManager.CalculateRentalFees`)
- **Reservation Flow**: Pending → Confirmed → Checked-Out (Rental created) → Checked-In → Payment
- **Vehicle Status**: Available | Rented | Maintenance (status changes during check-out/check-in)
- **Payment Types**: Cash, Card, E-Wallet (defined in `PaymentManager.GetPaymentTypes`)

## Development Workflows

### Building the Project
```bash
dotnet restore
dotnet build
dotnet run --project CarRentalSystem/CarRentalSystem.csproj
```

### Adding New Features
1. Start with Model if new entity needed (`Models/`)
2. Create Repository for data access (`Data/Repositories/`)
3. Add business logic in appropriate Manager (`Business/`)
4. Create UI form/control (`Forms/`)
5. Wire up in `MainForm.cs` navigation

### Database Changes
- Modify `DatabaseManager.InitializeDatabase()` with ALTER TABLE or new CREATE TABLE
- Database auto-upgrades on next run (simple migration strategy)

## Code Conventions

### Naming
- Repositories: `{Entity}Repository` (e.g., `VehicleRepository`)
- Managers: `{Domain}Manager` (e.g., `RentalManager`)
- Forms: `{Purpose}Form` or `{Entity}ManagementControl`

### Security Requirements
- **ALWAYS** use parameterized queries: `command.Parameters.AddWithValue("@param", value)`
- Never concatenate user input into SQL strings
- Example: See any repository method for correct pattern

### Error Handling
- Use try-catch in UI layer, display user-friendly messages
- Business layer throws `InvalidOperationException` or `ArgumentException` with descriptive messages
- Data layer should not throw to user (let business layer interpret)

## Common Tasks

### Adding a New Entity
1. Create model in `Models/` (properties + any business display properties like `DisplayName`)
2. Add table to `DatabaseManager.InitializeDatabase()`
3. Create repository in `Data/Repositories/` following existing pattern (GetAll, GetById, Insert, Update, Delete)
4. Add business logic in new or existing Manager
5. Create management control in `Forms/`

### Modifying Fee Calculation
Edit `RentalManager.CalculateRentalFees()` - this is the single source of truth for rental pricing

### Adding Payment Types
Modify `PaymentManager.GetPaymentTypes()` to return new types

## Testing Approach
No automated tests currently. Manual testing workflow:
1. Add vehicle and customer
2. Create and confirm reservation
3. Check-out vehicle with deposit
4. Check-in vehicle (verify fee calculation)
5. Process payment (verify balance tracking)
6. Generate receipt

## Integration Points
- **SQLite**: Via `System.Data.SQLite` NuGet package
- **WinForms**: Standard .NET WinForms controls, no custom libraries
- **File System**: Uses `Environment.SpecialFolder.ApplicationData` for database location

## Debugging Tips
- Database location: Check `%APPDATA%\CarRentalSystem\CarRental.db`
- Use SQLite browser tools to inspect database directly
- Business logic errors: Check Manager classes first
- UI not updating: Ensure `LoadData()` methods called after operations

## Don't
- ❌ Put SQL in Forms or Business layer
- ❌ Put business logic in Repositories
- ❌ Use string concatenation for SQL queries
- ❌ Reference WinForms types in Business or Data layers
- ❌ Create global database connections (use `DatabaseManager.Instance.GetConnection()`)

## Do
- ✅ Follow Repository Pattern for all data access
- ✅ Use `using` statements for database connections
- ✅ Validate input in both UI and Business layers
- ✅ Display user-friendly error messages
- ✅ Keep forms minimal - delegate to Business layer
- ✅ Use parameterized SQL queries exclusively
