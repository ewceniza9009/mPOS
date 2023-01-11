using System.IO;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
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
        private const int BACK_BUTTON_GUID = 16908332;

        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            Platform.Init();

            Forms.Init(this, savedInstanceState);

            var dbFile = "settings_db.db3";
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbFileCompletePath = Path.Combine(folderPath, dbFile);

            CoreSampleBrowser.Init(Resources, this);

            LoadApplication(new App(dbFileCompletePath));

            Toolbar toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            if (Window != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                {
                    IWindowInsetsController wicController = Window.InsetsController;


                    Window.SetDecorFitsSystemWindows(false);
                    Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

                    if (wicController != null)
                    {
                        wicController.Hide(WindowInsets.Type.Ime());
                        wicController.Hide(WindowInsets.Type.NavigationBars());
                    }
                }
            }
            else 
            {
                #pragma warning disable CS0618
                int uiOptions = (int)Window.DecorView.SystemUiVisibility;

                uiOptions |= (int)SystemUiFlags.LowProfile;
                uiOptions |= (int)SystemUiFlags.Fullscreen;
                uiOptions |= (int)SystemUiFlags.HideNavigation;
                uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
                #pragma warning restore CS0618
            }

            Window.SetSoftInputMode(SoftInput.AdjustResize);

            Instance = this;
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (Window != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                {
                    IWindowInsetsController wicController = Window.InsetsController;


                    Window.SetDecorFitsSystemWindows(false);
                    Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

                    if (wicController != null)
                    {
                        wicController.Hide(WindowInsets.Type.Ime());
                        wicController.Hide(WindowInsets.Type.NavigationBars());
                    }
                }
            }
            else
            {
                #pragma warning disable CS0618
                int uiOptions = (int)Window.DecorView.SystemUiVisibility;

                uiOptions |= (int)SystemUiFlags.LowProfile;
                uiOptions |= (int)SystemUiFlags.Fullscreen;
                uiOptions |= (int)SystemUiFlags.HideNavigation;
                uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
                #pragma warning restore CS0618
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == BACK_BUTTON_GUID)
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

            if (Window != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                {
                    IWindowInsetsController wicController = Window.InsetsController;


                    Window.SetDecorFitsSystemWindows(false);
                    Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

                    if (wicController != null)
                    {
                        wicController.Hide(WindowInsets.Type.Ime());
                        wicController.Hide(WindowInsets.Type.NavigationBars());
                    }
                }
            }
            else
            {
                #pragma warning disable CS0618
                int uiOptions = (int)Window.DecorView.SystemUiVisibility;

                uiOptions |= (int)SystemUiFlags.LowProfile;
                uiOptions |= (int)SystemUiFlags.Fullscreen;
                uiOptions |= (int)SystemUiFlags.HideNavigation;
                uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
                #pragma warning restore CS0618
            }

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