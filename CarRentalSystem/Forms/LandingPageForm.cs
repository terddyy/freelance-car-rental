using System;
using System.Drawing;
using System.Windows.Forms;

namespace CarRentalSystem.Forms
{
    /// <summary>
    /// Landing page form displayed when the application starts.
    /// </summary>
    public partial class LandingPageForm : Form
    {
        public LandingPageForm()
        {
            InitializeComponent();
            LoadLandingPageImage();
        }

        private void LoadLandingPageImage()
        {
            try
            {
                // Get the path to the landing page image
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                    "..", "..", "..", "..", "car-rental-images", "landing-page.png");
                imagePath = Path.GetFullPath(imagePath);

                if (File.Exists(imagePath))
                {
                    // Load and display the image
                    pictureBox.Image = Image.FromFile(imagePath);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    // Fallback if image not found - show text
                    var fallbackLabel = new Label
                    {
                        Text = "🚗 Car Rental System",
                        Font = new Font("Segoe UI", 32F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(45, 52, 67),
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    pictureBox.Controls.Add(fallbackLabel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading landing page image: {ex.Message}", 
                    "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void StartNowButton_Click(object? sender, EventArgs e)
        {
            // Close this landing page and open the main application
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
