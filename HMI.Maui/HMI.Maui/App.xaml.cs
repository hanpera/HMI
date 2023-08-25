namespace HMI.Maui
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("");
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
