namespace CarRentalSystem.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStripMenuItem homeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vehiclesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reservationsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rentalsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentsMenuItem;

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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.homeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vehiclesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rentalsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            
            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeMenuItem,
            this.vehiclesMenuItem,
            this.customersMenuItem,
            this.reservationsMenuItem,
            this.rentalsMenuItem,
            this.paymentsMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1400, 45);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(45, 52, 67);
            this.menuStrip.ForeColor = System.Drawing.Color.White;
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 8, 0, 8);
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            
            // homeMenuItem
            this.homeMenuItem.Name = "homeMenuItem";
            this.homeMenuItem.Size = new System.Drawing.Size(80, 29);
            this.homeMenuItem.Text = "🏠 Home";
            this.homeMenuItem.ForeColor = System.Drawing.Color.White;
            this.homeMenuItem.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.homeMenuItem.Click += new System.EventHandler(this.HomeMenuItem_Click);
            
            // vehiclesMenuItem
            this.vehiclesMenuItem.Name = "vehiclesMenuItem";
            this.vehiclesMenuItem.Size = new System.Drawing.Size(80, 29);
            this.vehiclesMenuItem.Text = "🚗 Vehicles";
            this.vehiclesMenuItem.ForeColor = System.Drawing.Color.White;
            this.vehiclesMenuItem.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.vehiclesMenuItem.Click += new System.EventHandler(this.VehiclesMenuItem_Click);
            
            // customersMenuItem
            this.customersMenuItem.Name = "customersMenuItem";
            this.customersMenuItem.Size = new System.Drawing.Size(100, 29);
            this.customersMenuItem.Text = "👥 Customers";
            this.customersMenuItem.ForeColor = System.Drawing.Color.White;
            this.customersMenuItem.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.customersMenuItem.Click += new System.EventHandler(this.CustomersMenuItem_Click);
            
            // reservationsMenuItem
            this.reservationsMenuItem.Name = "reservationsMenuItem";
            this.reservationsMenuItem.Size = new System.Drawing.Size(120, 29);
            this.reservationsMenuItem.Text = "📅 Reservations";
            this.reservationsMenuItem.ForeColor = System.Drawing.Color.White;
            this.reservationsMenuItem.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.reservationsMenuItem.Click += new System.EventHandler(this.ReservationsMenuItem_Click);
            
            // rentalsMenuItem
            this.rentalsMenuItem.Name = "rentalsMenuItem";
            this.rentalsMenuItem.Size = new System.Drawing.Size(90, 29);
            this.rentalsMenuItem.Text = "🔑 Rentals";
            this.rentalsMenuItem.ForeColor = System.Drawing.Color.White;
            this.rentalsMenuItem.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.rentalsMenuItem.Click += new System.EventHandler(this.RentalsMenuItem_Click);
            
            // paymentsMenuItem
            this.paymentsMenuItem.Name = "paymentsMenuItem";
            this.paymentsMenuItem.Size = new System.Drawing.Size(100, 29);
            this.paymentsMenuItem.Text = "💳 Payments";
            this.paymentsMenuItem.ForeColor = System.Drawing.Color.White;
            this.paymentsMenuItem.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.paymentsMenuItem.Click += new System.EventHandler(this.PaymentsMenuItem_Click);
            
            // mainPanel
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 45);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1400, 755);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Car Rental System - Modern Dashboard";
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BackColor = System.Drawing.Color.White;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
