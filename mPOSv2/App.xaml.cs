using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace mPOSv2
{
    public partial class App : Application
    {
        public static string FilePath;
        public App()
        {
            Device.SetFlags(new[] { "SwipeView_Experimental" });

            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }

        public App(string filePath)
        {
            Device.SetFlags(new[] { "SwipeView_Experimental" });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMzN0AzMTM4MmUzMTJlMzBYTml4RFZ2ZmVsRmlNbmdCcDNjVG9naS9qWEFzVXJvL0FkSmlJbnkzVHV3PQ==");

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
