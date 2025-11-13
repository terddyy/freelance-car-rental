using CarRentalSystem.Business;
using CarRentalSystem.Data.Repositories;

namespace CarRentalSystem.Forms
{
    public partial class ReceiptForm : Form
    {
        private readonly DocumentGenerator _docGenerator;
        private readonly PaymentManager _paymentManager;
        private readonly RentalManager _rentalManager;
        private readonly int _rentalId;
        private TextBox txtReceipt;
        private ComboBox cmbPayment;

        public ReceiptForm(int rentalId)
        {
            _docGenerator = new DocumentGenerator();
            _paymentManager = new PaymentManager();
            _rentalManager = new RentalManager();
            _rentalId = rentalId;
            
            this.Text = "Payment Receipt";
            this.Size = new Size(700, 650);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            
            InitializeComponents();
            LoadPayments();
        }

        private void InitializeComponents()
        {
            var titleLabel = new Label { Text = "Payment Receipt", Location = new Point(20, 20), Size = new Size(200, 30), Font = new Font("Segoe UI", 14F, FontStyle.Bold) };
            
            var lblPayment = new Label { Text = "Select Payment:", Location = new Point(20, 60), Size = new Size(120, 20) };
            cmbPayment = new ComboBox { Location = new Point(150, 57), Size = new Size(400, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbPayment.SelectedIndexChanged += (s, e) => GenerateReceipt();
            
            txtReceipt = new TextBox 
            { 
                Location = new Point(20, 100), 
                Size = new Size(640, 450), 
                Multiline = true, 
                ReadOnly = true, 
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Courier New", 9F)
            };
            
            var btnPrint = new Button { Text = "Print/Save", Location = new Point(150, 565), Size = new Size(120, 30) };
            btnPrint.Click += BtnPrint_Click;
            
            var btnClose = new Button { Text = "Close", Location = new Point(280, 565), Size = new Size(100, 30) };
            btnClose.Click += (s, e) => this.Close();
            
            this.Controls.AddRange(new Control[] { titleLabel, lblPayment, cmbPayment, txtReceipt, btnPrint, btnClose });
        }

        private void LoadPayments()
        {
            try
            {
                var payments = _paymentManager.GetPaymentHistory(_rentalId);
                
                var paymentItems = payments.Select(p => new
                {
                    Display = $"Payment #{p.PaymentId} - ${p.Amount:F2} ({p.PaymentType}) - {p.TransactionDate:yyyy-MM-dd HH:mm}",
                    Payment = p
                }).ToList();
                
                cmbPayment.DisplayMember = "Display";
                cmbPayment.ValueMember = "Payment";
                cmbPayment.DataSource = paymentItems;
                
                if (paymentItems.Count > 0)
                {
                    cmbPayment.SelectedIndex = paymentItems.Count - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateReceipt()
        {
            try
            {
                if (cmbPayment.SelectedValue == null) return;
                
                var payment = (Models.Payment)cmbPayment.SelectedValue;
                var rental = _rentalManager.GetRentalWithDetails(_rentalId);
                
                if (rental == null || rental.Reservation == null || rental.Reservation.Customer == null || rental.Reservation.Vehicle == null)
                {
                    MessageBox.Show("Unable to load rental details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                txtReceipt.Text = _docGenerator.GeneratePaymentReceipt(
                    payment, 
                    rental, 
                    rental.Reservation, 
                    rental.Reservation.Customer, 
                    rental.Reservation.Vehicle);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            using var saveDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                FileName = $"Receipt_{_rentalId}_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            };
            
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveDialog.FileName, txtReceipt.Text);
                    MessageBox.Show("Receipt saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
