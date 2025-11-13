using CarRentalSystem.Business;
using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Forms
{
    /// <summary>
    /// Customer-facing form to browse and view available rental cars.
    /// </summary>
    public partial class BrowseCarsForm : Form
    {
        private readonly ReservationManager _reservationManager;
        private readonly VehicleRepository _vehicleRepo;
        private FlowLayoutPanel pnlVehicleCards;
        private ComboBox cmbType, cmbLocation;
        private DateTimePicker dtpStart, dtpEnd;
        private Button btnSearch, btnAdminPanel;

        public BrowseCarsForm()
        {
            _reservationManager = new ReservationManager();
            _vehicleRepo = new VehicleRepository();
            
            this.Text = "Browse Available Cars - Rent A Ride";
            this.Size = new Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.WindowState = FormWindowState.Maximized;
            
            InitializeComponents();
            LoadFilters();
            LoadAllAvailableVehicles();
        }

        private void InitializeComponents()
        {
            // Available Vehicles Panel with proper scrolling - ADD FIRST (will be at bottom)
            var vehicleContainerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250),
                AutoScroll = true,
                Padding = new Padding(40, 20, 40, 20)
            };
            
            pnlVehicleCards = new FlowLayoutPanel 
            { 
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(10),
                BackColor = Color.Transparent,
                AutoScroll = false
            };
            
            vehicleContainerPanel.Controls.Add(pnlVehicleCards);
            this.Controls.Add(vehicleContainerPanel);
            
            // Search Filter Panel - ADD SECOND (will be above vehicle panel)
            var filterPanel = new Panel
            {
                Height = 100,
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Padding = new Padding(40, 20, 40, 20)
            };
            
            int filterY = 30;
            
            // Date Range
            AddFilterLabel(filterPanel, "Start Date:", 40, filterY);
            dtpStart = new DateTimePicker 
            { 
                Location = new Point(140, filterY - 3), 
                Size = new Size(180, 27), 
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                Font = new Font("Segoe UI", 10F)
            };
            filterPanel.Controls.Add(dtpStart);
            
            AddFilterLabel(filterPanel, "End Date:", 340, filterY);
            dtpEnd = new DateTimePicker 
            { 
                Location = new Point(420, filterY - 3), 
                Size = new Size(180, 27), 
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddDays(7),
                Font = new Font("Segoe UI", 10F)
            };
            filterPanel.Controls.Add(dtpEnd);
            
            // Type Filter
            AddFilterLabel(filterPanel, "Type:", 620, filterY);
            cmbType = new ComboBox 
            { 
                Location = new Point(680, filterY - 3), 
                Size = new Size(150, 27), 
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                DropDownHeight = 200
            };
            cmbType.DropDown += (s, e) => cmbType.BringToFront();
            filterPanel.Controls.Add(cmbType);
            
            // Location Filter
            AddFilterLabel(filterPanel, "Location:", 850, filterY);
            cmbLocation = new ComboBox 
            { 
                Location = new Point(940, filterY - 3), 
                Size = new Size(180, 27), 
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                DropDownHeight = 200
            };
            cmbLocation.DropDown += (s, e) => cmbLocation.BringToFront();
            filterPanel.Controls.Add(cmbLocation);
            
            // Search Button
            btnSearch = new Button 
            { 
                Text = "🔍 Search", 
                Location = new Point(1140, filterY - 5), 
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;
            filterPanel.Controls.Add(btnSearch);
            
            this.Controls.Add(filterPanel);
            
            // Header Section with Banner - ADD LAST (will be at top)
            var headerPanel = new Panel
            {
                Height = 120,
                BackColor = Color.White,
                Dock = DockStyle.Top
            };
            
            // Main Title
            var lblTitle = new Label
            {
                Text = "RENT A RIDE",
                Location = new Point(40, 20),
                Size = new Size(500, 50),
                Font = new Font("Segoe UI", 32F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 52, 67)
            };
            headerPanel.Controls.Add(lblTitle);
            
            var lblSubtitle = new Label
            {
                Text = "Find your perfect vehicle",
                Location = new Point(40, 75),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(108, 117, 125)
            };
            headerPanel.Controls.Add(lblSubtitle);
            
            // Admin Panel Button (top right) - positioned after panel is added
            btnAdminPanel = new Button
            {
                Text = "🔧 Admin Panel",
                Size = new Size(150, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand
            };
            btnAdminPanel.FlatAppearance.BorderSize = 0;
            btnAdminPanel.Click += BtnAdminPanel_Click;
            headerPanel.Controls.Add(btnAdminPanel);
            
            this.Controls.Add(headerPanel);
            
            // Position admin button correctly after layout
            this.Load += (s, e) =>
            {
                btnAdminPanel.Location = new Point(headerPanel.Width - btnAdminPanel.Width - 40, 40);
            };
            
            // Handle resize to keep admin button positioned and refresh layout
            this.Resize += (s, e) =>
            {
                btnAdminPanel.Location = new Point(headerPanel.Width - btnAdminPanel.Width - 40, 40);
                pnlVehicleCards.Refresh();
            };
        }

        private void AddFilterLabel(Panel parent, string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(90, 20),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            parent.Controls.Add(lbl);
        }

        private void LoadFilters()
        {
            cmbType.Items.Add("All Types");
            cmbType.Items.AddRange(_vehicleRepo.GetDistinctTypes().ToArray());
            cmbType.SelectedIndex = 0;
            
            cmbLocation.Items.Add("All Locations");
            cmbLocation.Items.AddRange(_vehicleRepo.GetDistinctLocations().ToArray());
            cmbLocation.SelectedIndex = 0;
        }

        private void LoadAllAvailableVehicles()
        {
            try
            {
                var startDate = DateTime.Today;
                var endDate = DateTime.Today.AddDays(7);
                
                // Update the date pickers to show what we're searching
                dtpStart.Value = startDate;
                dtpEnd.Value = endDate;
                
                var vehicles = _reservationManager.SearchAvailableVehicles(startDate, endDate, null, null);
                
                if (vehicles == null || vehicles.Count == 0)
                {
                    // If no vehicles found via search, try getting all vehicles directly
                    vehicles = _vehicleRepo.GetAll().Where(v => v.Status == "Available").ToList();
                }
                
                DisplayVehicleCards(vehicles);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            if (dtpStart.Value >= dtpEnd.Value)
            {
                MessageBox.Show("End date must be after start date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                string? type = cmbType.SelectedItem?.ToString() == "All Types" ? null : cmbType.SelectedItem?.ToString();
                string? location = cmbLocation.SelectedItem?.ToString() == "All Locations" ? null : cmbLocation.SelectedItem?.ToString();
                
                var vehicles = _reservationManager.SearchAvailableVehicles(dtpStart.Value, dtpEnd.Value, type, location);
                DisplayVehicleCards(vehicles);
                
                if (vehicles.Count == 0)
                {
                    MessageBox.Show("No vehicles available for the selected criteria.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching vehicles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayVehicleCards(List<Vehicle> vehicles)
        {
            pnlVehicleCards.Controls.Clear();

            if (vehicles == null || vehicles.Count == 0)
            {
                // Show "no vehicles" message
                var noVehiclesLabel = new Label
                {
                    Text = "No vehicles available for the selected dates.\nTry adjusting your search criteria.",
                    Location = new Point(50, 50),
                    Size = new Size(500, 100),
                    Font = new Font("Segoe UI", 14F),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlVehicleCards.Controls.Add(noVehiclesLabel);
                return;
            }

            foreach (var vehicle in vehicles)
            {
                var card = CreateVehicleCard(vehicle);
                pnlVehicleCards.Controls.Add(card);
            }
            
            pnlVehicleCards.Refresh();
        }

        private Panel CreateVehicleCard(Vehicle vehicle)
        {
            var card = new Panel
            {
                Size = new Size(380, 520),
                BackColor = Color.White,
                Margin = new Padding(15),
                Padding = new Padding(0)
            };
            
            // Add shadow effect
            card.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(220, 220, 220), 2), rect);
            };

            // Vehicle Image
            var picVehicle = new PictureBox
            {
                Location = new Point(0, 0),
                Size = new Size(380, 240),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            // Try to load the image
            bool imageLoaded = false;
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                try
                {
                    if (File.Exists(vehicle.ImagePath))
                    {
                        using (var fs = new FileStream(vehicle.ImagePath, FileMode.Open, FileAccess.Read))
                        {
                            picVehicle.Image = Image.FromStream(fs);
                            imageLoaded = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error for debugging
                    System.Diagnostics.Debug.WriteLine($"Error loading image for {vehicle.Make} {vehicle.Model}: {ex.Message}");
                }
            }

            // Show placeholder if no image loaded
            if (!imageLoaded)
            {
                // Create a simple placeholder
                var placeholderLabel = new Label
                {
                    Text = $"🚗\n{vehicle.Type}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 24F),
                    ForeColor = Color.FromArgb(150, 150, 150),
                    BackColor = Color.FromArgb(240, 240, 240)
                };
                picVehicle.Controls.Add(placeholderLabel);
            }
            
            card.Controls.Add(picVehicle);

            // Vehicle Name (with auto-sizing and wrapping)
            var lblName = new Label
            {
                Text = $"{vehicle.Make} {vehicle.Model}",
                Location = new Point(15, 255),
                Size = new Size(350, 40),
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 52, 67),
                AutoSize = false,
                TextAlign = ContentAlignment.TopLeft
            };
            card.Controls.Add(lblName);

            // Details section with better spacing
            int detailY = 305;
            AddVehicleDetail(card, "🎨 Color:", vehicle.Color, 15, detailY);
            detailY += 32;
            AddVehicleDetail(card, "⚙️ Transmission:", vehicle.Transmission, 15, detailY);
            detailY += 32;
            AddVehicleDetail(card, "⛽ Gas Type:", vehicle.GasType, 15, detailY);
            detailY += 32;
            AddVehicleDetail(card, "👥 Capacity:", $"{vehicle.SeatCapacity} Seater", 15, detailY);
            detailY += 45;

            // Rate
            var lblRate = new Label
            {
                Text = $"₱{vehicle.DailyRate:N2}",
                Location = new Point(15, detailY),
                Size = new Size(180, 32),
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69),
                AutoSize = false
            };
            card.Controls.Add(lblRate);
            
            var lblPerDay = new Label
            {
                Text = "/ day",
                Location = new Point(15, detailY + 28),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(108, 117, 125)
            };
            card.Controls.Add(lblPerDay);

            // View Details Button
            var btnDetails = new Button
            {
                Text = "View Details",
                Location = new Point(240, detailY + 5),
                Size = new Size(125, 40),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Tag = vehicle
            };
            btnDetails.FlatAppearance.BorderSize = 0;
            btnDetails.Click += (s, e) => ShowVehicleDetails(vehicle);
            card.Controls.Add(btnDetails);

            return card;
        }

        private void AddVehicleDetail(Panel card, string label, string value, int x, int y)
        {
            var lbl = new Label
            {
                Text = $"{label} {value}",
                Location = new Point(x, y),
                Size = new Size(350, 25),
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(73, 80, 87),
                AutoSize = false
            };
            card.Controls.Add(lbl);
        }

        private void ShowVehicleDetails(Vehicle vehicle)
        {
            string details = $@"Vehicle Details:

Make: {vehicle.Make}
Model: {vehicle.Model}
Type: {vehicle.Type}
Color: {vehicle.Color}
Transmission: {vehicle.Transmission}
Gas Type: {vehicle.GasType}
Seat Capacity: {vehicle.SeatCapacity} Seater
Location: {vehicle.Location}
Registration: {vehicle.RegistrationPlate}
Daily Rate: ₱{vehicle.DailyRate:N2}

To rent this vehicle, please contact the admin or visit the Admin Panel.";

            MessageBox.Show(details, $"{vehicle.Make} {vehicle.Model}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAdminPanel_Click(object? sender, EventArgs e)
        {
            // Open main admin form
            var mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
            mainForm.FormClosed += (s, args) => this.Show();
        }
    }
}
