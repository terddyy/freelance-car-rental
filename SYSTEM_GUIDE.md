# Car Rental System - Complete System Guide

## Table of Contents
1. [System Overview](#system-overview)
2. [Database Architecture](#database-architecture)
3. [Database Location & Access](#database-location--access)
4. [Reporting & Data Analysis](#reporting--data-analysis)
5. [Data Export & Backup](#data-export--backup)
6. [System Maintenance](#system-maintenance)
7. [Troubleshooting](#troubleshooting)

---

## System Overview

### Application Architecture
The Car Rental System is a desktop application built with:
- **Platform**: .NET 8 Windows Forms
- **Database**: SQLite (embedded, file-based database)
- **Architecture Pattern**: Three-layer architecture (Data → Business → Presentation)
- **Storage**: Local file system (no server required)

### Key Components
```
┌─────────────────────────────────────────┐
│     Presentation Layer (Forms)          │
│  - WinForms UI Controls & Dialogs       │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│     Business Logic Layer (Managers)     │
│  - RentalManager, PaymentManager, etc.  │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│     Data Access Layer (Repositories)    │
│  - Repository Pattern + SQL Queries     │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│          SQLite Database File           │
│         CarRental.db                    │
└─────────────────────────────────────────┘
```

---

## Database Architecture

### Database Location
**Primary Location:**
```
C:\Users\[USERNAME]\AppData\Roaming\CarRentalSystem\CarRental.db
```

**Quick Access:**
- Press `Windows + R`
- Type: `%APPDATA%\CarRentalSystem`
- Press Enter

The database file is automatically created on the first application launch.

### Database Schema

#### 1. Vehicles Table
Stores the fleet inventory.

| Column | Type | Description | Constraints |
|--------|------|-------------|-------------|
| Id | INTEGER | Unique vehicle identifier | PRIMARY KEY, AUTOINCREMENT |
| Make | TEXT | Vehicle manufacturer | NOT NULL |
| Model | TEXT | Vehicle model name | NOT NULL |
| Type | TEXT | Vehicle category (Sedan, SUV, etc.) | NOT NULL |
| Location | TEXT | Current parking location | NOT NULL |
| RegistrationNumber | TEXT | License plate number | NOT NULL, UNIQUE |
| DailyRate | REAL | Rental cost per day | NOT NULL |
| Status | TEXT | Current status (Available/Rented/Maintenance) | NOT NULL |

**Example Query:**
```sql
SELECT * FROM Vehicles WHERE Status = 'Available' ORDER BY Type, Make;
```

#### 2. Customers Table
Stores customer records.

| Column | Type | Description | Constraints |
|--------|------|-------------|-------------|
| Id | INTEGER | Unique customer identifier | PRIMARY KEY, AUTOINCREMENT |
| Name | TEXT | Full customer name | NOT NULL |
| ContactNumber | TEXT | Phone number | NOT NULL |
| LicenseNumber | TEXT | Driver's license number | NOT NULL, UNIQUE |
| Email | TEXT | Email address | |
| CreatedDate | TEXT | Registration date (ISO 8601 format) | NOT NULL |

**Example Query:**
```sql
SELECT * FROM Customers ORDER BY Name;
```

#### 3. Reservations Table
Stores booking records.

| Column | Type | Description | Constraints |
|--------|------|-------------|-------------|
| Id | INTEGER | Unique reservation identifier | PRIMARY KEY, AUTOINCREMENT |
| CustomerId | INTEGER | Reference to customer | NOT NULL, FOREIGN KEY |
| VehicleId | INTEGER | Reference to vehicle | NOT NULL, FOREIGN KEY |
| StartDate | TEXT | Reservation start date (ISO 8601) | NOT NULL |
| EndDate | TEXT | Reservation end date (ISO 8601) | NOT NULL |
| Status | TEXT | Current status (Pending/Confirmed/Cancelled/Completed) | NOT NULL |
| CreatedDate | TEXT | Booking creation date | NOT NULL |

**Example Query:**
```sql
SELECT 
    r.Id,
    c.Name AS CustomerName,
    v.Make || ' ' || v.Model AS Vehicle,
    r.StartDate,
    r.EndDate,
    r.Status
FROM Reservations r
JOIN Customers c ON r.CustomerId = c.Id
JOIN Vehicles v ON r.VehicleId = v.Id
WHERE r.Status = 'Confirmed'
ORDER BY r.StartDate DESC;
```

#### 4. Rentals Table
Stores active and completed rental transactions.

| Column | Type | Description | Constraints |
|--------|------|-------------|-------------|
| Id | INTEGER | Unique rental identifier | PRIMARY KEY, AUTOINCREMENT |
| ReservationId | INTEGER | Reference to reservation | NOT NULL, FOREIGN KEY |
| CheckOutDate | TEXT | Actual vehicle pickup date | NOT NULL |
| CheckInDate | TEXT | Actual vehicle return date | NULL (until returned) |
| Deposit | REAL | Security deposit paid | NOT NULL |
| TotalFee | REAL | Final rental fee (calculated on check-in) | DEFAULT 0 |
| LatePenalty | REAL | Late return penalty amount | DEFAULT 0 |
| Balance | REAL | Outstanding amount due | DEFAULT 0 |
| RentalTicket | TEXT | Generated rental agreement document | |
| Status | TEXT | Current status (Active/Completed) | NOT NULL |

**Example Query:**
```sql
SELECT 
    r.Id,
    c.Name AS CustomerName,
    v.RegistrationNumber,
    r.CheckOutDate,
    r.CheckInDate,
    r.TotalFee,
    r.LatePenalty,
    r.Balance,
    r.Status
FROM Rentals r
JOIN Reservations res ON r.ReservationId = res.Id
JOIN Customers c ON res.CustomerId = c.Id
JOIN Vehicles v ON res.VehicleId = v.Id
WHERE r.Status = 'Active';
```

#### 5. Payments Table
Stores all payment transactions.

| Column | Type | Description | Constraints |
|--------|------|-------------|-------------|
| Id | INTEGER | Unique payment identifier | PRIMARY KEY, AUTOINCREMENT |
| RentalId | INTEGER | Reference to rental | NOT NULL, FOREIGN KEY |
| Amount | REAL | Payment amount | NOT NULL |
| PaymentType | TEXT | Payment method (Cash/Card/E-Wallet) | NOT NULL |
| PaymentDate | TEXT | Transaction date (ISO 8601) | NOT NULL |
| Notes | TEXT | Additional payment notes | |
| Receipt | TEXT | Generated receipt document | |

**Example Query:**
```sql
SELECT 
    p.Id,
    c.Name AS CustomerName,
    v.RegistrationNumber,
    p.Amount,
    p.PaymentType,
    p.PaymentDate,
    r.Balance AS RemainingBalance
FROM Payments p
JOIN Rentals r ON p.RentalId = r.Id
JOIN Reservations res ON r.ReservationId = res.Id
JOIN Customers c ON res.CustomerId = c.Id
JOIN Vehicles v ON res.VehicleId = v.Id
ORDER BY p.PaymentDate DESC;
```

### Entity Relationships
```
Customers (1) ──→ (N) Reservations
Vehicles (1) ──→ (N) Reservations
Reservations (1) ──→ (1) Rentals
Rentals (1) ──→ (N) Payments
```

---

## Reporting & Data Analysis

### Method 1: Using SQLite Browser (Recommended)
This is the easiest method for generating reports and analyzing data.

#### Installation:
1. Download **DB Browser for SQLite** from: https://sqlitebrowser.org/
2. Install the application
3. Open the database file: `%APPDATA%\CarRentalSystem\CarRental.db`

#### Common Reports:

**1. Revenue Report (Monthly)**
```sql
SELECT 
    strftime('%Y-%m', p.PaymentDate) AS Month,
    COUNT(DISTINCT p.RentalId) AS TotalRentals,
    SUM(p.Amount) AS TotalRevenue,
    AVG(p.Amount) AS AveragePayment
FROM Payments p
GROUP BY strftime('%Y-%m', p.PaymentDate)
ORDER BY Month DESC;
```

**2. Vehicle Utilization Report**
```sql
SELECT 
    v.RegistrationNumber,
    v.Make || ' ' || v.Model AS Vehicle,
    v.Type,
    COUNT(r.Id) AS TotalRentals,
    SUM(r.TotalFee) AS TotalRevenue,
    ROUND(AVG(julianday(r.CheckInDate) - julianday(r.CheckOutDate)), 1) AS AvgRentalDays
FROM Vehicles v
LEFT JOIN Reservations res ON v.Id = res.VehicleId
LEFT JOIN Rentals r ON res.Id = r.ReservationId
WHERE r.Status = 'Completed'
GROUP BY v.Id
ORDER BY TotalRevenue DESC;
```

**3. Customer Activity Report**
```sql
SELECT 
    c.Name,
    c.ContactNumber,
    c.Email,
    COUNT(DISTINCT res.Id) AS TotalReservations,
    COUNT(DISTINCT r.Id) AS TotalRentals,
    COALESCE(SUM(p.Amount), 0) AS TotalSpent,
    MAX(r.CheckOutDate) AS LastRentalDate
FROM Customers c
LEFT JOIN Reservations res ON c.Id = res.CustomerId
LEFT JOIN Rentals r ON res.Id = r.ReservationId
LEFT JOIN Payments p ON r.Id = p.RentalId
GROUP BY c.Id
ORDER BY TotalSpent DESC;
```

**4. Late Returns & Penalties Report**
```sql
SELECT 
    c.Name AS CustomerName,
    v.RegistrationNumber,
    r.CheckOutDate,
    r.CheckInDate,
    res.EndDate AS ScheduledReturn,
    r.LatePenalty,
    CAST(julianday(r.CheckInDate) - julianday(res.EndDate) AS INTEGER) AS DaysLate
FROM Rentals r
JOIN Reservations res ON r.ReservationId = res.Id
JOIN Customers c ON res.CustomerId = c.Id
JOIN Vehicles v ON res.VehicleId = v.Id
WHERE r.LatePenalty > 0
ORDER BY r.LatePenalty DESC;
```

**5. Outstanding Payments Report**
```sql
SELECT 
    r.Id AS RentalId,
    c.Name AS CustomerName,
    c.ContactNumber,
    v.RegistrationNumber,
    r.TotalFee,
    COALESCE(SUM(p.Amount), 0) AS AmountPaid,
    r.Balance AS AmountDue,
    r.CheckInDate AS ReturnDate
FROM Rentals r
JOIN Reservations res ON r.ReservationId = res.Id
JOIN Customers c ON res.CustomerId = c.Id
JOIN Vehicles v ON res.VehicleId = v.Id
LEFT JOIN Payments p ON r.Id = p.RentalId
WHERE r.Balance > 0
GROUP BY r.Id
ORDER BY r.Balance DESC;
```

**6. Fleet Status Overview**
```sql
SELECT 
    v.Status,
    COUNT(*) AS VehicleCount,
    ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM Vehicles), 2) AS Percentage
FROM Vehicles v
GROUP BY v.Status;
```

**7. Daily Revenue Report**
```sql
SELECT 
    DATE(p.PaymentDate) AS Date,
    COUNT(p.Id) AS Transactions,
    SUM(p.Amount) AS DailyRevenue,
    GROUP_CONCAT(p.PaymentType) AS PaymentMethods
FROM Payments p
GROUP BY DATE(p.PaymentDate)
ORDER BY Date DESC
LIMIT 30;
```

### Method 2: Excel/CSV Export

#### Using DB Browser for SQLite:
1. Open the database in DB Browser
2. Go to "Execute SQL" tab
3. Run your query
4. Click "Export to CSV" button
5. Open in Microsoft Excel for charts and pivot tables

#### Using PowerShell (Automated):
```powershell
# Install SQLite PowerShell module (one-time)
Install-Module -Name PSSQLite

# Export Payments data to CSV
$dbPath = "$env:APPDATA\CarRentalSystem\CarRental.db"
$query = "SELECT * FROM Payments ORDER BY PaymentDate DESC"
$data = Invoke-SqliteQuery -DataSource $dbPath -Query $query
$data | Export-Csv -Path "C:\Reports\payments_report.csv" -NoTypeInformation
```

### Method 3: Programmatic Reporting (C# Extension)

You can add custom reporting features to the application by creating a new form:

```csharp
// Example: ReportForm.cs
public class ReportForm : Form
{
    public void GenerateRevenueReport()
    {
        using var conn = DatabaseManager.Instance.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT 
                strftime('%Y-%m', PaymentDate) AS Month,
                SUM(Amount) AS Revenue
            FROM Payments
            GROUP BY strftime('%Y-%m', PaymentDate)
            ORDER BY Month DESC";
        
        using var reader = cmd.ExecuteReader();
        // Display results in DataGridView or export to file
    }
}
```

---

## Data Export & Backup

### Manual Backup
**Simple File Copy:**
1. Close the Car Rental System application
2. Navigate to: `%APPDATA%\CarRentalSystem`
3. Copy `CarRental.db` to your backup location
4. Rename with date: `CarRental_Backup_2025-11-13.db`

### Automated Backup Script
Save this as `backup_database.ps1`:

```powershell
# PowerShell Backup Script
$sourceDb = "$env:APPDATA\CarRentalSystem\CarRental.db"
$backupFolder = "C:\CarRentalBackups"
$timestamp = Get-Date -Format "yyyy-MM-dd_HHmmss"
$backupFile = "$backupFolder\CarRental_$timestamp.db"

# Create backup folder if it doesn't exist
if (-not (Test-Path $backupFolder)) {
    New-Item -ItemType Directory -Path $backupFolder
}

# Copy database
Copy-Item $sourceDb -Destination $backupFile
Write-Host "Backup completed: $backupFile"

# Optional: Keep only last 30 days of backups
Get-ChildItem $backupFolder -Filter "*.db" | 
    Where-Object {$_.LastWriteTime -lt (Get-Date).AddDays(-30)} | 
    Remove-Item
```

**Run Backup:**
```powershell
powershell -ExecutionPolicy Bypass -File backup_database.ps1
```

### Restore from Backup
1. Close the Car Rental System application
2. Navigate to: `%APPDATA%\CarRentalSystem`
3. Rename current `CarRental.db` to `CarRental_old.db` (safety)
4. Copy your backup file and rename it to `CarRental.db`
5. Restart the application

---

## System Maintenance

### Database Health Check

**Check Database Integrity:**
```sql
PRAGMA integrity_check;
```
Expected result: `ok`

**Check Database Size:**
```sql
SELECT 
    page_count * page_size / 1024.0 / 1024.0 AS SizeMB 
FROM pragma_page_count(), pragma_page_size();
```

**Optimize Database (Vacuum):**
```sql
VACUUM;
```
This reclaims unused space and defragments the database.

### Performance Monitoring

**Table Row Counts:**
```sql
SELECT 'Vehicles' AS Table, COUNT(*) AS RowCount FROM Vehicles
UNION ALL
SELECT 'Customers', COUNT(*) FROM Customers
UNION ALL
SELECT 'Reservations', COUNT(*) FROM Reservations
UNION ALL
SELECT 'Rentals', COUNT(*) FROM Rentals
UNION ALL
SELECT 'Payments', COUNT(*) FROM Payments;
```

**Recent Activity Log:**
```sql
SELECT 
    'Reservation' AS Activity,
    CreatedDate AS Timestamp,
    Status
FROM Reservations
UNION ALL
SELECT 
    'Rental',
    CheckOutDate,
    Status
FROM Rentals
UNION ALL
SELECT 
    'Payment',
    PaymentDate,
    PaymentType
FROM Payments
ORDER BY Timestamp DESC
LIMIT 50;
```

### Data Cleanup Recommendations

**Archive Old Completed Rentals (older than 2 years):**
```sql
-- First, export to backup
-- Then delete old records
DELETE FROM Rentals 
WHERE Status = 'Completed' 
  AND CheckInDate < date('now', '-2 years');
```

**Remove Cancelled Reservations (older than 1 year):**
```sql
DELETE FROM Reservations 
WHERE Status = 'Cancelled' 
  AND CreatedDate < date('now', '-1 year');
```

---

## Troubleshooting

### Common Issues

#### Issue 1: "Database is locked" Error
**Cause:** Application is already running or crashed without releasing the database.

**Solution:**
1. Open Task Manager (Ctrl+Shift+Esc)
2. Find and end all `CarRentalSystem.exe` processes
3. Restart the application

#### Issue 2: Database File Not Found
**Cause:** Database was manually deleted or moved.

**Solution:**
The application will automatically create a new database on next launch. If you have a backup, restore it before launching.

#### Issue 3: Corrupted Database
**Symptoms:** Application crashes on startup, data appears incorrect.

**Solution:**
```sql
-- Try to recover
PRAGMA integrity_check;
-- If fails, restore from backup
```

#### Issue 4: Slow Performance
**Cause:** Database needs optimization.

**Solution:**
```sql
-- Rebuild indexes and optimize
REINDEX;
VACUUM;
ANALYZE;
```

### Database Recovery Commands

**Export All Data (Emergency Backup):**
```sql
.mode csv
.headers on
.output vehicles_backup.csv
SELECT * FROM Vehicles;
.output customers_backup.csv
SELECT * FROM Customers;
-- Repeat for all tables
```

**Check for Orphaned Records:**
```sql
-- Reservations without valid customers
SELECT * FROM Reservations 
WHERE CustomerId NOT IN (SELECT Id FROM Customers);

-- Rentals without valid reservations
SELECT * FROM Rentals 
WHERE ReservationId NOT IN (SELECT Id FROM Reservations);
```

---

## Advanced Reporting Features

### Create Custom Views
You can create SQL views for frequently used reports:

```sql
-- Monthly Revenue Summary View
CREATE VIEW IF NOT EXISTS MonthlyRevenue AS
SELECT 
    strftime('%Y-%m', PaymentDate) AS Month,
    COUNT(DISTINCT RentalId) AS Rentals,
    SUM(Amount) AS Revenue,
    AVG(Amount) AS AvgPayment
FROM Payments
GROUP BY Month;

-- Usage:
SELECT * FROM MonthlyRevenue ORDER BY Month DESC;
```

```sql
-- Active Rentals Dashboard View
CREATE VIEW IF NOT EXISTS ActiveRentalsDashboard AS
SELECT 
    r.Id AS RentalId,
    c.Name AS Customer,
    c.ContactNumber,
    v.Make || ' ' || v.Model AS Vehicle,
    v.RegistrationNumber,
    r.CheckOutDate,
    res.EndDate AS DueDate,
    CASE 
        WHEN date('now') > res.EndDate THEN 'OVERDUE'
        ELSE 'ON TIME'
    END AS Status,
    r.Balance AS Outstanding
FROM Rentals r
JOIN Reservations res ON r.ReservationId = res.Id
JOIN Customers c ON res.CustomerId = c.Id
JOIN Vehicles v ON res.VehicleId = v.Id
WHERE r.Status = 'Active';

-- Usage:
SELECT * FROM ActiveRentalsDashboard;
```

---

## System Statistics Dashboard

**Quick Stats Query:**
```sql
SELECT 
    (SELECT COUNT(*) FROM Vehicles WHERE Status = 'Available') AS AvailableVehicles,
    (SELECT COUNT(*) FROM Vehicles WHERE Status = 'Rented') AS RentedVehicles,
    (SELECT COUNT(*) FROM Rentals WHERE Status = 'Active') AS ActiveRentals,
    (SELECT COUNT(*) FROM Reservations WHERE Status = 'Confirmed') AS PendingReservations,
    (SELECT COALESCE(SUM(Balance), 0) FROM Rentals WHERE Balance > 0) AS TotalOutstanding,
    (SELECT COALESCE(SUM(Amount), 0) FROM Payments WHERE DATE(PaymentDate) = DATE('now')) AS TodayRevenue,
    (SELECT COALESCE(SUM(Amount), 0) FROM Payments WHERE strftime('%Y-%m', PaymentDate) = strftime('%Y-%m', 'now')) AS MonthRevenue;
```

---

## Contact & Support

For technical issues with the database or reporting:
1. Check this guide first
2. Verify database integrity with `PRAGMA integrity_check;`
3. Restore from backup if necessary
4. Review application logs (if logging is implemented)

**Database File Location (Quick Reference):**
```
%APPDATA%\CarRentalSystem\CarRental.db
```

---

*Last Updated: November 13, 2025*
