using CarRentalSystem.Business;
using CarRentalSystem.Data.Repositories;

namespace CarRentalSystem.Forms
{
    public partial class RentalManagementControl : UserControl
    {
        private readonly RentalManager _rentalManager;
        private DataGridView rentalDataGridView;
        private Button btnCheckIn, btnRefresh, btnViewActive;

        public RentalManagementControl()
        {
            _rentalManager = new RentalManager();
            InitializeComponents();
            LoadRentals();
        }

        private void InitializeComponents()
        {
            var titleLabel = new Label 
            { 
                Text = "🔑 Rental Management", 
                Dock = DockStyle.Top, 
                Font = new Font("Segoe UI", 18F, FontStyle.Bold), 
                Height = 70, 
                Padding = new Padding(25, 20, 10, 10), 
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(45, 52, 67)
            };
            
            var buttonPanel = new Panel { Dock = DockStyle.Top, Height = 70, Padding = new Padding(20, 15, 20, 15), BackColor = Color.White };
            
            btnViewActive = CreateModernButton("👁️ View Active", new Point(25, 18), new Size(140, 38), Color.FromArgb(0, 123, 255));
            btnCheckIn = CreateModernButton("✅ Check-In Vehicle", new Point(175, 18), new Size(160, 38), Color.FromArgb(40, 167, 69));
            btnRefresh = CreateModernButton("🔄 Refresh", new Point(345, 18), new Size(120, 38), Color.FromArgb(233, 236, 239), Color.FromArgb(45, 52, 67));
            
            buttonPanel.Controls.AddRange(new Control[] { btnViewActive, btnCheckIn, btnRefresh });
            
            rentalDataGridView = CreateModernDataGridView();
            
            this.Controls.AddRange(new Control[] { rentalDataGridView, buttonPanel, titleLabel });
            
            btnViewActive.Click += (s, e) => LoadActiveRentals();
            btnCheckIn.Click += BtnCheckIn_Click;
            btnRefresh.Click += (s, e) => LoadRentals();
        }
        
        private Button CreateModernButton(string text, Point location, Size size, Color backColor, Color? foreColor = null)
        {
            var btn = new Button
            {
                Text = text,
                Location = location,
                Size = size,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = foreColor ?? Color.White,
                BackColor = backColor,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
        
        private DataGridView CreateModernDataGridView()
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeight = 45,
                RowTemplate = { Height = 40 }
            };
            
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding = new Padding(8);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(73, 80, 87);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 252, 253);
            
            return dgv;
        }

        private void LoadRentals()
        {
            try
            {
                var rentals = _rentalManager.GetActiveRentalsWithDetails();
                DisplayRentals(rentals);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rentals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadActiveRentals()
        {
            try
            {
                var rentals = _rentalManager.GetActiveRentalsWithDetails();
                DisplayRentals(rentals);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading active rentals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayRentals(List<Models.Rental> rentals)
        {
            var displayData = rentals.Select(r => new
            {
                r.RentalId,
                CustomerName = r.Reservation?.Customer?.Name ?? "N/A",
                VehicleInfo = r.Reservation?.Vehicle?.DisplayName ?? "N/A",
                CheckOut = r.CheckOutTime.ToString("yyyy-MM-dd HH:mm"),
                CheckIn = r.CheckInTime?.ToString("yyyy-MM-dd HH:mm") ?? "Active",
                TotalFee = $"${r.TotalFee:F2}",
                Penalties = $"${r.Penalties:F2}",
                Deposit = $"${r.DepositPaid:F2}",
                Status = r.IsPaid ? "Paid" : "Unpaid"
            }).ToList();
            
            rentalDataGridView.DataSource = displayData;
        }

        private void BtnCheckIn_Click(object? sender, EventArgs e)
        {
            if (rentalDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rental to check-in.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var rentalId = rentalDataGridView.SelectedRows[0].Cells["RentalId"].Value as int?;
            if (rentalId == null) return;
            
            using var form = new CheckInForm(rentalId.Value);
            if (form.ShowDialog() == DialogResult.OK) LoadRentals();
        }
    }
}
