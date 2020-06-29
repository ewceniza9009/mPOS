using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOSv2.Services;
using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Start
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        private SettingsViewModel vm;
        public Settings()
        {
            InitializeComponent();
            vm = new SettingsViewModel();

            BindingContext = vm;
        }

        private void CmdGetDefault_OnClicked(object sender, EventArgs e)
        {
            vm.Settings.ServerName = GlobalVariables.UriBase;
            vm.RefreshSettings();
        }
    }
}