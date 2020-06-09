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
using Color = System.Drawing.Color;


namespace mPOS.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        #region Initialize
        public void Load(object sender = null)
        {
            string search = sender?.ToString() ?? string.Empty;

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

        public string SearchCustomerEntry
        {
            get => _SearchCustomerEntry;
            set => SetProperty(ref _SearchCustomerEntry, value);
        }
        private string _SearchCustomerEntry;

        public bool IsChanged { get; set; }

        public string Title => $"{SelectedCustomer.Customer ?? "New Customer"}'s Detail";

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
        public Command RefreshCustomers
        {
            get => _RefreshCustomers ?? (_RefreshCustomers = new Command(Load, (x) => true));
            set => SetProperty(ref _RefreshCustomers, value);
        }
        private Command _RefreshCustomers;

        public Command RefreshSelectedCustomer
        {
            get => _RefreshSelectedCustomer ?? (_RefreshSelectedCustomer = new Command(ExecuteRefreshSelectedCustomer, () => true));
            set => SetProperty(ref _RefreshSelectedCustomer, value);
        }
        private Command _RefreshSelectedCustomer;

        public Command Add
        {
            get => _Add ?? (_Add = new Command(ExecuteAdd, (x) => true));
            set => SetProperty(ref _Add, value);
        }
        private Command _Add;

        public Command Search
        {
            get => _Search ?? (_Search = new Command(ExecuteSearch, (x) => true));
            set => SetProperty(ref _Search, value);
        }
        private Command _Search;

        public Command SelectCustomer
        {
            get => _SelectCustomer ?? (_SelectCustomer = new Command(ExecuteSelectCustomer, (x) => true));
            set => SetProperty(ref _SelectCustomer, value);
        }
        private Command _SelectCustomer;

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
        private void ExecuteRefreshSelectedCustomer()
        {
            OnPropertyChanged(nameof(SelectedCustomer));
            OnPropertyChanged(nameof(Title));
        }

        private void ExecuteAdd(object sender)
        {
            var newCustomer = new MstCustomer();

            Customers.Add(newCustomer);
            SelectedCustomer = newCustomer;

            Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PushAsync(new CustomerDetailView(this)));
        }

        private void ExecuteSearch(object sender)
        {
            Load(SearchCustomerEntry);
        }

        private void ExecuteSelectCustomer(object sender)
        {
            if (sender is MstCustomer selectedCustomer)
            {
                IsChanged = false;
                SelectedCustomer = selectedCustomer;

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PushAsync(new CustomerDetailView(this)));
            }
        }

        private void ExecuteSave()
        {
            var isTaskRun = false;
            
            IsProcessingAPI = true;

            Task.Run(async () =>
            {
                Thread.Sleep(1000);

                SelectedCustomerId = await ApiRequest<POCO.MstCustomer, POCO.MstCustomer>
                    .Save("MstCustomer/Save", SelectedCustomer);

                IsProcessingAPI = false;

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(Title, "Record saved.", "Ok"));

                isTaskRun = true;
            });

            if (!isTaskRun)
            {
                if ((SelectedCustomer.Customer?.Length ?? 0) < 2)
                {
                    SelectedCustomer.Customer = "NA";
                    OnPropertyChanged(nameof(SelectedCustomer));

                    IsProcessingAPI = false;
                }
            }
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
                                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PopAsync());
                            });
                        }
                    },
                    TaskScheduler.FromCurrentSynchronizationContext())
                );
        }
        #endregion
    }
}
