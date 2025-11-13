using CarRentalSystem.Business;
using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Forms
{
    public partial class ReservationManagementControl : UserControl
    {
        private readonly ReservationManager _reservationManager;
        private DataGridView reservationDataGridView;
        private Button btnNew, btnConfirm, btnCancel, btnRefresh, btnCheckOut;

        public ReservationManagementControl()
        {
            _reservationManager = new ReservationManager();
            InitializeComponents();
            LoadReservations();
        }

        private void InitializeComponents()
        {
            var titleLabel = new Label 
            { 
                Text = "📅 Reservation Management", 
                Dock = DockStyle.Top, 
                Font = new Font("Segoe UI", 18F, FontStyle.Bold), 
                Height = 70, 
                Padding = new Padding(25, 20, 10, 10), 
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(45, 52, 67)
            };
            
            var buttonPanel = new Panel { Dock = DockStyle.Top, Height = 70, Padding = new Padding(20, 15, 20, 15), BackColor = Color.White };
            
            btnNew = CreateModernButton("➕ New Reservation", new Point(25, 18), new Size(160, 38), Color.FromArgb(40, 167, 69));
            btnConfirm = CreateModernButton("✅ Confirm", new Point(195, 18), new Size(120, 38), Color.FromArgb(0, 123, 255));
            btnCancel = CreateModernButton("❌ Cancel", new Point(325, 18), new Size(120, 38), Color.FromArgb(220, 53, 69));
            btnCheckOut = CreateModernButton("🔑 Check-Out", new Point(455, 18), new Size(130, 38), Color.FromArgb(255, 193, 7), Color.FromArgb(52, 58, 64));
            btnRefresh = CreateModernButton("🔄 Refresh", new Point(595, 18), new Size(120, 38), Color.FromArgb(233, 236, 239), Color.FromArgb(45, 52, 67));
            
            buttonPanel.Controls.AddRange(new Control[] { btnNew, btnConfirm, btnCancel, btnCheckOut, btnRefresh });
            
            reservationDataGridView = CreateModernDataGridView();
            
            this.Controls.AddRange(new Control[] { reservationDataGridView, buttonPanel, titleLabel });
            
            btnNew.Click += BtnNew_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnCheckOut.Click += BtnCheckOut_Click;
            btnRefresh.Click += (s, e) => LoadReservations();
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

        private void LoadReservations()
        {
            try
            {
                var reservations = _reservationManager.GetAllReservationsWithDetails();
                var displayData = reservations.Select(r => new
                {
                    r.ReservationId,
                    CustomerName = r.Customer?.Name ?? "N/A",
                    VehicleInfo = r.Vehicle?.DisplayName ?? "N/A",
                    StartDate = r.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = r.EndDate.ToString("yyyy-MM-dd"),
                    r.Status,
                    Created = r.DateCreated.ToString("yyyy-MM-dd HH:mm")
                }).ToList();
                
                reservationDataGridView.DataSource = displayData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reservations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNew_Click(object? sender, EventArgs e)
        {
            using var form = new ReservationCreateForm();
            if (form.ShowDialog() == DialogResult.OK) LoadReservations();
        }

        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            var reservationId = GetSelectedReservationId();
            if (reservationId == null) return;
            
            try
            {
                _reservationManager.ConfirmReservation(reservationId.Value);
                MessageBox.Show("Reservation confirmed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error confirming reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            var reservationId = GetSelectedReservationId();
            if (reservationId == null) return;
            
            if (MessageBox.Show("Cancel this reservation?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _reservationManager.CancelReservation(reservationId.Value);
                    MessageBox.Show("Reservation cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReservations();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error cancelling reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCheckOut_Click(object? sender, EventArgs e)
        {
            var reservationId = GetSelectedReservationId();
            if (reservationId == null) return;
            
            using var form = new CheckOutForm(reservationId.Value);
            if (form.ShowDialog() == DialogResult.OK) LoadReservations();
        }

        private int? GetSelectedReservationId()
        {
            if (reservationDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a reservation.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            
            var row = reservationDataGridView.SelectedRows[0];
            return row.Cells["ReservationId"].Value as int?;
        }
    }
}
