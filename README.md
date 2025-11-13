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

### Quick Start After Cloning

If you just cloned this repository, follow these steps to run the application:

#### Option 1: Using Visual Studio (Recommended)

1. **Open the Solution**
   ```bash
   # Navigate to the cloned folder
   cd freelance-car-rental
   ```
   - Double-click `CarRentalSystem.sln` to open in Visual Studio 2022

2. **Restore and Run**
   - Visual Studio will automatically restore NuGet packages
   - Press `F5` (or click the green ▶️ Start button)
   - The application will build and launch

#### Option 2: Using Command Line

1. **Navigate to the Project**
   ```powershell
   # Open PowerShell or Command Prompt
   cd path\to\freelance-car-rental
   ```

2. **Restore Dependencies**
   ```powershell
   dotnet restore
   ```

3. **Build the Project**
   ```powershell
   dotnet build
   ```

4. **Run the Application**
   ```powershell
   dotnet run --project CarRentalSystem/CarRentalSystem.csproj
   ```

**First Run Notes:**
- The database will be created automatically on first run
- Sample data (4 vehicles and 1 customer) will be added automatically
- The database location: `%APPDATA%\CarRentalSystem\CarRental.db`

### Prerequisites
- .NET 8 SDK or later ([Download here](https://dotnet.microsoft.com/download/dotnet/8.0))
- Windows operating system
- Visual Studio 2022 (recommended) or Visual Studio Code

### Using Visual Studio

If you're using Visual Studio 2022 (recommended for WinForms development), follow these detailed steps:

#### 1. Open the Solution
```powershell
# Navigate to the cloned folder
cd freelance-car-rental
```
- Double-click `CarRentalSystem.sln` to open in Visual Studio 2022
- Or in Visual Studio: **File → Open → Project/Solution** and select the `.sln` file

#### 2. Restore NuGet Packages
- Visual Studio automatically restores packages when you open the solution
- If needed, manually restore: **Right-click solution in Solution Explorer → Restore NuGet Packages**
- Required packages:
  - `System.Data.SQLite` (for database operations)
  - `System.Data.SQLite.Core`

#### 3. Build the Solution
- **Build → Build Solution** (or press `Ctrl + Shift + B`)
- Check the **Output** window for build messages
- Ensure build succeeds with "Build succeeded" message

#### 4. Run the Application

**Option A: Run Normally**
- Press `F5` (or click the green ▶️ **Start** button)
- Or **Debug → Start Debugging**
- The application window will launch

**Option B: Run Without Debugger**
- Press `Ctrl + F5`
- Or **Debug → Start Without Debugging**
- Runs faster, useful when you don't need to debug

#### 5. Debugging in Visual Studio

**Set Breakpoints:**
- Click in the left margin (gray bar) next to any line of code
- A red dot appears indicating a breakpoint
- Code execution will pause at that line

**Debug Controls:**
- `F5` - Continue execution
- `F10` - Step Over (execute current line)
- `F11` - Step Into (enter method)
- `Shift + F11` - Step Out (exit current method)
- `Shift + F5` - Stop debugging

**Useful Windows During Debugging:**
- **Locals** (Debug → Windows → Locals) - View variable values
- **Watch** (Debug → Windows → Watch) - Monitor specific variables
- **Call Stack** (Debug → Windows → Call Stack) - See method call hierarchy
- **Output** - View debug messages and exceptions

#### 6. Solution Explorer Navigation

- **Solution Explorer** (View → Solution Explorer or `Ctrl + Alt + L`)
- Expand folders to see project structure:
  - `Forms/` - All UI forms and controls
  - `Models/` - Data classes
  - `Data/Repositories/` - Database access layer
  - `Business/` - Business logic managers

#### 7. Viewing and Editing Forms

**To Edit UI (Designer):**
- In Solution Explorer, navigate to `Forms/` folder
- **Right-click** any `.cs` form file → **View Designer**
- Or select the file and press `Shift + F7`

**To Edit Code:**
- **Right-click** form file → **View Code**
- Or select the file and press `F7`

#### 8. Managing Project Settings

**Set Startup Project:**
- If you have multiple projects, right-click `CarRentalSystem` → **Set as Startup Project**

**Project Properties:**
- Right-click `CarRentalSystem` → **Properties**
- View/edit target framework (.NET 8.0)
- Configure build options and output paths

#### Troubleshooting in Visual Studio

**If build fails:**
- Clean the solution: **Build → Clean Solution**
- Rebuild: **Build → Rebuild Solution**
- Check **Error List** window (View → Error List) for specific errors

**If NuGet packages don't restore:**
- **Tools → NuGet Package Manager → Manage NuGet Packages for Solution**
- Check for missing packages and restore them

**If database errors occur:**
- Check Output window for exception details
- Database auto-creates on first run at `%APPDATA%\CarRentalSystem\CarRental.db`

### Using Command Line
```bash
# Navigate to the solution directory
cd freelance-car-rental

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run --project CarRentalSystem/CarRentalSystem.csproj
```

### Using Visual Studio Code

If you're using VS Code instead of Visual Studio, follow these steps:

#### 1. Install Required Extensions
- Install the **C# Dev Kit** extension (by Microsoft)
  - Open VS Code
  - Press `Ctrl + Shift + X` to open Extensions
  - Search for "C# Dev Kit" and install it
  - This includes C# language support and debugging tools

#### 2. Open the Project
```powershell
# Navigate to the cloned folder
cd freelance-car-rental

# Open in VS Code
code .
```

#### 3. Build and Run

**Option A: Using Integrated Terminal**
1. Open terminal in VS Code (`Ctrl + `` ` `` or View → Terminal)
2. Run these commands:
   ```powershell
   # Restore dependencies
   dotnet restore
   
   # Build the project
   dotnet build
   
   # Run the application
   dotnet run --project CarRentalSystem/CarRentalSystem.csproj
   ```

**Option B: Using Tasks (Recommended)**
1. Press `Ctrl + Shift + B` to build
2. Or press `F5` to run with debugger
   - If prompted, select ".NET Core" as the environment
   - VS Code will auto-generate launch configurations

**Option C: Using the Run Menu**
1. Click **Run → Start Debugging** (or press `F5`)
2. Select **.NET 5+ and .NET Core** when prompted
3. The app will build and launch

#### 4. Debugging in VS Code
- Set breakpoints by clicking left of line numbers
- Press `F5` to start debugging
- Use Debug toolbar to step through code (F10, F11, etc.)

#### Important Notes for VS Code Users
- **No Visual Designer**: VS Code doesn't have a drag-and-drop WinForms designer
  - To edit UI visually, use Visual Studio 2022
  - Or manually edit the `.Designer.cs` files (not recommended for beginners)
- **IntelliSense**: Works great for code editing and navigation
- **Database Viewing**: Install "SQLite" extension to view `.db` files directly in VS Code

## System Flow (Non-Technical Explanation)

Think of this system like running a physical car rental shop, but digitally:

1. **Setup Your Inventory** 📋
   - Add all your cars to the system (like writing them in your inventory book)
   - Register your customers (like keeping customer files)

2. **Customer Makes a Booking** 📞
   - Customer calls and wants to rent a car for specific dates
   - You search which cars are free during those dates
   - You create a reservation (like penciling in their name on the calendar)

3. **Customer Confirms** ✅
   - When the customer confirms, you mark the reservation as "Confirmed"
   - The car is now blocked for those dates (no one else can book it)

4. **Pick-Up Day** 🚗
   - Customer arrives to pick up the car
   - You "Check-Out" the vehicle (hand over the keys)
   - Customer pays a deposit
   - System prints a rental agreement/ticket

5. **Return Day** 🔄
   - Customer brings the car back
   - You "Check-In" the vehicle (get the keys back)
   - System calculates: rental days × daily rate + any late fees
   - If they're late, system adds 50% penalty per extra day

6. **Final Payment** 💳
   - Customer pays the remaining balance (total cost - deposit)
   - You select payment method (Cash/Card/E-Wallet)
   - System generates a receipt
   - Transaction complete!

**The system keeps track of everything automatically** - no manual calculations, no lost papers, all information stored safely in a database.

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

## How to Edit the UI with Visual Studio (Drag & Drop)

This application uses **Windows Forms**, which has a visual designer in Visual Studio that lets you edit the interface by dragging and dropping controls.

### Opening the Visual Designer

1. **Open the Project in Visual Studio 2022**
   - Open `CarRentalSystem.sln` in Visual Studio (not VS Code)
   - Wait for the solution to load

2. **Navigate to a Form**
   - In **Solution Explorer** (usually on the right), expand the `Forms` folder
   - You'll see forms like `MainForm.cs`, `VehicleEditForm.cs`, etc.

3. **Open the Designer**
   - **Right-click** on any `.cs` file (e.g., `MainForm.cs`)
   - Select **View Designer** (or press `Shift + F7`)
   - The visual designer opens showing the form layout

### Using the Drag & Drop Designer

1. **Open the Toolbox**
   - Press `Ctrl + Alt + X` or go to **View → Toolbox**
   - You'll see a list of controls: Button, Label, TextBox, DataGridView, etc.

2. **Add Controls**
   - **Drag** any control from the Toolbox onto your form
   - **Drop** it where you want it to appear
   - Resize by dragging the corners/edges

3. **Edit Properties**
   - **Click** any control to select it
   - Press `F4` or go to **View → Properties Window**
   - Change properties like:
     - `Text` (the label/button text)
     - `Font` (size and style)
     - `BackColor` / `ForeColor` (colors)
     - `Size` and `Location` (position)
     - `Dock` (fill parent container)
     - `Anchor` (responsive positioning)

4. **Add Event Handlers**
   - Select a control (e.g., a Button)
   - In the Properties window, click the **⚡ lightning bolt icon** (Events)
   - Double-click next to an event name (e.g., `Click`)
   - Visual Studio automatically creates a method and opens the code editor

5. **Save and Build**
   - Press `Ctrl + S` to save
   - Press `F5` to build and run the app with your changes

### Important Files

- **FormName.Designer.cs**: Contains the visual designer code (auto-generated, avoid manual editing)
- **FormName.cs**: Contains your event handlers and custom logic (edit this manually)

### Tips for Editing UI

- Use **Layout Panels** (`TableLayoutPanel`, `FlowLayoutPanel`) for organized layouts
- Set `Dock = Fill` to make controls resize with the window
- Use `Anchor` property to keep controls positioned when window resizes
- Test frequently by pressing `F5` to see how it looks when running
- The designer auto-saves changes to `.Designer.cs` files

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

### How to Open the Database with SQLite Browser

**DB Browser for SQLite** is a free, open-source tool that lets you view and edit SQLite databases with a graphical interface.

#### 1. Download and Install SQLite Browser

- Download from: [https://sqlitebrowser.org/dl/](https://sqlitebrowser.org/dl/)
- Choose the Windows installer (`.msi` or `.exe`)
- Install with default settings

#### 2. Locate the Database File

The database file is stored in your user's AppData folder. To find it:

**Option A: Copy the Path Directly**
- Press `Win + R` to open Run dialog
- Paste this path and press Enter:
  ```
  %APPDATA%\CarRentalSystem
  ```
- You'll see the `CarRental.db` file

**Option B: Navigate Manually**
- Open File Explorer
- Navigate to: `C:\Users\[YourUsername]\AppData\Roaming\CarRentalSystem\`
- Look for `CarRental.db`

#### 3. Open the Database

- Launch **DB Browser for SQLite**
- Click **Open Database** button (or File → Open Database)
- Navigate to `%APPDATA%\CarRentalSystem\`
- Select `CarRental.db` and click Open

#### 4. View and Edit Data

Once opened, you can:

- **Browse Data** tab: View and edit table contents
  - Click on any table (Vehicles, Customers, etc.)
  - See all records in a spreadsheet-like view
  - Click cells to edit data directly
  - Click "Write Changes" to save

- **Database Structure** tab: View table schemas
  - See all columns, data types, and constraints
  - Useful for understanding the database design

- **Execute SQL** tab: Run custom queries
  - Write SQL commands to search, filter, or modify data
  - Example: `SELECT * FROM Vehicles WHERE Status = 'Available'`

- **Browse Tables**: Expand the tree on the left to see all tables and their structure

#### 5. Important Tips

- **Always close the app first**: Close the Car Rental System application before opening the database in SQLite Browser (SQLite locks the database when in use)
- **Backup before editing**: Make a copy of `CarRental.db` before making manual changes
- **Read-only mode**: If you just want to view data, open in read-only mode (Database → Open Database Read Only)

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



