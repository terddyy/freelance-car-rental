using CarRentalSystem.Data.Repositories;
using CarRentalSystem.Models;

namespace CarRentalSystem.Forms
{
    /// <summary>
    /// Form for adding or editing a vehicle.
    /// </summary>
    public partial class VehicleEditForm : Form
    {
        private readonly VehicleRepository _vehicleRepo;
        private readonly Vehicle _vehicle;
        private readonly bool _isNew;
        private string? _selectedImagePath;

        public VehicleEditForm() : this(null)
        {
        }

        public VehicleEditForm(Vehicle? vehicle)
        {
            InitializeComponent();
            _vehicleRepo = new VehicleRepository();
            _vehicle = vehicle ?? new Vehicle();
            _isNew = vehicle == null;
            _selectedImagePath = _vehicle.ImagePath;

            this.Text = _isNew ? "Add Vehicle" : "Edit Vehicle";
            LoadData();
        }

        private void LoadData()
        {
            txtMake.Text = _vehicle.Make;
            txtModel.Text = _vehicle.Model;
            txtType.Text = _vehicle.Type;
            txtColor.Text = _vehicle.Color;
            txtTransmission.Text = _vehicle.Transmission;
            txtGasType.Text = _vehicle.GasType;
            
            // Ensure SeatCapacity is within valid range (minimum is 1)
            numSeatCapacity.Value = _vehicle.SeatCapacity > 0 ? _vehicle.SeatCapacity : 5;
            
            txtLocation.Text = _vehicle.Location;
            txtRegistration.Text = _vehicle.RegistrationPlate;
            numDailyRate.Value = _vehicle.DailyRate;
            cmbStatus.SelectedItem = _vehicle.Status;

            if (cmbStatus.SelectedItem == null)
            {
                cmbStatus.SelectedIndex = 0;
            }

            // Load image if exists
            if (!string.IsNullOrEmpty(_vehicle.ImagePath) && File.Exists(_vehicle.ImagePath))
            {
                try
                {
                    pictureBoxVehicle.Image = Image.FromFile(_vehicle.ImagePath);
                    pictureBoxVehicle.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch { }
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                _vehicle.Make = txtMake.Text.Trim();
                _vehicle.Model = txtModel.Text.Trim();
                _vehicle.Type = txtType.Text.Trim();
                _vehicle.Color = txtColor.Text.Trim();
                _vehicle.Transmission = txtTransmission.Text.Trim();
                _vehicle.GasType = txtGasType.Text.Trim();
                _vehicle.SeatCapacity = (int)numSeatCapacity.Value;
                _vehicle.Location = txtLocation.Text.Trim();
                _vehicle.RegistrationPlate = txtRegistration.Text.Trim();
                _vehicle.DailyRate = numDailyRate.Value;
                _vehicle.Status = cmbStatus.SelectedItem?.ToString() ?? "Available";
                _vehicle.ImagePath = _selectedImagePath;

                if (_isNew)
                {
                    _vehicleRepo.Insert(_vehicle);
                    MessageBox.Show("Vehicle added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _vehicleRepo.Update(_vehicle);
                    MessageBox.Show("Vehicle updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMake.Text))
            {
                MessageBox.Show("Please enter vehicle make.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMake.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Please enter vehicle model.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtModel.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Please enter vehicle type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRegistration.Text))
            {
                MessageBox.Show("Please enter registration plate.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRegistration.Focus();
                return false;
            }

            if (numDailyRate.Value <= 0)
            {
                MessageBox.Show("Please enter a valid daily rate.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numDailyRate.Focus();
                return false;
            }

            return true;
        }

        private void BtnBrowseImage_Click(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Select Vehicle Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Copy image to application data folder
                    string appDataPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "CarRentalSystem", "VehicleImages");
                    
                    if (!Directory.Exists(appDataPath))
                    {
                        Directory.CreateDirectory(appDataPath);
                    }

                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(openFileDialog.FileName)}";
                    string destPath = Path.Combine(appDataPath, fileName);

                    File.Copy(openFileDialog.FileName, destPath, true);
                    _selectedImagePath = destPath;

                    // Display image
                    pictureBoxVehicle.Image = Image.FromFile(destPath);
                    pictureBoxVehicle.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
