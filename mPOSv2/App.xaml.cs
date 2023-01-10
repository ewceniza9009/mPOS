using Syncfusion.Licensing;
using Xamarin.Forms;

namespace mPOSv2
{
    public partial class App : Application
    {
        public static string FilePath;

        public App()
        {
            Device.SetFlags(new[] {"SwipeView_Experimental"});

            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }

        public App(string filePath)
        {
            Device.SetFlags(new[] {"SwipeView_Experimental"});

            SyncfusionLicenseProvider.RegisterLicense("MzcxM0AzMjMwMkUzNDJFMzBpYnpCR0U4NjhVTjR2QWFIRkZHa2VHOGI3N1JRYlFKQ3dYbk5iTE9JTmdFPQ==");

            FilePath = filePath;

            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}