using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Forms
{
    /// <summary>
    /// User control for managing vehicles.
    /// </summary>
    public partial class VehicleManagementControl : UserControl
    {
        private readonly VehicleRepository _vehicleRepo;

        public VehicleManagementControl()
        {
            InitializeComponent();
            _vehicleRepo = new VehicleRepository();
            LoadVehicles();
        }

        private void LoadVehicles()
        {
            try
            {
                var vehicles = _vehicleRepo.GetAll();
                vehicleDataGridView.DataSource = vehicles;

                // Hide VehicleId column
                if (vehicleDataGridView.Columns["VehicleId"] != null)
                {
                    vehicleDataGridView.Columns["VehicleId"].Visible = false;
                }

                // Hide DisplayName column
                if (vehicleDataGridView.Columns["DisplayName"] != null)
                {
                    vehicleDataGridView.Columns["DisplayName"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            using var form = new VehicleEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadVehicles();
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (vehicleDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a vehicle to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var vehicle = vehicleDataGridView.SelectedRows[0].DataBoundItem as Vehicle;
            if (vehicle != null)
            {
                using var form = new VehicleEditForm(vehicle);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadVehicles();
                }
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (vehicleDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a vehicle to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var vehicle = vehicleDataGridView.SelectedRows[0].DataBoundItem as Vehicle;
            if (vehicle != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete {vehicle.DisplayName}?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _vehicleRepo.Delete(vehicle.VehicleId);
                        LoadVehicles();
                        MessageBox.Show("Vehicle deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadVehicles();
        }
    }
}
