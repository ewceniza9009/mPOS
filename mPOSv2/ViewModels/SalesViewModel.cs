﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using mPOSv2.Enums;
using mPOS.POCO;
using mPOSv2.Services;
using mPOSv2.Views.Activity.Sales;
using mPOSv2.Views.Start;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace mPOSv2.ViewModels
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
                TrnSalesFilter salesFilter = null;

                if (!string.IsNullOrEmpty(search) || SearchSaleDate != null)
                {
                    salesFilter = new TrnSalesFilter()
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

                Customers = await APISalesRequest.GetCustomers();

                Sales = await ApiRequest<TrnSalesFilter, ObservableCollection<TrnSales>>
                    .PostRead("TrnSales/BulkGet", salesFilter);

                Sales.ForEach(x =>
                {
                    x.CustomerName = Customers
                        .SingleOrDefault(c => c.Id == x.CustomerId)
                        ?.Customer;
                });

                Items = await APISalesRequest.GetItems();
                SaleUnits = await APISalesRequest.GetUnits();

                OnPropertyChanged(nameof(Sales));
                OnPropertyChanged(nameof(IsListEmpty));
                OnPropertyChanged(nameof(IsListNotEmpty));

                IsBusy = false;
            });
        }

        public void LoadItems(object sender = null)
        {
            string search = sender?.ToString() ?? string.Empty;

            IsBusy = true;

            Task.Run(async () =>
            {
                MstItemFilter itemFilter = null;

                if (!string.IsNullOrEmpty(search))
                {
                    itemFilter = new MstItemFilter()
                    {
                        ItemDescription = search,
                        filterMethods = new FilterMethods()
                        {
                            Operations = new List<FilterOperation>()
                            {
                                new FilterOperation("ItemDescription", Operation.Contains)
                            }
                        }
                    };
                }

                Items = await ApiRequest<MstItemFilter, ObservableCollection<MstItem>>
                    .PostRead("MstItem/BulkGet", itemFilter);

                IsBusy = false;
            });
        }
        #endregion

        #region Properties

        public bool IsListEmpty
        {
            get
            {
                var result = false;

                if (Sales != null)
                {
                    result = !Sales.Any();
                }

                return result;
            }
        }

        public bool IsListNotEmpty
        {
            get
            {
                var result = false;

                if (Sales != null)
                {
                    result = Sales.Any();
                }

                return result;
            }
        }

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

        public string SearchItemEntry
        {
            get => _SearchItemEntry;
            set => SetProperty(ref _SearchItemEntry, value);
        }
        private string _SearchItemEntry;

        public string SearchBarcode { get; set; }

        public ItemFrom ItemFrom { get; set; }

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

        public ObservableCollection<MstUnit> SaleUnits
        {
            get => _SaleUnits;
            set => SetProperty(ref _SaleUnits, value);
        }
        private ObservableCollection<MstUnit> _SaleUnits;

        public MstCustomer SelectedCustomer
        {
            get => _SelectedCustomer;
            set => SetProperty(ref _SelectedCustomer, value);
        }
        private MstCustomer _SelectedCustomer;

        public ObservableCollection<MstCustomer> Customers
        {
            get => _Customers;
            set => SetProperty(ref _Customers, value);
        }
        private ObservableCollection<MstCustomer> _Customers;

        public MstItem SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
        }
        private MstItem _SelectedItem;

        public ObservableCollection<MstItem> Items
        {
            get => _Items;
            set => SetProperty(ref _Items, value);
        }
        private ObservableCollection<MstItem> _Items;

        public TrnSales SelectedSale
        {
            get => _SelectedSale;
            set => _SelectedSale = value;

        }
        private TrnSales _SelectedSale;

        public TrnSalesLine SelectedSaleLine
        {
            get => _SelectedSaleLine;
            set => _SelectedSaleLine = value;
        }
        private TrnSalesLine _SelectedSaleLine;

        public ObservableCollection<TrnSales> Sales
        {
            get => _Sales;
            set => SetProperty(ref _Sales, value);
        }
        private ObservableCollection<TrnSales> _Sales;
        #endregion

        #region Commands
        public Command RefreshSales
        {
            get => _RefreshSales ?? (_RefreshSales = new Command(Load, (x) => true));
            set => SetProperty(ref _RefreshSales, value);
        }
        private Command _RefreshSales;

        public Command RefreshItems
        {
            get => _RefreshItems ?? (_RefreshItems = new Command(LoadItems, (x) => true));
            set => SetProperty(ref _RefreshItems, value);
        }
        private Command _RefreshItems;

        public Command RefreshSelectedSale
        {
            get => _RefreshSelectedSale ??
                   (_RefreshSelectedSale = new Command(ExecuteRefreshSelectedSale, (x) => true));
            set => SetProperty(ref _RefreshSelectedSale, value);
        }
        private Command _RefreshSelectedSale;

        public Command SelectCustomer
        {
            get => _SelectCustomer ?? (_SelectCustomer = new Command(ExecuteSelectCustomer, (x) => true));
            set => SetProperty(ref _SelectCustomer, value);
        }
        private Command _SelectCustomer;

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

        public Command SearchItem
        {
            get => _SearchItem ?? (_SearchItem = new Command(ExecuteSearchItem, (x) => true));
            set => SetProperty(ref _SearchItem, value);
        }
        private Command _SearchItem;

        public Command SelectSale
        {
            get => _SelectSale ?? (_SelectSale = new Command(ExecuteSelectSale, (x) => true));
            set => SetProperty(ref _SelectSale, value);
        }
        private Command _SelectSale;

        public Command SelectSaleLine
        {
            get => _SelectSaleLine ?? (_SelectSaleLine = new Command(ExecuteSelectItem, (x) => true));
            set => SetProperty(ref _SelectSaleLine, value);
        }
        private Command _SelectSaleLine;

        public Command DeleteSaleLine
        {
            get => _DeleteSaleLine ?? (_DeleteSaleLine = new Command(ExecuteDeleteSaleLine, (x) => true));
            set => SetProperty(ref _DeleteSaleLine, value);
        }

        private Command _DeleteSaleLine;

        public Command SelectItem
        {
            get => _SelectItem ?? (_SelectItem = new Command(ExecuteSelectItem, (x) => true));
            set => SetProperty(ref _SelectItem, value);
        }
        private Command _SelectItem;

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
        public void ExecuteRefreshSelectedSale(object sender)
        {
            OnPropertyChanged(nameof(SelectedSale));
            OnPropertyChanged(nameof(SearchBarcode));
            OnPropertyChanged(nameof(Title));
        }

        public void ExecuteRefreshSelectedSaleLine(object sender)
        {
            OnPropertyChanged(nameof(SelectedSaleLine));
            OnPropertyChanged(nameof(SearchBarcode));
        }

        private void ExecuteAdd(object sender)
        {
            var newSale = new TrnSales()
            {
                SalesDate = DateTime.Now.Date,
                CustomerId = 5451,
                CustomerName = "Walk In"
            };
            newSale.TrnSalesLines = new List<TrnSalesLine>();

            SelectedCustomer = Customers.SingleOrDefault(y => y.Id == 5451);

            Sales.Add(newSale);
            SelectedSale = newSale;

            LoadSalesLine();

            Device.BeginInvokeOnMainThread(async () =>
                await Application.Current.MainPage.Navigation.PushAsync(new SalesDetailView(this)));
        }

        public void ExecuteSearch(object sender)
        {
            Load(SearchSaleEntry);

            OnPropertyChanged(nameof(Sales));
            OnPropertyChanged(nameof(IsListEmpty));
            OnPropertyChanged(nameof(IsListNotEmpty));
        }

        public void ExecuteSearchItem(object sender)
        {
            LoadItems(SearchItemEntry);
        }

        private void ExecuteSelectSale(object sender)
        {
            if (sender is TrnSales selectedSale)
            {
                IsChanged = false;
                SelectedSale = selectedSale;
                SelectedCustomer = Customers.SingleOrDefault(y => y.Id == SelectedSale.CustomerId);

                LoadSalesLine();

                Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage.Navigation.PushAsync(new SalesDetailView(this)));
            }
        }

        private void ExecuteSelectItem(object sender)
        {
            if (sender is MstItem selectedItem)
            {
                ItemFrom = ItemFrom.Item;

                IsChanged = false;
                SelectedItem = selectedItem;

                SelectedSaleLine = new TrnSalesLine()
                {
                    ItemId = SelectedItem.Id,
                    ItemDescription = SelectedItem.ItemDescription,
                    BarCode = SelectedItem.BarCode,
                    UnitId = SelectedItem.UnitId,
                    UnitName = SaleUnits?.SingleOrDefault(x => x.Id == SelectedItem.UnitId)?.Unit ?? "Unit(s)",
                    Quantity = 1,
                    Price = SelectedItem.Price,
                    NetPrice = SelectedItem.Price,
                    Amount = SelectedItem.Price,
                    SalesLineTimeStamp = DateTime.Now
                };

                ExecuteRefreshSelectedSaleLine(new object());

                Device.BeginInvokeOnMainThread(
                    async () => await Application.Current.MainPage.Navigation.PopModalAsync());
                Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage.Navigation.PushAsync(new SalesItemDetailView(this)));
            }
            else
            {
                ItemFrom = ItemFrom.SaleLine;

                SelectedSaleLine = sender as TrnSalesLine;

                ExecuteRefreshSelectedSaleLine(new object());

                Device.BeginInvokeOnMainThread(async () =>
                await Application.Current.MainPage.Navigation.PushAsync(new SalesItemDetailView(this)));
            }
        }

        private void ExecuteDeleteSaleLine(object sender)
        {
            ((TrnSalesLine) sender).IsDeleted = true; 
            ReloadSalesLines();
        }

        public void ExecuteSelectCustomer(object obj)
        {
            if (SelectedCustomer != null)
            {
                SelectedSale.CustomerId = SelectedCustomer.Id;
            }
        }

        private void ExecuteSave()
        {
            var isTaskRun = false;

            IsProcessingAPI = true;

            Task.Run(async () =>
            {
                Thread.Sleep(1000);

                SelectedSale.Amount = SelectedSale.TrnSalesLines.Sum(x => x.Amount);

                SelectedSaleId = await ApiRequest<TrnSales, TrnSales>
                    .Save("TrnSales/Save", SelectedSale);

                IsProcessingAPI = false;

                Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage.DisplayAlert(Title, "Record saved.", "Ok"));

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

                                    await ApiRequest<MstItem, MstItem>
                                        .Delete("TrnSales/Delete", SelectedSale.Id);

                                    IsProcessingAPI = false;

                                    Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(Title, "Record deleted.",
                                            "Ok"));
                                    Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PopAsync());
                                });
                            }
                        },
                        TaskScheduler.FromCurrentSynchronizationContext())
                    );
        }
        #endregion

        #region Other Methods
        public void ExecuteShowItems()
        {
            Device.BeginInvokeOnMainThread(async () =>
                await Application.Current.MainPage.Navigation.PushModalAsync(new SalesItemView(this)));
        }

        public void ExecuteSelectItemByBarCode()
        {
            if (!string.IsNullOrEmpty(SearchBarcode))
            {
                ItemFrom = ItemFrom.Item;

                IsChanged = false;
                SelectedItem = Items.SingleOrDefault(x => x.BarCode == SearchBarcode);

                SelectedSaleLine = new TrnSalesLine()
                {
                    ItemId = SelectedItem.Id,
                    ItemDescription = SelectedItem.ItemDescription,
                    BarCode = SelectedItem.BarCode,
                    UnitId = SelectedItem.UnitId,
                    UnitName = SaleUnits?.SingleOrDefault(x => x.Id == SelectedItem.UnitId)?.Unit ?? "Unit(s)",
                    Quantity = 1,
                    Price = SelectedItem.Price,
                    NetPrice = SelectedItem.Price,
                    Amount = SelectedItem.Price,
                    SalesLineTimeStamp = DateTime.Now
                };

                ExecuteRefreshSelectedSaleLine(new object());

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PushAsync(new SalesItemDetailView(this)));
            }
        }

        public void ExecuteSelectItemByContinuesBarCode()
        {
            if (!string.IsNullOrEmpty(SearchBarcode))
            {
                SelectedItem = Items.SingleOrDefault(x => x.BarCode == SearchBarcode);

                var isItemPunchedIn = SelectedSale.TrnSalesLines.Any(x => x.ItemId == SelectedItem.Id);

                if (!isItemPunchedIn)
                {
                    SelectedSaleLine = new TrnSalesLine()
                    {
                        ItemId = SelectedItem.Id,
                        ItemDescription = SelectedItem.ItemDescription,
                        BarCode = SelectedItem.BarCode,
                        UnitId = SelectedItem.UnitId,
                        UnitName = SaleUnits?.SingleOrDefault(x => x.Id == SelectedItem.UnitId)?.Unit ?? "Unit(s)",
                        Quantity = 1,
                        Price = SelectedItem.Price,
                        NetPrice = SelectedItem.Price,
                        Amount = SelectedItem.Price,
                        SalesLineTimeStamp = DateTime.Now
                    };

                    SelectedSale.TrnSalesLines.Add(SelectedSaleLine);
                }
                else
                {
                    SelectedSaleLine = SelectedSale.TrnSalesLines.SingleOrDefault(x => x.ItemId == SelectedItem.Id);

                    if (SelectedSaleLine != null)
                    {
                        SelectedSaleLine.Quantity = SelectedSaleLine.Quantity + 1;
                        SelectedSaleLine.Amount = SelectedSaleLine.Quantity * SelectedSaleLine.NetPrice;
                    }
                }

                ExecuteRefreshSelectedSaleLine(new object());
                ReloadSalesLines();
            }
        }

        public void LoadSalesLine()
        {
            OnPropertyChanged(nameof(SaleUnits));

            SelectedSale?.TrnSalesLines.ForEach(y =>
            {
                var item = Items.SingleOrDefault(z => z.Id == y.ItemId);

                y.ItemDescription = item?.ItemDescription;
                y.BarCode = item?.BarCode;
            });

            SelectedSale?.TrnSalesLines.ForEach(y =>
            {
                var unit = SaleUnits.SingleOrDefault(z => z.Id == y.UnitId);

                y.UnitName = unit?.Unit;
            });
        }

        public void ReloadSalesLines()
        {
            var saleLines = SelectedSale.TrnSalesLines
                .Where(x => x.IsDeleted == false)
                .ToList();
            SelectedSale.TrnSalesLines = new List<TrnSalesLine>();

            saleLines.ForEach(line =>
            {
                SelectedSale.TrnSalesLines.Add(line);
            });
             
            ExecuteRefreshSelectedSale(new object());
        }
        #endregion
    }
}