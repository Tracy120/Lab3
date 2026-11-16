using Lab3.Controllers;
using Lab3.Views;

namespace Lab3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var application = new AppController();
            try
            {
                application.Start();
                Application.Run(new LoginForm(application));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Startup error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}