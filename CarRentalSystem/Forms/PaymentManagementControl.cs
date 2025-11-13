using CarRentalSystem.Business;
using CarRentalSystem.Data.Repositories;

namespace CarRentalSystem.Forms
{
    public partial class PaymentManagementControl : UserControl
    {
        private readonly PaymentManager _paymentManager;
        private readonly RentalManager _rentalManager;
        private DataGridView paymentDataGridView;
        private Button btnNewPayment, btnViewRental, btnRefresh, btnGenerateReceipt;

        public PaymentManagementControl()
        {
            _paymentManager = new PaymentManager();
            _rentalManager = new RentalManager();
            InitializeComponents();
            LoadUnpaidRentals();
        }

        private void InitializeComponents()
        {
            var titleLabel = new Label 
            { 
                Text = "💳 Payment Management", 
                Dock = DockStyle.Top, 
                Font = new Font("Segoe UI", 18F, FontStyle.Bold), 
                Height = 70, 
                Padding = new Padding(25, 20, 10, 10), 
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(45, 52, 67)
            };
            
            var buttonPanel = new Panel { Dock = DockStyle.Top, Height = 70, Padding = new Padding(20, 15, 20, 15), BackColor = Color.White };
            
            btnNewPayment = CreateModernButton("💰 Process Payment", new Point(25, 18), new Size(160, 38), Color.FromArgb(40, 167, 69));
            btnViewRental = CreateModernButton("👁️ View Details", new Point(195, 18), new Size(150, 38), Color.FromArgb(0, 123, 255));
            btnGenerateReceipt = CreateModernButton("📄 Generate Receipt", new Point(355, 18), new Size(160, 38), Color.FromArgb(108, 117, 125));
            btnRefresh = CreateModernButton("🔄 Refresh", new Point(525, 18), new Size(120, 38), Color.FromArgb(233, 236, 239), Color.FromArgb(45, 52, 67));
            
            buttonPanel.Controls.AddRange(new Control[] { btnNewPayment, btnViewRental, btnGenerateReceipt, btnRefresh });
            
            paymentDataGridView = CreateModernDataGridView();
            
            this.Controls.AddRange(new Control[] { paymentDataGridView, buttonPanel, titleLabel });
            
            btnNewPayment.Click += BtnNewPayment_Click;
            btnViewRental.Click += BtnViewRental_Click;
            btnGenerateReceipt.Click += BtnGenerateReceipt_Click;
            btnRefresh.Click += (s, e) => LoadUnpaidRentals();
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

        private void LoadUnpaidRentals()
        {
            try
            {
                var rentals = _rentalManager.GetUnpaidRentalsWithDetails();
                var displayData = rentals.Select(r => new
                {
                    r.RentalId,
                    CustomerName = r.Reservation?.Customer?.Name ?? "N/A",
                    VehicleInfo = r.Reservation?.Vehicle?.DisplayName ?? "N/A",
                    CheckOut = r.CheckOutTime.ToString("yyyy-MM-dd"),
                    CheckIn = r.CheckInTime?.ToString("yyyy-MM-dd") ?? "Active",
                    TotalFee = $"${r.TotalFee:F2}",
                    Penalties = $"${r.Penalties:F2}",
                    Deposit = $"${r.DepositPaid:F2}",
                    TotalDue = $"${(r.TotalFee + r.Penalties - r.DepositPaid):F2}",
                    TotalPaid = $"${_paymentManager.GetTotalPaidForRental(r.RentalId):F2}",
                    Balance = $"${_paymentManager.GetOutstandingBalance(r.RentalId):F2}",
                    Status = r.IsPaid ? "Paid" : "Unpaid"
                }).ToList();
                
                paymentDataGridView.DataSource = displayData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rentals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNewPayment_Click(object? sender, EventArgs e)
        {
            var rentalId = GetSelectedRentalId();
            if (rentalId == null) return;
            
            using var form = new PaymentForm(rentalId.Value);
            if (form.ShowDialog() == DialogResult.OK) LoadUnpaidRentals();
        }

        private void BtnViewRental_Click(object? sender, EventArgs e)
        {
            var rentalId = GetSelectedRentalId();
            if (rentalId == null) return;
            
            try
            {
                var rental = _rentalManager.GetRentalWithDetails(rentalId.Value);
                if (rental == null) return;
                
                string details = $"Rental ID: {rental.RentalId}\n" +
                               $"Customer: {rental.Reservation?.Customer?.Name}\n" +
                               $"Vehicle: {rental.Reservation?.Vehicle?.DisplayName}\n" +
                               $"Check-Out: {rental.CheckOutTime:yyyy-MM-dd HH:mm}\n" +
                               $"Check-In: {(rental.CheckInTime.HasValue ? rental.CheckInTime.Value.ToString("yyyy-MM-dd HH:mm") : "Not returned")}\n\n" +
                               $"Rental Fee: ${rental.TotalFee:F2}\n" +
                               $"Penalties: ${rental.Penalties:F2}\n" +
                               $"Deposit: -${rental.DepositPaid:F2}\n" +
                               $"Total Due: ${(rental.TotalFee + rental.Penalties - rental.DepositPaid):F2}\n\n" +
                               $"Total Paid: ${_paymentManager.GetTotalPaidForRental(rental.RentalId):F2}\n" +
                               $"Balance: ${_paymentManager.GetOutstandingBalance(rental.RentalId):F2}";
                
                MessageBox.Show(details, "Rental Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing rental: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGenerateReceipt_Click(object? sender, EventArgs e)
        {
            var rentalId = GetSelectedRentalId();
            if (rentalId == null) return;
            
            using var form = new ReceiptForm(rentalId.Value);
            form.ShowDialog();
        }

        private int? GetSelectedRentalId()
        {
            if (paymentDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rental.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            
            return paymentDataGridView.SelectedRows[0].Cells["RentalId"].Value as int?;
        }
    }
}
