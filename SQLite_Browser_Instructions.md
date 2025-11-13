# SQLite Browser Setup and Usage Instructions

This guide provides step-by-step instructions for downloading, installing, and using DB Browser for SQLite to open and inspect the Car Rental System database.

## Prerequisites
- Windows operating system
- Internet connection for downloading the installer

## Step 1: Download DB Browser for SQLite
1. Open your web browser and navigate to the official DB Browser for SQLite website: [https://sqlitebrowser.org/](https://sqlitebrowser.org/)
2. Scroll down to the "Downloads" section.
3. Click on the download link for the Windows MSI installer (e.g., `DB.Browser.for.SQLite-3.x.x-win64.msi` for 64-bit Windows).
4. Save the MSI file to your Downloads folder or another convenient location.

## Step 2: Install DB Browser for SQLite
1. Locate the downloaded MSI file (e.g., `DB.Browser.for.SQLite-3.x.x-win64.msi`).
2. Double-click the file to start the installation process.
3. Follow the on-screen prompts in the installer wizard:
   - Accept the license agreement.
   - Choose the installation location (default is usually fine).
   - Click "Install" and wait for the installation to complete.
4. Once installed, click "Finish" to close the installer.

## Step 3: Launch DB Browser for SQLite
1. Open the Start menu and search for "DB Browser for SQLite".
2. Click on the application icon to launch it.
   - Alternatively, find the shortcut on your desktop or in the Start menu.

## Step 4: Open the Car Rental System Database
1. In the DB Browser for SQLite window, click on **"Open Database"** (or go to **File > Open Database** from the menu).
2. In the file dialog, navigate to the database location:
   - Path: `C:\Users\PC 22\AppData\Roaming\CarRentalSystem\CarRental.db`
   - Note: The `AppData` folder may be hidden. To show hidden folders:
     - In File Explorer, click on the "View" tab.
     - Check the box for "Hidden items".
3. Select the `CarRental.db` file and click **"Open"**.
4. The database will load, and you should see the tables in the left panel (e.g., Vehicles, Customers, Reservations, etc.).

## Step 5: Explore the Database
- **Browse Data**: Click the **"Browse Data"** tab to view records in each table.
- **Database Structure**: Click **"Database Structure"** to see table schemas.
- **Execute SQL**: Use the **"Execute SQL"** tab to run custom queries.
- **Edit Data**: You can modify data directly if needed (use caution).

## Troubleshooting
- If the database file is not found, ensure the Car Rental System application has been run at least once to create the database.
- If access is denied, try running DB Browser for SQLite as Administrator (right-click the icon and select "Run as administrator").
- For more help, refer to the official documentation at [https://sqlitebrowser.org/](https://sqlitebrowser.org/).

## Notes
- This tool is for local database inspection only. Changes made here will affect the live application data.
- Always back up the database before making edits (File > Export > Database to SQL file).