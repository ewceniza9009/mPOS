using System;
using System.Collections.Generic;
using System.Text;
using mPOS.Services;
using Xamarin.Forms;

namespace mPOS.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            Load();
        }

        public void Load()
        {
            Settings = SettingsRepository.GetSettings();
        }

        public Models.Settings Settings
        {
            get => _Settings;
            set => SetProperty(ref _Settings, value);
        }

        private Models.Settings _Settings;

        public Command Save
        {
            get => _Saves ?? (_Saves = new Command(ExecuteSave, (x) => true));
            set => SetProperty(ref _Saves, value);
        }
        private Command _Saves;

        private void ExecuteSave(object sender)
        {
            OnPropertyChanged(nameof(Settings));

            SettingsRepository.Save(Settings);

            Device.BeginInvokeOnMainThread(async () =>
                await Application.Current.MainPage.DisplayAlert("Settings", "Record saved.", "Ok"));
        }
    }
}
