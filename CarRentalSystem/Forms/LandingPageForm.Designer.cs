namespace CarRentalSystem.Forms
{
    partial class LandingPageForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button startNowButton;
        private System.Windows.Forms.Panel buttonPanel;

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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.startNowButton = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            
            // pictureBox
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1200, 700);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            
            // buttonPanel
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Height = 120;
            this.buttonPanel.TabIndex = 1;
            this.buttonPanel.BackColor = System.Drawing.Color.Transparent;
            
            // startNowButton
            this.startNowButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.startNowButton.Name = "startNowButton";
            this.startNowButton.Size = new System.Drawing.Size(220, 60);
            this.startNowButton.TabIndex = 0;
            this.startNowButton.Text = "START NOW";
            this.startNowButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.startNowButton.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.startNowButton.ForeColor = System.Drawing.Color.White;
            this.startNowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startNowButton.FlatAppearance.BorderSize = 0;
            this.startNowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startNowButton.UseVisualStyleBackColor = false;
            this.startNowButton.Click += new System.EventHandler(this.StartNowButton_Click);
            
            // Mouse hover effects
            this.startNowButton.MouseEnter += (s, e) => 
            {
                this.startNowButton.BackColor = System.Drawing.Color.FromArgb(0, 105, 217);
            };
            this.startNowButton.MouseLeave += (s, e) => 
            {
                this.startNowButton.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            };
            
            // buttonPanel controls
            this.buttonPanel.Controls.Add(this.startNowButton);
            this.buttonPanel.Resize += (s, e) => this.CenterButton();
            
            // LandingPageForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LandingPageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Car Rental System";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
