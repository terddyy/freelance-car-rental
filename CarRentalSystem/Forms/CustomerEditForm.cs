using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Forms
{
    public partial class CustomerEditForm : Form
    {
        private readonly CustomerRepository _customerRepo;
        private readonly Customer _customer;
        private readonly bool _isNew;
        private TextBox txtName, txtContact, txtLicense, txtEmail;

        public CustomerEditForm() : this(null) { }

        public CustomerEditForm(Customer? customer)
        {
            _customerRepo = new CustomerRepository();
            _customer = customer ?? new Customer();
            _isNew = customer == null;
            this.Text = _isNew ? "Add Customer" : "Edit Customer";
            this.Size = new Size(550, 380);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.Font = new Font("Segoe UI", 9F);
            
            InitializeComponents();
            LoadData();
        }

        private void InitializeComponents()
        {
            int y = 30;
            AddField("Name:", ref txtName, ref y);
            AddField("Contact Info:", ref txtContact, ref y);
            AddField("License Number:", ref txtLicense, ref y);
            AddField("Email:", ref txtEmail, ref y);
            
            var btnSave = new Button 
            { 
                Text = "💾 Save", 
                Location = new Point(180, y + 20), 
                Size = new Size(130, 40),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 167, 69),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            
            var btnCancel = new Button 
            { 
                Text = "❌ Cancel", 
                Location = new Point(320, y + 20), 
                Size = new Size(130, 40),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(73, 80, 87),
                BackColor = Color.FromArgb(233, 236, 239),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            
            this.Controls.AddRange(new Control[] { btnSave, btnCancel });
        }

        private void AddField(string labelText, ref TextBox textBox, ref int yPos)
        {
            var label = new Label 
            { 
                Text = labelText, 
                Location = new Point(30, yPos), 
                Size = new Size(130, 22), 
                AutoSize = false,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            textBox = new TextBox 
            { 
                Location = new Point(180, yPos - 3), 
                Size = new Size(310, 27),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.AddRange(new Control[] { label, textBox });
            yPos += 50;
        }

        private void LoadData()
        {
            txtName.Text = _customer.Name;
            txtContact.Text = _customer.ContactInfo;
            txtLicense.Text = _customer.LicenseNumber;
            txtEmail.Text = _customer.Email;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            
            try
            {
                _customer.Name = txtName.Text.Trim();
                _customer.ContactInfo = txtContact.Text.Trim();
                _customer.LicenseNumber = txtLicense.Text.Trim();
                _customer.Email = txtEmail.Text.Trim();
                
                if (_isNew)
                {
                    _customer.DateCreated = DateTime.Now;
                    _customerRepo.Insert(_customer);
                }
                else
                {
                    _customerRepo.Update(_customer);
                }
                
                MessageBox.Show($"Customer {(_isNew ? "added" : "updated")} successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Please enter customer name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (string.IsNullOrWhiteSpace(txtContact.Text)) { MessageBox.Show("Please enter contact info.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (string.IsNullOrWhiteSpace(txtLicense.Text)) { MessageBox.Show("Please enter license number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            return true;
        }
    }
}
