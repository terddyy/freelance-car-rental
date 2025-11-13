namespace CarRentalSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            
            // Initialize database
            _ = Data.DatabaseManager.Instance;
            
            // Show landing page first
            using (var landingPage = new Forms.LandingPageForm())
            {
                if (landingPage.ShowDialog() == DialogResult.OK)
                {
                    // User clicked "Start Now" - open main application
                    Application.Run(new Forms.MainForm());
                }
            }
        }
    }
}
