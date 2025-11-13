using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Forms
{
    public partial class CustomerManagementControl : UserControl
    {
        private readonly CustomerRepository _customerRepo;
        private DataGridView customerDataGridView;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh;
        private Label titleLabel;

        public CustomerManagementControl()
        {
            _customerRepo = new CustomerRepository();
            InitializeComponents();
            LoadCustomers();
        }

        private void InitializeComponents()
        {
            this.titleLabel = new Label 
            { 
                Text = "👥 Customer Management", 
                Dock = DockStyle.Top, 
                Font = new Font("Segoe UI", 18F, FontStyle.Bold), 
                Height = 70, 
                Padding = new Padding(25, 20, 10, 10), 
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(45, 52, 67)
            };
            
            var buttonPanel = new Panel { Dock = DockStyle.Top, Height = 70, Padding = new Padding(20, 15, 20, 15), BackColor = Color.White };
            
            this.btnAdd = new Button 
            { 
                Text = "➕ Add Customer", 
                Location = new Point(25, 18), 
                Size = new Size(150, 38),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 167, 69),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            this.btnAdd.FlatAppearance.BorderSize = 0;
            
            this.btnEdit = new Button 
            { 
                Text = "✏️ Edit", 
                Location = new Point(185, 18), 
                Size = new Size(120, 38),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 123, 255),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            this.btnEdit.FlatAppearance.BorderSize = 0;
            
            this.btnDelete = new Button 
            { 
                Text = "🗑️ Delete", 
                Location = new Point(315, 18), 
                Size = new Size(120, 38),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(220, 53, 69),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            this.btnDelete.FlatAppearance.BorderSize = 0;
            
            this.btnRefresh = new Button 
            { 
                Text = "🔄 Refresh", 
                Location = new Point(445, 18), 
                Size = new Size(120, 38),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(45, 52, 67),
                BackColor = Color.FromArgb(233, 236, 239),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            
            buttonPanel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });
            
            this.customerDataGridView = new DataGridView 
            { 
                Dock = DockStyle.Fill, 
                ReadOnly = true, 
                AllowUserToAddRows = false, 
                AllowUserToDeleteRows = false, 
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, 
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeight = 45,
                RowTemplate = { Height = 40 }
            };
            
            customerDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            customerDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            customerDataGridView.DefaultCellStyle.Padding = new Padding(8);
            customerDataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            customerDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            customerDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(73, 80, 87);
            customerDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            customerDataGridView.ColumnHeadersDefaultCellStyle.Padding = new Padding(10);
            customerDataGridView.EnableHeadersVisualStyles = false;
            customerDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 252, 253);
            
            this.Controls.AddRange(new Control[] { customerDataGridView, buttonPanel, titleLabel });
            
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += (s, e) => LoadCustomers();
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerRepo.GetAll();
                customerDataGridView.DataSource = customers;
                if (customerDataGridView.Columns["CustomerId"] != null) customerDataGridView.Columns["CustomerId"].Visible = false;
                if (customerDataGridView.Columns["DisplayName"] != null) customerDataGridView.Columns["DisplayName"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            using var form = new CustomerEditForm();
            if (form.ShowDialog() == DialogResult.OK) LoadCustomers();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (customerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var customer = customerDataGridView.SelectedRows[0].DataBoundItem as Customer;
            if (customer != null)
            {
                using var form = new CustomerEditForm(customer);
                if (form.ShowDialog() == DialogResult.OK) LoadCustomers();
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (customerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var customer = customerDataGridView.SelectedRows[0].DataBoundItem as Customer;
            if (customer != null && MessageBox.Show($"Delete {customer.Name}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _customerRepo.Delete(customer.CustomerId);
                    LoadCustomers();
                    MessageBox.Show("Customer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
