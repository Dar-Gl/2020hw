using ProductMaintenance.DataAccess;

namespace ProductMaintenance
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            try
            {
                DemoDatabaseInitializer.InitializeIfRequested();
                Application.Run(new frmProductMaintenance());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The Product Maintenance app could not start.\n\n" +
                    "Check that SQL Server Express LocalDB is installed and the " +
                    "connection string in App.config is correct.\n\nDetails: " + ex.Message,
                    "Database Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
