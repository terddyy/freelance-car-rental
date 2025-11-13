using CarRentalSystem.Business;
using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Forms
{
    public partial class ReservationCreateForm : Form
    {
        private readonly ReservationManager _reservationManager;
        private readonly CustomerRepository _customerRepo;
        private readonly VehicleRepository _vehicleRepo;
        
        private ComboBox cmbCustomer, cmbVehicle, cmbType, cmbLocation;
        private DateTimePicker dtpStart, dtpEnd;
        private Button btnSearch;
        private FlowLayoutPanel pnlVehicleCards;
        private Vehicle? selectedVehicle;

        public ReservationCreateForm()
        {
            _reservationManager = new ReservationManager();
            _customerRepo = new CustomerRepository();
            _vehicleRepo = new VehicleRepository();
            
            this.Text = "Create New Reservation";
            this.Size = new Size(1200, 700);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.AutoScroll = true;
            
            InitializeComponents();
            LoadCustomers();
            LoadFilters();
            LoadInitialVehicles();
        }

        private void LoadInitialVehicles()
        {
            try
            {
                // Load all available vehicles on startup
                var startDate = DateTime.Today;
                var endDate = DateTime.Today.AddDays(7);
                var vehicles = _reservationManager.SearchAvailableVehicles(startDate, endDate, null, null);
                DisplayVehicleCards(vehicles);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponents()
        {
            int y = 20;
            
            // Customer selection
            AddLabel("Customer:", 20, y);
            cmbCustomer = AddComboBox(150, y, 400);
            y += 40;
            
            // Date range
            AddLabel("Start Date:", 20, y);
            dtpStart = new DateTimePicker { Location = new Point(150, y), Size = new Size(200, 23), Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            this.Controls.Add(dtpStart);
            
            AddLabel("End Date:", 380, y);
            dtpEnd = new DateTimePicker { Location = new Point(480, y), Size = new Size(200, 23), Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(7) };
            this.Controls.Add(dtpEnd);
            y += 40;
            
            // Search filters
            AddLabel("Type (Optional):", 20, y);
            cmbType = AddComboBox(150, y, 200);
            
            AddLabel("Location (Optional):", 380, y);
            cmbLocation = AddComboBox(480, y, 200);
            y += 40;
            
            btnSearch = new Button { Text = "Search Available Vehicles", Location = new Point(150, y), Size = new Size(200, 35), BackColor = Color.FromArgb(0, 123, 255), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);
            y += 60;
            
            // Available vehicles cards panel
            AddLabel("Available Vehicles:", 20, y, 160);
            y += 30;
            
            pnlVehicleCards = new FlowLayoutPanel 
            { 
                Location = new Point(20, y), 
                Size = new Size(1140, 480),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            this.Controls.Add(pnlVehicleCards);
        }

        private void AddLabel(string text, int x, int y, int width = 120)
        {
            this.Controls.Add(new Label { Text = text, Location = new Point(x, y), Size = new Size(width, 20), AutoSize = false, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
        }

        private ComboBox AddComboBox(int x, int y, int width)
        {
            var cmb = new ComboBox { Location = new Point(x, y), Size = new Size(width, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            this.Controls.Add(cmb);
            return cmb;
        }

        private void LoadCustomers()
        {
            var customers = _customerRepo.GetAll();
            cmbCustomer.DisplayMember = "DisplayName";
            cmbCustomer.ValueMember = "CustomerId";
            cmbCustomer.DataSource = customers;
        }

        private void LoadFilters()
        {
            cmbType.Items.Add("(Any)");
            cmbType.Items.AddRange(_vehicleRepo.GetDistinctTypes().ToArray());
            cmbType.SelectedIndex = 0;
            
            cmbLocation.Items.Add("(Any)");
            cmbLocation.Items.AddRange(_vehicleRepo.GetDistinctLocations().ToArray());
            cmbLocation.SelectedIndex = 0;
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
                string? type = cmbType.SelectedItem?.ToString() == "(Any)" ? null : cmbType.SelectedItem?.ToString();
                string? location = cmbLocation.SelectedItem?.ToString() == "(Any)" ? null : cmbLocation.SelectedItem?.ToString();
                
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
            selectedVehicle = null;

            foreach (var vehicle in vehicles)
            {
                var card = CreateVehicleCard(vehicle);
                pnlVehicleCards.Controls.Add(card);
            }
        }

        private Panel CreateVehicleCard(Vehicle vehicle)
        {
            var card = new Panel
            {
                Size = new Size(540, 320),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(15)
            };

            // Vehicle name label
            var lblName = new Label
            {
                Text = $"{vehicle.Make} {vehicle.Model}",
                Location = new Point(15, 15),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            card.Controls.Add(lblName);

            // Vehicle image
            var picVehicle = new PictureBox
            {
                Location = new Point(15, 50),
                Size = new Size(280, 200),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.None
            };

            // Load image if path exists
            if (!string.IsNullOrEmpty(vehicle.ImagePath) && File.Exists(vehicle.ImagePath))
            {
                try
                {
                    picVehicle.Image = Image.FromFile(vehicle.ImagePath);
                }
                catch
                {
                    picVehicle.BackColor = Color.LightGray;
                }
            }
            else
            {
                picVehicle.BackColor = Color.FromArgb(240, 240, 240);
            }
            card.Controls.Add(picVehicle);

            // Details panel (right side)
            int detailsX = 310;
            int detailsY = 50;

            // Car Color
            AddDetailLabel(card, "Car Color:", vehicle.Color, detailsX, detailsY);
            detailsY += 35;

            // Transmission
            AddDetailLabel(card, "Transmission:", vehicle.Transmission, detailsX, detailsY);
            detailsY += 35;

            // Gas Type
            AddDetailLabel(card, "Gas Type:", vehicle.GasType, detailsX, detailsY);
            detailsY += 35;

            // Seat Capacity
            AddDetailLabel(card, "Seat Capacity:", $"{vehicle.SeatCapacity} Seater", detailsX, detailsY);
            detailsY += 35;

            // Rate
            var lblRateTitle = new Label
            {
                Text = "Rate:",
                Location = new Point(detailsX, detailsY),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(102, 102, 102)
            };
            card.Controls.Add(lblRateTitle);

            var lblRate = new Label
            {
                Text = $"₱{vehicle.DailyRate:N0} per day",
                Location = new Point(detailsX, detailsY + 20),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            card.Controls.Add(lblRate);
            detailsY += 60;

            // Rent button
            var btnRent = new Button
            {
                Text = "Rent",
                Location = new Point(detailsX, detailsY),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRent.FlatAppearance.BorderSize = 0;
            btnRent.Click += (s, e) => SelectVehicleAndCreateReservation(vehicle);
            card.Controls.Add(btnRent);

            return card;
        }

        private void AddDetailLabel(Panel card, string title, string value, int x, int y)
        {
            var lblTitle = new Label
            {
                Text = title,
                Location = new Point(x, y),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(102, 102, 102)
            };
            card.Controls.Add(lblTitle);

            var lblValue = new Label
            {
                Text = value,
                Location = new Point(x + 105, y),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            card.Controls.Add(lblValue);
        }

        private void SelectVehicleAndCreateReservation(Vehicle vehicle)
        {
            selectedVehicle = vehicle;
            BtnCreate_Click(null, EventArgs.Empty);
        }

        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (selectedVehicle == null)
            {
                MessageBox.Show("Please select a vehicle by clicking the 'Rent' button.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                int customerId = (int)cmbCustomer.SelectedValue;
                int vehicleId = selectedVehicle.VehicleId;
                
                var reservation = _reservationManager.CreateReservation(customerId, vehicleId, dtpStart.Value, dtpEnd.Value);
                MessageBox.Show($"Reservation created successfully for {selectedVehicle.Make} {selectedVehicle.Model}.\nReservation ID: {reservation.ReservationId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
