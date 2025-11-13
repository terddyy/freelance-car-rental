namespace CarRentalSystem.Forms
{
    partial class VehicleEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMake;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblTransmission;
        private System.Windows.Forms.Label lblGasType;
        private System.Windows.Forms.Label lblSeatCapacity;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblRegistration;
        private System.Windows.Forms.Label lblDailyRate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtMake;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.TextBox txtTransmission;
        private System.Windows.Forms.TextBox txtGasType;
        private System.Windows.Forms.NumericUpDown numSeatCapacity;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtRegistration;
        private System.Windows.Forms.NumericUpDown numDailyRate;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.PictureBox pictureBoxVehicle;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMake = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblTransmission = new System.Windows.Forms.Label();
            this.lblGasType = new System.Windows.Forms.Label();
            this.lblSeatCapacity = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblRegistration = new System.Windows.Forms.Label();
            this.lblDailyRate = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.txtTransmission = new System.Windows.Forms.TextBox();
            this.txtGasType = new System.Windows.Forms.TextBox();
            this.numSeatCapacity = new System.Windows.Forms.NumericUpDown();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtRegistration = new System.Windows.Forms.TextBox();
            this.numDailyRate = new System.Windows.Forms.NumericUpDown();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.pictureBoxVehicle = new System.Windows.Forms.PictureBox();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numDailyRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeatCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVehicle)).BeginInit();
            this.SuspendLayout();
            
            int labelX = 20;
            int controlX = 160;
            int controlWidth = 280;
            int rowHeight = 45;
            int currentY = 20;
            
            // lblMake
            this.lblMake.AutoSize = false;
            this.lblMake.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblMake.Name = "lblMake";
            this.lblMake.Size = new System.Drawing.Size(130, 20);
            this.lblMake.TabIndex = 0;
            this.lblMake.Text = "Make:";
            this.lblMake.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblMake.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblMake.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // txtMake
            this.txtMake.Location = new System.Drawing.Point(controlX, currentY);
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(controlWidth, 27);
            this.txtMake.TabIndex = 1;
            this.txtMake.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMake.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            currentY += rowHeight;
            
            // lblModel
            this.lblModel.AutoSize = false;
            this.lblModel.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(130, 20);
            this.lblModel.TabIndex = 2;
            this.lblModel.Text = "Model:";
            this.lblModel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblModel.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // txtModel
            this.txtModel.Location = new System.Drawing.Point(controlX, currentY);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(controlWidth, 27);
            this.txtModel.TabIndex = 3;
            this.txtModel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            currentY += rowHeight;
            
            // lblType
            this.lblType.AutoSize = false;
            this.lblType.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(130, 20);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type:";
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // txtType
            this.txtType.Location = new System.Drawing.Point(controlX, currentY);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(controlWidth, 27);
            this.txtType.TabIndex = 5;
            this.txtType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            currentY += rowHeight;
            
            // lblColor
            this.lblColor.AutoSize = false;
            this.lblColor.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(130, 20);
            this.lblColor.TabIndex = 6;
            this.lblColor.Text = "Color:";
            this.lblColor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblColor.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // txtColor
            this.txtColor.Location = new System.Drawing.Point(controlX, currentY);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(controlWidth, 27);
            this.txtColor.TabIndex = 7;
            this.txtColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            currentY += rowHeight;
            
            // lblTransmission
            this.lblTransmission.AutoSize = false;
            this.lblTransmission.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblTransmission.Name = "lblTransmission";
            this.lblTransmission.Size = new System.Drawing.Size(130, 20);
            this.lblTransmission.TabIndex = 8;
            this.lblTransmission.Text = "Transmission:";
            this.lblTransmission.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblTransmission.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblTransmission.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // txtTransmission
            this.txtTransmission.Location = new System.Drawing.Point(controlX, currentY);
            this.txtTransmission.Name = "txtTransmission";
            this.txtTransmission.Size = new System.Drawing.Size(controlWidth, 27);
            this.txtTransmission.TabIndex = 9;
            this.txtTransmission.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTransmission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            currentY += rowHeight;
            
            // lblGasType
            this.lblGasType.AutoSize = false;
            this.lblGasType.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblGasType.Name = "lblGasType";
            this.lblGasType.Size = new System.Drawing.Size(130, 20);
            this.lblGasType.TabIndex = 10;
            this.lblGasType.Text = "Gas Type:";
            this.lblGasType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblGasType.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblGasType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // txtGasType
            this.txtGasType.Location = new System.Drawing.Point(controlX, currentY);
            this.txtGasType.Name = "txtGasType";
            this.txtGasType.Size = new System.Drawing.Size(controlWidth, 27);
            this.txtGasType.TabIndex = 11;
            this.txtGasType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGasType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            currentY += rowHeight;
            
            // lblSeatCapacity
            this.lblSeatCapacity.AutoSize = false;
            this.lblSeatCapacity.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblSeatCapacity.Name = "lblSeatCapacity";
            this.lblSeatCapacity.Size = new System.Drawing.Size(130, 20);
            this.lblSeatCapacity.TabIndex = 12;
            this.lblSeatCapacity.Text = "Seat Capacity:";
            this.lblSeatCapacity.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblSeatCapacity.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblSeatCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // numSeatCapacity
            this.numSeatCapacity.Location = new System.Drawing.Point(controlX, currentY);
            this.numSeatCapacity.Name = "numSeatCapacity";
            this.numSeatCapacity.Size = new System.Drawing.Size(controlWidth, 27);
            this.numSeatCapacity.TabIndex = 13;
            this.numSeatCapacity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSeatCapacity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numSeatCapacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSeatCapacity.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numSeatCapacity.Value = new decimal(new int[] { 5, 0, 0, 0 });
            currentY += rowHeight;
            
            // lblLocation
            this.lblLocation.AutoSize = false;
            this.lblLocation.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(130, 20);
            this.lblLocation.TabIndex = 14;
            this.lblLocation.Text = "Location:";
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblLocation.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // txtLocation
            this.txtLocation.Location = new System.Drawing.Point(controlX, currentY);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(controlWidth, 27);
            this.txtLocation.TabIndex = 15;
            this.txtLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            currentY += rowHeight;
            
            // lblRegistration
            this.lblRegistration.AutoSize = false;
            this.lblRegistration.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblRegistration.Name = "lblRegistration";
            this.lblRegistration.Size = new System.Drawing.Size(130, 20);
            this.lblRegistration.TabIndex = 16;
            this.lblRegistration.Text = "Registration Plate:";
            this.lblRegistration.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblRegistration.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // txtRegistration
            this.txtRegistration.Location = new System.Drawing.Point(controlX, currentY);
            this.txtRegistration.Name = "txtRegistration";
            this.txtRegistration.Size = new System.Drawing.Size(controlWidth, 27);
            this.txtRegistration.TabIndex = 17;
            this.txtRegistration.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRegistration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            currentY += rowHeight;
            
            // lblDailyRate
            this.lblDailyRate.AutoSize = false;
            this.lblDailyRate.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblDailyRate.Name = "lblDailyRate";
            this.lblDailyRate.Size = new System.Drawing.Size(130, 20);
            this.lblDailyRate.TabIndex = 18;
            this.lblDailyRate.Text = "Daily Rate:";
            this.lblDailyRate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblDailyRate.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblDailyRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // numDailyRate
            this.numDailyRate.DecimalPlaces = 2;
            this.numDailyRate.Location = new System.Drawing.Point(controlX, currentY);
            this.numDailyRate.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numDailyRate.Name = "numDailyRate";
            this.numDailyRate.Size = new System.Drawing.Size(controlWidth, 27);
            this.numDailyRate.TabIndex = 19;
            this.numDailyRate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numDailyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            currentY += rowHeight;
            
            // lblStatus
            this.lblStatus.AutoSize = false;
            this.lblStatus.Location = new System.Drawing.Point(labelX, currentY + 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(130, 20);
            this.lblStatus.TabIndex = 20;
            this.lblStatus.Text = "Status:";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // cmbStatus
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] { "Available", "Rented", "Maintenance" });
            this.cmbStatus.Location = new System.Drawing.Point(controlX, currentY);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(controlWidth, 27);
            this.cmbStatus.TabIndex = 21;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            currentY += rowHeight + 10;
            
            // Vehicle Image Section
            var lblImage = new System.Windows.Forms.Label
            {
                Text = "Vehicle Image:",
                Location = new System.Drawing.Point(labelX, currentY + 3),
                Size = new System.Drawing.Size(130, 20),
                Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.Color.FromArgb(73, 80, 87),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblImage);
            
            // pictureBoxVehicle
            this.pictureBoxVehicle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVehicle)).BeginInit();
            this.pictureBoxVehicle.Location = new System.Drawing.Point(controlX, currentY);
            this.pictureBoxVehicle.Name = "pictureBoxVehicle";
            this.pictureBoxVehicle.Size = new System.Drawing.Size(180, 140);
            this.pictureBoxVehicle.TabIndex = 24;
            this.pictureBoxVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxVehicle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pictureBoxVehicle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            
            // btnBrowseImage
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.btnBrowseImage.Location = new System.Drawing.Point(controlX + 190, currentY);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(90, 35);
            this.btnBrowseImage.TabIndex = 25;
            this.btnBrowseImage.Text = "📁 Browse";
            this.btnBrowseImage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowseImage.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnBrowseImage.ForeColor = System.Drawing.Color.White;
            this.btnBrowseImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseImage.FlatAppearance.BorderSize = 0;
            this.btnBrowseImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowseImage.Click += new System.EventHandler(this.BtnBrowseImage_Click);
            
            currentY += 150;
            
            // btnSave
            this.btnSave.Location = new System.Drawing.Point(controlX, currentY);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "💾 Save";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            
            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(controlX + 140, currentY);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "❌ Cancel";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(233, 236, 239);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            
            // VehicleEditForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, currentY + 60);
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.numDailyRate);
            this.Controls.Add(this.lblDailyRate);
            this.Controls.Add(this.txtRegistration);
            this.Controls.Add(this.lblRegistration);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.numSeatCapacity);
            this.Controls.Add(this.lblSeatCapacity);
            this.Controls.Add(this.txtGasType);
            this.Controls.Add(this.lblGasType);
            this.Controls.Add(this.txtTransmission);
            this.Controls.Add(this.lblTransmission);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.txtMake);
            this.Controls.Add(this.lblMake);
            this.Controls.Add(this.pictureBoxVehicle);
            this.Controls.Add(this.btnBrowseImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vehicle";
            ((System.ComponentModel.ISupportInitialize)(this.numDailyRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeatCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVehicle)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
