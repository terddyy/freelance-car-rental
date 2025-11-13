using CarRentalSystem.Business;

namespace CarRentalSystem.Forms
{
    public partial class CheckOutForm : Form
    {
        private readonly RentalManager _rentalManager;
        private readonly DocumentGenerator _docGenerator;
        private readonly int _reservationId;
        private NumericUpDown numDeposit;
        private TextBox txtTicket;

        public CheckOutForm(int reservationId)
        {
            _rentalManager = new RentalManager();
            _docGenerator = new DocumentGenerator();
            _reservationId = reservationId;
            
            this.Text = "Check-Out Vehicle";
            this.Size = new Size(700, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var titleLabel = new Label { Text = "Vehicle Check-Out", Location = new Point(20, 20), Size = new Size(200, 30), Font = new Font("Segoe UI", 14F, FontStyle.Bold) };
            
            var lblDeposit = new Label { Text = "Deposit Amount:", Location = new Point(20, 60), Size = new Size(120, 20) };
            numDeposit = new NumericUpDown { Location = new Point(150, 57), Size = new Size(200, 23), DecimalPlaces = 2, Maximum = 10000 };
            
            var lblTicket = new Label { Text = "Rental Ticket/Agreement:", Location = new Point(20, 100), Size = new Size(200, 20) };
            txtTicket = new TextBox { Location = new Point(20, 125), Size = new Size(640, 350), Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true, Font = new Font("Courier New", 9F) };
            
            var btnGenerate = new Button { Text = "Generate Ticket", Location = new Point(20, 490), Size = new Size(130, 30) };
            btnGenerate.Click += BtnGenerate_Click;
            
            var btnCheckOut = new Button { Text = "Complete Check-Out", Location = new Point(160, 490), Size = new Size(150, 30) };
            btnCheckOut.Click += BtnCheckOut_Click;
            
            var btnCancel = new Button { Text = "Cancel", Location = new Point(320, 490), Size = new Size(100, 30) };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            
            this.Controls.AddRange(new Control[] { titleLabel, lblDeposit, numDeposit, lblTicket, txtTicket, btnGenerate, btnCheckOut, btnCancel });
        }

        private void BtnGenerate_Click(object? sender, EventArgs e)
        {
            try
            {
                var reservation = new ReservationManager().GetReservationWithDetails(_reservationId);
                if (reservation == null || reservation.Customer == null || reservation.Vehicle == null)
                {
                    MessageBox.Show("Reservation details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                var tempRental = new Models.Rental
                {
                    RentalId = 0,
                    ReservationId = _reservationId,
                    CheckOutTime = DateTime.Now,
                    DepositPaid = numDeposit.Value
                };
                
                txtTicket.Text = _docGenerator.GenerateRentalTicket(tempRental, reservation, reservation.Customer, reservation.Vehicle);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCheckOut_Click(object? sender, EventArgs e)
        {
            try
            {
                var rental = _rentalManager.CheckOutVehicle(_reservationId, numDeposit.Value);
                MessageBox.Show($"Vehicle checked out successfully.\nRental ID: {rental.RentalId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking out vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
