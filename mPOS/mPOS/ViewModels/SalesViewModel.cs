using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using mPOS.POCO;
using mPOS.Services;
using mPOS.Views.Activities.Sales;
using mPOS.Views.Start;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace mPOS.ViewModels
{
    public class SalesViewModel : ViewModelBase
    {
        #region Initialize
        public void Load(object sender = null)
        {
            string search = sender?.ToString() ?? string.Empty;

            IsBusy = true;

            Task.Run(async () =>
            {
                POCO.TrnSalesFilter salesFilter = null;

                if (!string.IsNullOrEmpty(search) || SearchSaleDate != null)
                {
                    salesFilter = new POCO.TrnSalesFilter()
                    {
                        SalesNumber = search,
                        SalesDate = SearchSaleDate ?? DateTime.Now.Date,
                        filterMethods = new FilterMethods()
                        {
                            Operations = new List<FilterOperation>()
                            {
                                new FilterOperation("SalesNumber", Operation.Contains),
                                new FilterOperation("SalesDate", Operation.Equals)
                            }
                        }
                    };
                }

                SaleUnits = await APIItemRequest.GetUnits();
                Customers = await APISalesRequest.GetCustomers();

                Sales = await ApiRequest<POCO.TrnSalesFilter, ObservableCollection<POCO.TrnSales>>
                    .PostRead("TrnSales/BulkGet", salesFilter);

                Sales.ForEach(x =>
                {
                    x.CustomerName = Customers
                        .SingleOrDefault(c => c.Id == x.CustomerId)
                        ?.Customer;
                });

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

        public string SearchSaleEntry
        {
            get => _SearchSaleEntry;
            set => SetProperty(ref _SearchSaleEntry, value);
        }
        private string _SearchSaleEntry;

        public DateTime? SearchSaleDate
        {
            get => _SearchSaleDate ?? DateTime.Now.Date;
            set => SetProperty(ref _SearchSaleDate, value);
        }

        private DateTime? _SearchSaleDate;

        public bool IsChanged { get; set; }

        public string Title => $"INV #: {SelectedSale.SalesNumber ?? "New"}";

        public long SelectedSaleId
        {
            get => _SelectedSaleId == 0 ? SelectedSale.Id : _SelectedSaleId;
            set => SetProperty(ref _SelectedSaleId, value);
        }
        private long _SelectedSaleId;

        public MstUnit SelectedUnit
        {
            get => _SelectedUnit;
            set => SetProperty(ref _SelectedUnit, value);

        }
        private MstUnit _SelectedUnit;

        public ObservableCollection<POCO.MstUnit> SaleUnits
        {
            get => _SaleUnits;
            set => SetProperty(ref _SaleUnits, value);
        }
        private ObservableCollection<POCO.MstUnit> _SaleUnits;

        public ObservableCollection<POCO.MstCustomer> Customers
        {
            get => _Customers;
            set => SetProperty(ref _Customers, value);
        }
        private ObservableCollection<POCO.MstCustomer> _Customers;

        public POCO.TrnSales SelectedSale
        {
            get => _SelectedSale;
            set => _SelectedSale = value;

        }
        private POCO.TrnSales _SelectedSale;

        public ObservableCollection<POCO.TrnSales> Sales
        {
            get => _Sales;
            set => SetProperty(ref _Sales, value);
        }
        private ObservableCollection<POCO.TrnSales> _Sales;
        #endregion

        #region Commands
        public Command RefreshSales
        {
            get => _RefreshSales ?? (_RefreshSales = new Command(Load, (x) => true));
            set => SetProperty(ref _RefreshSales, value);
        }
        private Command _RefreshSales;

        public Command RefreshSelectedSale
        {
            get => _RefreshSelectedSale ?? (_RefreshSelectedSale = new Command(ExecuteRefreshSelectedItem, (x) => true));
            set => SetProperty(ref _RefreshSelectedSale, value);
        }
        private Command _RefreshSelectedSale;

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

        public Command SelectSale
        {
            get => _SelectSale ?? (_SelectSale = new Command(ExecuteSelectSale, (x) => true));
            set => SetProperty(ref _SelectSale, value);
        }
        private Command _SelectSale;

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
        public void ExecuteRefreshSelectedItem(object sender)
        {
            OnPropertyChanged(nameof(SelectedSale));
            OnPropertyChanged(nameof(Title));
        }

        private void ExecuteAdd(object sender)
        {
            var newItem = new POCO.TrnSales();

            Sales.Add(newItem);
            SelectedSale = newItem;

            Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PushAsync(new SalesDetailView(this)));
        }

        private void ExecuteSearch(object sender)
        {
            Load(SearchSaleEntry);
        }

        private void ExecuteSelectSale(object sender)
        {
            if (sender is POCO.TrnSales selectedSale)
            {
                IsChanged = false;
                SelectedSale = selectedSale;

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PushAsync(new SalesDetailView(this)));
            }
        }

        private void ExecuteSave()
        {
            var isTaskRun = false;

            IsProcessingAPI = true;

            Task.Run(async () =>
            {
                Thread.Sleep(1000);

                SelectedSaleId = await ApiRequest<POCO.TrnSales, POCO.TrnSales>
                    .Save("TrnSales/Save", SelectedSale);

                IsProcessingAPI = false;

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(Title, "Record saved.", "Ok"));

                isTaskRun = true;
            });

            if (!isTaskRun)
            {
                if ((SelectedSale.SalesNumber?.Length ?? 0) < 2)
                {
                    SelectedSale.SalesNumber = "NA";
                    OnPropertyChanged(nameof(SelectedSale));

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

                                await ApiRequest<POCO.MstItem, POCO.MstItem>
                                    .Delete("TrnSales/Delete", SelectedSale.Id);

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
