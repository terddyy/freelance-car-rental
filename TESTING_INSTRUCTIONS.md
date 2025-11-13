# Testing Instructions - Card-Based Vehicle Display

## What Was Changed

### 1. **Database Seeding**
   - Added automatic sample data creation when the database is first initialized
   - 4 sample vehicles with images and details are pre-loaded:
     - Toyota Fortuner (Grey, Automatic, Diesel) - ₱1,500/day
     - Nissan Terra (Brown, Manual, Gas) - ₱2,000/day
     - Mitsubishi Montero Sport (White, Automatic, Gas) - ₱2,500/day
     - Ford Everest Sport (Blue, Automatic, Gas) - ₱3,500/day
   - 1 sample customer (John Doe) is pre-loaded

### 2. **Reservation Form UI**
   - **Changed from**: Simple ListBox showing vehicle names
   - **Changed to**: Beautiful card-based layout displaying:
     - Vehicle image (from car-rental-images folder)
     - Vehicle name (Make + Model)
     - Car specifications (Color, Transmission, Gas Type, Seat Capacity)
     - Daily rate prominently displayed
     - Blue "Rent" button on each card
   
### 3. **Visual Design**
   - Cards are arranged in a scrollable grid (2 per row)
   - Each card is 540px × 320px
   - Modern color scheme with professional styling
   - Images on the left, details on the right
   - Clean, minimalistic design matching the reference image

## How to Test

### Step 1: Delete Old Database (if exists)
The application might be holding the database open. Close any running instances first, then:

1. Close ALL instances of CarRentalSystem
2. Navigate to: `C:\Users\PC 22\AppData\Roaming\CarRentalSystem`
3. Delete `CarRental.db` file
4. The app will create a fresh database with sample data on next run

### Step 2: Run the Application
```bash
cd "c:\Users\PC 22\Desktop\terd\Car-rental-system"
dotnet run --project CarRentalSystem/CarRentalSystem.csproj
```

### Step 3: Test the Card View
1. From the main menu, click **Reservations** → **Create Reservation**
2. Select the pre-loaded customer "John Doe"
3. Select start and end dates
4. Click **"Search Available Vehicles"**
5. You should now see beautiful vehicle cards with:
   - Car images (for Toyota, Nissan, and Mitsubishi)
   - All vehicle details nicely formatted
   - A blue "Rent" button on each card

### Step 4: Make a Reservation
1. Click the **"Rent"** button on any vehicle card
2. The reservation will be created immediately
3. You'll see a success message with the reservation ID

## Expected Results

✅ **You should see:**
- Large, attractive vehicle cards instead of a plain list
- Car images displayed for Toyota Fortuner, Nissan Terra, and Mitsubishi Montero Sport
- All vehicle specifications clearly visible
- Professional blue "Rent" buttons
- Smooth scrolling if there are many vehicles

✅ **User Experience:**
- Much more visual and intuitive
- Easy to compare vehicles at a glance
- One-click to select and reserve a vehicle
- Matches the modern UI design from your reference image

## Troubleshooting

### Problem: Database is locked
**Solution**: Close all instances of the app, then delete the database file manually

### Problem: Images not showing
**Solution**: Check that these files exist:
- `c:\Users\PC 22\Desktop\terd\Car-rental-system\car-rental-images\cars\fortuner.png`
- `c:\Users\PC 22\Desktop\terd\Car-rental-system\car-rental-images\cars\nissan-tera.png`
- `c:\Users\PC 22\Desktop\terd\Car-rental-system\car-rental-images\cars\jaero-sport.png`

### Problem: No vehicles showing
**Solution**: The database might already have data. Delete `CarRental.db` and restart the app

## Next Steps

If you want to add more vehicles with images:
1. Add vehicle images to `car-rental-images/cars/` folder
2. Go to **Vehicles** menu → **Add Vehicle**
3. Fill in all details and include the full path to the image
4. The new vehicle will appear as a card in the reservation search

## Architecture Notes

- Cards are dynamically generated from the Vehicle list
- Each card is a self-contained Panel control
- The FlowLayoutPanel automatically arranges cards in a grid
- Images are loaded on-demand when displaying cards
- Clicking "Rent" sets the selected vehicle and immediately creates the reservation
