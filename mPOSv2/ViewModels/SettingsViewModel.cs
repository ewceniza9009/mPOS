using mPOSv2.Models;
using mPOSv2.Services;
using Xamarin.Forms;

namespace mPOSv2.ViewModels
{
    internal class SettingsViewModel : ViewModelBase
    {
        private Command _Saves;

        private Settings _Settings;

        public SettingsViewModel()
        {
            Load();
        }

        public Settings Settings
        {
            get => _Settings;
            set => SetProperty(ref _Settings, value);
        }

        public Command Save
        {
            get => _Saves ?? (_Saves = new Command(ExecuteSave, x => true));
            set => SetProperty(ref _Saves, value);
        }

        public void Load()
        {
            Settings = SettingsRepository.GetSettings();
        }

        private void ExecuteSave(object sender)
        {
            OnPropertyChanged(nameof(Settings));

            SettingsRepository.Save(Settings);

            Device.BeginInvokeOnMainThread(async () =>
                await Application.Current.MainPage.DisplayAlert("Settings", "Record saved.", "Ok"));
        }

        public void RefreshSettings()
        {
            OnPropertyChanged(nameof(Settings));
        }
    }
}