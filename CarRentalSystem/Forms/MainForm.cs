namespace CarRentalSystem.Forms
{
    /// <summary>
    /// Main application form with navigation menu.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ShowDashboard();
        }

        private void ShowDashboard()
        {
            mainPanel.Controls.Clear();
            
            var welcomePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(40)
            };
            
            // Welcome header
            var headerLabel = new Label
            {
                Text = "Welcome to Car Rental System",
                Location = new Point(40, 60),
                Size = new Size(800, 50),
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 52, 67)
            };
            
            var subHeaderLabel = new Label
            {
                Text = "Manage your fleet, customers, and rentals efficiently",
                Location = new Point(40, 120),
                Size = new Size(600, 30),
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(108, 117, 125)
            };
            
            // Quick action cards
            int cardY = 200;
            int cardX = 40;
            var cards = new[]
            {
                ("🚗 Manage Vehicles", "Add and manage your vehicle fleet", new Action(() => VehiclesMenuItem_Click(null, EventArgs.Empty))),
                ("👥 Manage Customers", "View and edit customer information", new Action(() => CustomersMenuItem_Click(null, EventArgs.Empty))),
                ("📅 New Reservation", "Create a new vehicle reservation", new Action(() => ReservationsMenuItem_Click(null, EventArgs.Empty))),
                ("🔑 Active Rentals", "Check-in/out vehicles", new Action(() => RentalsMenuItem_Click(null, EventArgs.Empty))),
                ("💳 Process Payments", "Handle rental payments", new Action(() => PaymentsMenuItem_Click(null, EventArgs.Empty)))
            };
            
            for (int i = 0; i < cards.Length; i++)
            {
                var card = CreateDashboardCard(cards[i].Item1, cards[i].Item2, cards[i].Item3);
                card.Location = new Point(cardX, cardY);
                welcomePanel.Controls.Add(card);
                
                cardX += 280;
                if ((i + 1) % 4 == 0)
                {
                    cardX = 40;
                    cardY += 140;
                }
            }
            
            welcomePanel.Controls.AddRange(new Control[] { headerLabel, subHeaderLabel });
            mainPanel.Controls.Add(welcomePanel);
        }
        
        private Panel CreateDashboardCard(string title, string description, Action onClick)
        {
            var card = new Panel
            {
                Size = new Size(260, 120),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            
            // Add shadow effect through border
            card.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(220, 220, 220), 1), 0, 0, card.Width - 1, card.Height - 1);
            };
            
            var titleLabel = new Label
            {
                Text = title,
                Location = new Point(15, 15),
                Size = new Size(230, 30),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 52, 67)
            };
            
            var descLabel = new Label
            {
                Text = description,
                Location = new Point(15, 50),
                Size = new Size(230, 50),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(108, 117, 125)
            };
            
            card.Controls.AddRange(new Control[] { titleLabel, descLabel });
            card.Click += (s, e) => onClick();
            titleLabel.Click += (s, e) => onClick();
            descLabel.Click += (s, e) => onClick();
            
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(240, 244, 248);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;
            
            return card;
        }

        private void HomeMenuItem_Click(object? sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void VehiclesMenuItem_Click(object? sender, EventArgs e)
        {
            LoadUserControl(new VehicleManagementControl());
        }

        private void CustomersMenuItem_Click(object? sender, EventArgs e)
        {
            LoadUserControl(new CustomerManagementControl());
        }

        private void ReservationsMenuItem_Click(object? sender, EventArgs e)
        {
            LoadUserControl(new ReservationManagementControl());
        }

        private void RentalsMenuItem_Click(object? sender, EventArgs e)
        {
            LoadUserControl(new RentalManagementControl());
        }

        private void PaymentsMenuItem_Click(object? sender, EventArgs e)
        {
            LoadUserControl(new PaymentManagementControl());
        }

        private void LoadUserControl(UserControl control)
        {
            mainPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(control);
        }
    }
}
