using mPOS.POCO;
using mPOS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using mPOS.Views.Start;
using Xamarin.Forms;
using Xamarin.Forms.Internals;


namespace mPOS.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        #region Initialize
        public void Load(object obj = null)
        {
            string search = obj?.ToString() ?? string.Empty;

            IsBusy = true;

            Task.Run(async () =>
            {
                POCO.MstCustomerFilter customerFilter = null;

                if (!string.IsNullOrEmpty(search))
                {
                    customerFilter = new MstCustomerFilter()
                    {
                        Customer = search,
                        filterMethods = new FilterMethods()
                        {
                            Operations = new List<FilterOperation>()
                            {
                                new FilterOperation("Customer", Operation.Contains)
                            }
                        }
                    };
                }

                Customers = await ApiRequest<POCO.MstCustomerFilter, ObservableCollection<POCO.MstCustomer>>
                    .PostRead("MstCustomer/BulkGet", customerFilter);

                IsBusy = false;
            });
        }
        #endregion

        #region Properties
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value);
        }
        private bool _IsBusy;

        public bool IsProcessingAPI
        {
            get => _IsProcessingApi;
            set => SetProperty(ref _IsProcessingApi, value);
        }
        private bool _IsProcessingApi;

        public bool IsChanged { get; set; }

        public string Title => $"{SelectedCustomer.Customer ?? "Customer"}'s Detail";

        public long SelectedCustomerId
        {
            get => _SelectedCustomerId == 0 ? SelectedCustomer.Id : _SelectedCustomerId;
            set => SetProperty(ref _SelectedCustomerId, value);
        }

        private long _SelectedCustomerId;

        public MstCustomer SelectedCustomer
        {
            get => _SelectedCustomer;
            set => SetProperty(ref _SelectedCustomer, value);

        }
        private MstCustomer _SelectedCustomer;

        public ObservableCollection<POCO.MstCustomer> Customers
        {
            get => _Customers;
            set => SetProperty(ref _Customers, value);
        }
        private ObservableCollection<POCO.MstCustomer> _Customers;
        #endregion

        #region Commands
        public Command RefreshCommand
        {
            get => _RefreshCommand ?? (_RefreshCommand = new Command(Load, (x) => true));
            set => SetProperty(ref _RefreshCommand, value);
        }
        private Command _RefreshCommand;

        public Command Save
        {

            get => _Save ?? (_Save = new Command(ExecuteSave, () => true));
            set => SetProperty(ref _Save, value);
        }
        private Command _Save;

        public Command Delete
        {

            get => _Delete ?? (_Delete = new Command(ExecuteDelete, () => true));
            set => SetProperty(ref _Delete, value);
        }
        private Command _Delete;
        #endregion

        #region Methods
        private void ExecuteSave()
        {
            IsProcessingAPI = true;

            Task.Run(async () =>
            {
                Thread.Sleep(1000);

                SelectedCustomerId = await ApiRequest<POCO.MstCustomer, POCO.MstCustomer>
                    .Save("MstCustomer/Save", SelectedCustomer);

                IsProcessingAPI = false;

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(Title, "Record saved.", "Ok"));
            });
        }

        private void ExecuteDelete()
        {
            Device.BeginInvokeOnMainThread(async () =>
                await Application.Current.MainPage
                    .DisplayAlert(Title, "Do you want to delete this record?.", "Yes", "No")
                    .ContinueWith(x =>
                    {
                        if (x.Result)
                        {
                            IsProcessingAPI = true;

                            Task.Run(async () =>
                            {
                                Thread.Sleep(1000);

                                await ApiRequest<POCO.MstCustomer, POCO.MstCustomer>
                                    .Delete("MstCustomer/Delete", SelectedCustomer.Id);

                                IsProcessingAPI = false;

                                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(Title, "Record deleted.", "Ok"));
                                //Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PopAsync());

                                SelectedCustomer = new POCO.MstCustomer();
                            });
                        }
                    },
                    TaskScheduler.FromCurrentSynchronizationContext())
                );
        }

        public void RefreshSelectedCustomer()
        {
            OnPropertyChanged(nameof(SelectedCustomer));
            OnPropertyChanged(nameof(Title));
        }
        #endregion
    }
}
