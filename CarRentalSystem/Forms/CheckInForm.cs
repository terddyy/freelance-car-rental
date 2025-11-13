using CarRentalSystem.Business;

namespace CarRentalSystem.Forms
{
    public partial class CheckInForm : Form
    {
        private readonly RentalManager _rentalManager;
        private readonly int _rentalId;
        private TextBox txtSummary;
        private Label lblTotalFee, lblPenalties, lblDeposit, lblBalance;

        public CheckInForm(int rentalId)
        {
            _rentalManager = new RentalManager();
            _rentalId = rentalId;
            
            this.Text = "Check-In Vehicle";
            this.Size = new Size(600, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            
            InitializeComponents();
            LoadRentalInfo();
        }

        private void InitializeComponents()
        {
            var titleLabel = new Label { Text = "Vehicle Check-In", Location = new Point(20, 20), Size = new Size(200, 30), Font = new Font("Segoe UI", 14F, FontStyle.Bold) };
            
            txtSummary = new TextBox { Location = new Point(20, 60), Size = new Size(540, 200), Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true };
            
            int y = 280;
            AddLabelPair("Total Rental Fee:", ref lblTotalFee, ref y);
            AddLabelPair("Late Penalties:", ref lblPenalties, ref y);
            AddLabelPair("Deposit Applied:", ref lblDeposit, ref y);
            AddLabelPair("Balance Due:", ref lblBalance, ref y);
            
            var btnCheckIn = new Button { Text = "Complete Check-In", Location = new Point(150, y + 20), Size = new Size(150, 30) };
            btnCheckIn.Click += BtnCheckIn_Click;
            
            var btnCancel = new Button { Text = "Cancel", Location = new Point(310, y + 20), Size = new Size(100, 30) };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            
            this.Controls.AddRange(new Control[] { titleLabel, txtSummary, btnCheckIn, btnCancel });
        }

        private void AddLabelPair(string labelText, ref Label valueLabel, ref int y)
        {
            var label = new Label { Text = labelText, Location = new Point(20, y), Size = new Size(150, 20) };
            valueLabel = new Label { Text = "$0.00", Location = new Point(180, y), Size = new Size(150, 20), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.Controls.AddRange(new Control[] { label, valueLabel });
            y += 30;
        }

        private void LoadRentalInfo()
        {
            try
            {
                var rental = _rentalManager.GetRentalWithDetails(_rentalId);
                if (rental == null || rental.Reservation == null)
                {
                    MessageBox.Show("Rental not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                
                var customer = rental.Reservation.Customer;
                var vehicle = rental.Reservation.Vehicle;
                
                txtSummary.Text = $"Customer: {customer?.Name}\r\n" +
                                 $"Vehicle: {vehicle?.DisplayName}\r\n" +
                                 $"Check-Out: {rental.CheckOutTime:yyyy-MM-dd HH:mm}\r\n" +
                                 $"Expected Return: {rental.Reservation.EndDate:yyyy-MM-dd}\r\n" +
                                 $"Current Time: {DateTime.Now:yyyy-MM-dd HH:mm}\r\n\r\n" +
                                 $"Daily Rate: ${vehicle?.DailyRate:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rental: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCheckIn_Click(object? sender, EventArgs e)
        {
            try
            {
                var rental = _rentalManager.CheckInVehicle(_rentalId);
                
                lblTotalFee.Text = $"${rental.TotalFee:F2}";
                lblPenalties.Text = $"${rental.Penalties:F2}";
                lblDeposit.Text = $"-${rental.DepositPaid:F2}";
                
                decimal balance = rental.TotalFee + rental.Penalties - rental.DepositPaid;
                lblBalance.Text = $"${balance:F2}";
                lblBalance.ForeColor = balance > 0 ? Color.Red : Color.Green;
                
                MessageBox.Show($"Vehicle checked in successfully.\n\nTotal Due: ${balance:F2}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking in vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
