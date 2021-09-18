using System.IO;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using mPOSv2.Views.Activity.Sales;
using SampleBrowser.Core.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ZXing.Net.Mobile.Android;
using Environment = System.Environment;
using Platform = ZXing.Net.Mobile.Forms.Android.Platform;

namespace mPOSv2.Android
{
    [Activity(Label = "mPOS", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Platform.Init();

            Forms.Init(this, savedInstanceState);

            var dbFile = "settings_db.db3";
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbFileCompletePath = Path.Combine(folderPath, dbFile);

            CoreSampleBrowser.Init(Resources, this);

            LoadApplication(new App(dbFileCompletePath));

            Toolbar toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            int uiOptions = (int)Window.DecorView.SystemUiVisibility;

            uiOptions |= (int)SystemUiFlags.LowProfile;
            uiOptions |= (int)SystemUiFlags.Fullscreen;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;

            Window.SetSoftInputMode(SoftInput.AdjustResize);
        }

        protected override void OnResume()
        {
            base.OnResume();

            int uiOptions = (int)Window.DecorView.SystemUiVisibility;

            uiOptions |= (int)SystemUiFlags.LowProfile;
            uiOptions |= (int)SystemUiFlags.Fullscreen;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 16908332)
            {
                var page = Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

                if (page is SalesDetailView salesDetailView) 
                {
                    var currentpage = salesDetailView;

                    if (currentpage?.BackButtonAction != null)
                    {
                        currentpage?.BackButtonAction.Invoke();
                        return false;
                    }
                }
                return base.OnOptionsItemSelected(item);
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed() 
        {
            var page = Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            if (page is SalesDetailView salesDetailView)
            {
                try
                {
                    var currentpage = salesDetailView;

                    if (currentpage?.BackButtonAction != null)
                    {
                        currentpage?.BackButtonAction.Invoke();
                    }
                    else
                    {
                        base.OnBackPressed();
                    }  
                }
                catch 
                {
                    base.OnBackPressed();
                }
            }
            else
            {
                base.OnBackPressed();
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            int uiOptions = (int)Window.DecorView.SystemUiVisibility;

            uiOptions |= (int)SystemUiFlags.LowProfile;
            uiOptions |= (int)SystemUiFlags.Fullscreen;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}