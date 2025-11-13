using CarRentalSystem.Business;

namespace CarRentalSystem.Forms
{
    public partial class PaymentForm : Form
    {
        private readonly PaymentManager _paymentManager;
        private readonly RentalManager _rentalManager;
        private readonly int _rentalId;
        private NumericUpDown numAmount;
        private ComboBox cmbPaymentType;
        private TextBox txtNotes, txtRentalInfo;

        public PaymentForm(int rentalId)
        {
            _paymentManager = new PaymentManager();
            _rentalManager = new RentalManager();
            _rentalId = rentalId;
            
            this.Text = "Process Payment";
            this.Size = new Size(600, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            
            InitializeComponents();
            LoadRentalInfo();
        }

        private void InitializeComponents()
        {
            var titleLabel = new Label { Text = "Process Payment", Location = new Point(20, 20), Size = new Size(200, 30), Font = new Font("Segoe UI", 14F, FontStyle.Bold) };
            
            var lblRentalInfo = new Label { Text = "Rental Information:", Location = new Point(20, 60), Size = new Size(150, 20) };
            txtRentalInfo = new TextBox { Location = new Point(20, 85), Size = new Size(540, 150), Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
            
            var lblAmount = new Label { Text = "Payment Amount:", Location = new Point(20, 255), Size = new Size(120, 20) };
            numAmount = new NumericUpDown { Location = new Point(150, 252), Size = new Size(200, 23), DecimalPlaces = 2, Maximum = 100000 };
            
            var lblType = new Label { Text = "Payment Type:", Location = new Point(20, 290), Size = new Size(120, 20) };
            cmbPaymentType = new ComboBox { Location = new Point(150, 287), Size = new Size(200, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbPaymentType.DataSource = _paymentManager.GetPaymentTypes();
            
            var lblNotes = new Label { Text = "Notes (Optional):", Location = new Point(20, 325), Size = new Size(120, 20) };
            txtNotes = new TextBox { Location = new Point(150, 322), Size = new Size(410, 60), Multiline = true };
            
            var btnProcess = new Button { Text = "Process Payment", Location = new Point(150, 400), Size = new Size(140, 30) };
            btnProcess.Click += BtnProcess_Click;
            
            var btnCancel = new Button { Text = "Cancel", Location = new Point(300, 400), Size = new Size(100, 30) };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            
            this.Controls.AddRange(new Control[] { titleLabel, lblRentalInfo, txtRentalInfo, lblAmount, numAmount, lblType, cmbPaymentType, lblNotes, txtNotes, btnProcess, btnCancel });
        }

        private void LoadRentalInfo()
        {
            try
            {
                var rental = _rentalManager.GetRentalWithDetails(_rentalId);
                if (rental == null)
                {
                    MessageBox.Show("Rental not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                
                decimal totalDue = rental.TotalFee + rental.Penalties - rental.DepositPaid;
                decimal totalPaid = _paymentManager.GetTotalPaidForRental(_rentalId);
                decimal balance = _paymentManager.GetOutstandingBalance(_rentalId);
                
                txtRentalInfo.Text = $"Customer: {rental.Reservation?.Customer?.Name}\r\n" +
                                    $"Vehicle: {rental.Reservation?.Vehicle?.DisplayName}\r\n\r\n" +
                                    $"Rental Fee: ${rental.TotalFee:F2}\r\n" +
                                    $"Late Penalties: ${rental.Penalties:F2}\r\n" +
                                    $"Deposit Applied: -${rental.DepositPaid:F2}\r\n" +
                                    $"Total Due: ${totalDue:F2}\r\n\r\n" +
                                    $"Total Paid: ${totalPaid:F2}\r\n" +
                                    $"Outstanding Balance: ${balance:F2}";
                
                numAmount.Value = balance;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rental: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnProcess_Click(object? sender, EventArgs e)
        {
            if (numAmount.Value <= 0)
            {
                MessageBox.Show("Payment amount must be greater than zero.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (cmbPaymentType.SelectedItem == null)
            {
                MessageBox.Show("Please select a payment type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                string paymentType = cmbPaymentType.SelectedItem.ToString() ?? "Cash";
                string? notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim();
                
                var payment = _paymentManager.ProcessPayment(_rentalId, numAmount.Value, paymentType, notes);
                
                decimal balance = _paymentManager.GetOutstandingBalance(_rentalId);
                string message = $"Payment processed successfully.\n\nPayment ID: {payment.PaymentId}\nAmount: ${payment.Amount:F2}\nRemaining Balance: ${balance:F2}";
                
                if (balance <= 0)
                {
                    message += "\n\nRental is now fully paid!";
                }
                
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
