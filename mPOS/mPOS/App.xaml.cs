using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mPOS.Services;
using mPOS.Views;

namespace mPOS
{
    public partial class App : Application
    {
        public static string FilePath;
        public App()
        {
            Device.SetFlags(new[] { "SwipeView_Experimental" });

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new Login());
        }

        public App(string filePath)
        {
            Device.SetFlags(new[] { "SwipeView_Experimental" });

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new Login());

            FilePath = filePath;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
