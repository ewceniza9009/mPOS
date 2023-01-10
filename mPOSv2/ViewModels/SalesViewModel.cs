﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Humanizer;
using mPOS.POCO;
using mPOSv2.Enums;
using mPOSv2.Models.Page;
using mPOSv2.Services;
using mPOSv2.Views.Activity.Sales;
using Xamarin.Forms;
using Xamarin.Forms.Internals;


namespace mPOSv2.ViewModels
{
    public class SalesViewModel : ViewModelBase
    {
        #region Initialize
        public void Load(object sender = null)
        {
            var search = sender?.ToString() ?? string.Empty;

            IsBusy = true;

            SearchSaleDate = GlobalVariables.TempSearchSalesDate ?? SettingsRepository.GetSettings().TempSearchSalesDate ?? DateTime.Now.Date;

            Task.Run(async () =>
            {
                TrnSalesFilter salesFilter = null;

                if (!string.IsNullOrEmpty(search) || SearchSaleDate != null)
                    salesFilter = new TrnSalesFilter
                    {
                        SalesNumber = search,
                        SalesDate = (DateTime)SearchSaleDate,
                        FilterMethods = new FilterMethods
                        {
                            Operations = new List<FilterOperation>
                            {
                                new FilterOperation("SalesNumber", Operation.Contains),
                                new FilterOperation("SalesDate", Operation.Equals)
                            }
                        }
                    };

                Customers = await APISalesRequest.GetCustomers();
                Terms = await APISalesRequest.GetTerms();

                Sales = await ApiRequest<TrnSalesFilter, ObservableCollection<TrnSales>>
                    .PostRead("TrnSales/BulkGet", salesFilter);

                Sales.ForEach(x =>
                {
                    x.CustomerName = Customers
                        .SingleOrDefault(c => c.Id == x.CustomerId)
                        ?.Customer;
                });

                OnPropertyChanged(nameof(Sales));
                OnPropertyChanged(nameof(IsListEmpty));
                OnPropertyChanged(nameof(IsListNotEmpty));

                IsBusy = false;

                Sales.CollectionChanged += Sales_CollectionChanged;

                Pager.PageSize = Services.SettingsRepository.GetSalesLinePageSize();
            });
        }

        public void LoadItems(object sender = null)
        {
            var search = sender?.ToString() ?? string.Empty;

            IsBusy = true;

            Task.Run(async () =>
            {
                MstItemFilter itemFilter = null;

                if (!string.IsNullOrEmpty(search))
                    itemFilter = new MstItemFilter
                    {
                        ItemDescription = search,
                        FilterMethods = new FilterMethods
                        {
                            Operations = new List<FilterOperation>
                            {
                                new FilterOperation("ItemDescription", Operation.Contains)
                            }
                        }
                    };

                Items = await ApiRequest<MstItemFilter, ObservableCollection<MstItem>>
                    .PostRead("MstItem/BulkGet", itemFilter);

                IsBusy = false;
            });
        }
        #endregion

        #region Events
        private void Sales_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                IsCollectionChanged = true;
            }
        }
        #endregion

        #region Properties
        public bool IsListEmpty
        {
            get
            {
                var result = false;

                if (Sales != null) result = !Sales.Any();

                return result;
            }
        }
        public bool IsListNotEmpty
        {
            get
            {
                var result = false;

                if (Sales != null) result = Sales.Any();

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
            get => _SearchSaleDate;
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

        public string Title => $"SI #: {SelectedSale.SalesNumber ?? "(New)"}";
        
        public string TenderTitle => $"TENDER - {SelectedSale.SalesNumber ?? "(New)"}";

        public bool ShowSalesLinesPagerButtons => (decimal)(SelectedSale?.TrnSalesLines?.Count ?? Pager.PageSize) / Pager.PageSize > 1m;

        public List<string> CreditCardTypes 
        {
            get 
            {
                return new List<string>
                {
                    "American Express",
                    "Diners Club",
                    "Discover",
                    "JCB",
                    "Master Card",
                    "Union Pay",
                    "Visa",
                    "Others"
                };
            }
        }

        public bool IsBarcodeModalShown = false;
        public bool IsSalesChargeModalShown = false;
        public bool IsSaved = false;

        public bool CanEditDiscount 
        {
            get => _IsDiscountAmountEnabled;
            set => SetProperty(ref _IsDiscountAmountEnabled, value);
        }
        private bool _IsDiscountAmountEnabled;

        public long SelectedCollectionId
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

        public MstTax SelectedTax
        {
            get => _SelectedTax;
            set => SetProperty(ref _SelectedTax, value);
        }
        private MstTax _SelectedTax;

        public ObservableCollection<MstTax> Taxes
        {
            get => _Taxes;
            set => SetProperty(ref _Taxes, value);
        }
        private ObservableCollection<MstTax> _Taxes;

        public MstTerm SelectedTerm
        {
            get => _SelectedTerm;
            set => SetProperty(ref _SelectedTerm, value);
        }
        private MstTerm _SelectedTerm;

        public ObservableCollection<MstTerm> Terms
        {
            get => _Terms;
            set => SetProperty(ref _Terms, value);
        }
        private ObservableCollection<MstTerm> _Terms;

        public MstDiscount SelectedDiscount
        {
            get => _SelectedDiscount;
            set => SetProperty(ref _SelectedDiscount, value);
        }
        private MstDiscount _SelectedDiscount;

        public ObservableCollection<MstDiscount> Discounts
        {
            get => _Discounts;
            set => SetProperty(ref _Discounts, value);
        }
        private ObservableCollection<MstDiscount> _Discounts;

        public ObservableCollection<MstPayType> PayTypes 
        {
            get => _PayTypes; 
            set => SetProperty(ref _PayTypes, value);
        }
        private ObservableCollection<MstPayType> _PayTypes;

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
            set => SetProperty(ref _SelectedSale, value);
        }
        private TrnSales _SelectedSale;

        public PropertyChangeTracker SelectedSaleTracker 
        {
            get => _SelectedSaleTracker;
            set => SetProperty(ref _SelectedSaleTracker, value);
        }
        private PropertyChangeTracker _SelectedSaleTracker;

        public bool IsCollectionChanged 
        {
            get => _IsCollectionChanged;
            set => SetProperty(ref _IsCollectionChanged, value);
        }
        private bool _IsCollectionChanged = false;

        public TrnSalesLine SelectedSaleLine { get; set; }

        public ObservableCollection<TrnSales> Sales
        {
            get => _Sales;
            set => SetProperty(ref _Sales, value);
        }
        private ObservableCollection<TrnSales> _Sales;

        public ObservableCollection<TrnSalesLine> SalesLines
        {
            get => _SalesLines;
            set => SetProperty(ref _SalesLines, value);
        }
        private ObservableCollection<TrnSalesLine> _SalesLines;

        public TrnCollection NewTender
        {
            get => _NewTender;
            set => SetProperty(ref _NewTender, value);
        }
        private TrnCollection _NewTender;

        public TrnCollectionLine SelectedCollectionLine
        {
            get => _SelectedCollectionLine;
            set => SetProperty(ref _SelectedCollectionLine, value);
        }
        private TrnCollectionLine _SelectedCollectionLine;
        #endregion

        #region Commands
        public Command RefreshSales
        {
            get => _RefreshSales ?? (_RefreshSales = new Command(Load, x => true));
            set => SetProperty(ref _RefreshSales, value);
        }
        private Command _RefreshSales;

        public Command RefreshItems
        {
            get => _RefreshItems ?? (_RefreshItems = new Command(LoadItems, x => true));
            set => SetProperty(ref _RefreshItems, value);
        }
        private Command _RefreshItems;

        public Command RefreshSelectedSale
        {
            get => _RefreshSelectedSale ??
                   (_RefreshSelectedSale = new Command(ExecuteRefreshSelectedSale, x => true));
            set => SetProperty(ref _RefreshSelectedSale, value);
        }
        private Command _RefreshSelectedSale;

        public Command SelectCustomer
        {
            get => _SelectCustomer ?? (_SelectCustomer = new Command(ExecuteSelectCustomer, x => true));
            set => SetProperty(ref _SelectCustomer, value);
        }
        private Command _SelectCustomer;

        public Command Add
        {
            get => _Add ?? (_Add = new Command(ExecuteAdd, x => true));
            set => SetProperty(ref _Add, value);
        }
        private Command _Add;

        public Command Search
        {
            get => _Search ?? (_Search = new Command(ExecuteSearch, x => true));
            set => SetProperty(ref _Search, value);
        }
        private Command _Search;

        public Command SearchItem
        {
            get => _SearchItem ?? (_SearchItem = new Command(ExecuteSearchItem, x => true));
            set => SetProperty(ref _SearchItem, value);
        }
        private Command _SearchItem;

        public Command SelectSale
        {
            get => _SelectSale ?? (_SelectSale = new Command(ExecuteSelectSale, x => true));
            set => SetProperty(ref _SelectSale, value);
        }
        private Command _SelectSale;

        public Command SelectSaleLine
        {
            get => _SelectSaleLine ?? (_SelectSaleLine = new Command(ExecuteSelectItem, x => true));
            set => SetProperty(ref _SelectSaleLine, value);
        }
        private Command _SelectSaleLine;

        public Command DeleteSaleLine
        {
            get => _DeleteSaleLine ?? (_DeleteSaleLine = new Command(ExecuteDeleteSaleLine, x => true));
            set => SetProperty(ref _DeleteSaleLine, value);
        }
        private Command _DeleteSaleLine;

        public Command SelectItem
        {
            get => _SelectItem ?? (_SelectItem = new Command(ExecuteSelectItem, x => true));
            set => SetProperty(ref _SelectItem, value);
        }
        private Command _SelectItem;

        public Command Save
        {
            get => _Save ?? (_Save = new Command(ExecuteSave, () => SelectedSale?.IsNotTendered ?? true));
            set => SetProperty(ref _Save, value);
        }
        private Command _Save;

        public Command Delete
        {
            get => _Delete ?? (_Delete = new Command(ExecuteDelete, () => SelectedSale?.IsNotTendered ?? true));
            set => SetProperty(ref _Delete, value);
        }
        private Command _Delete;

        public Command Tender
        {
            get => _Tender ?? (_Tender = new Command(ExecuteTender, x => true));
            set => SetProperty(ref _Tender, value);
        }
        private Command _Tender;

        public Command SaveTender
        {
            get => _SaveTender ?? (_SaveTender = new Command(ExecuteSaveTender, x => true));
            set => SetProperty(ref _SaveTender, value);
        }
        private Command _SaveTender;

        public Command SelectPayType
        {
            get => _SelectPayType ?? (_SelectPayType = new Command(ExecuteSelectPayType, x => true));
            set => SetProperty(ref _SelectPayType, value);
        }
        private Command _SelectPayType;

        public ICommand ReprintOR => new Command(OnReprintOR);
        #endregion

        #region Methods
        #region Executes
        public void ExecuteRefreshSelectedSale(object sender)
        {
            OnPropertyChanged(nameof(SelectedSale));
            OnPropertyChanged(nameof(SearchBarcode));
            OnPropertyChanged(nameof(Title));
        }

        public void ExecuteRefreshSelectedSaleLine()
        {
            OnPropertyChanged(nameof(SelectedSaleLine));
            OnPropertyChanged(nameof(SearchBarcode));
        }

        private async void ExecuteAdd(object sender)
        {
            await InitializeControlLookUpSource();

            var newSale = new TrnSales
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
            GlobalVariables.TempSearchSalesDate = SearchSaleDate;

            Load(SearchSaleEntry);

            OnPropertyChanged(nameof(Sales));
            OnPropertyChanged(nameof(IsListEmpty));
            OnPropertyChanged(nameof(IsListNotEmpty));
        }

        public void ExecuteSearchItem(object sender)
        {
            LoadItems(SearchItemEntry);
        }

        private async void ExecuteSelectSale(object sender)
        {
            await InitializeControlLookUpSource();

            if (sender is TrnSales selectedSale)
            {
                IsChanged = false;
                IsSaved = true;
                SelectedSale = selectedSale;
                SelectedCustomer = Customers.SingleOrDefault(y => y.Id == SelectedSale.CustomerId);
                SelectedTerm = Terms.SingleOrDefault(y => y.Id == SelectedSale.TermId);

                GlobalVariables.TempSearchSalesDate = SearchSaleDate;

                LoadSalesLine();

                SelectedSaleTracker = new PropertyChangeTracker(SelectedSale);
                
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
                SelectedTax = Taxes.SingleOrDefault(x => x.Id == SelectedItem.OutTaxId);
                SelectedDiscount = Discounts.FirstOrDefault();

                var taxAmount = 0m;
                var amount = selectedItem.Price;

                SelectedSaleLine = new TrnSalesLine
                {
                    ItemId = SelectedItem.Id,
                    ItemDescription = SelectedItem.ItemDescription,
                    BarCode = SelectedItem.BarCode,
                    DiscountId = SelectedDiscount.Id,
                    UnitId = SelectedItem.UnitId,
                    UnitName = SaleUnits?.SingleOrDefault(x => x.Id == SelectedItem.UnitId)?.Unit ?? "Unit(s)",
                    Quantity = 1,
                    Price = SelectedItem.Price,
                    NetPrice = SelectedItem.Price,
                    TaxId = SelectedTax.Id,
                    TaxRate = SelectedTax.Rate,
                    TaxAmount = taxAmount,
                    TaxAccountId = SelectedTax.AccountId,
                    Amount = amount,
                    SalesLineTimeStamp = DateTime.Now
                };

                ComputeVatAmount();

                ExecuteRefreshSelectedSaleLine();

                Device.BeginInvokeOnMainThread( async () => await Application.Current.MainPage.Navigation.PopAsync());
                Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage.Navigation.PushAsync(new SalesItemDetailView(this)));
            }
            else
            {
                ItemFrom = ItemFrom.SaleLine;

                SelectedSaleLine = sender as TrnSalesLine;
                SelectedTax = Taxes.SingleOrDefault(x => x.Id == SelectedSaleLine.TaxId);
                SelectedDiscount = Discounts.SingleOrDefault(x => x.Id == SelectedSaleLine.DiscountId);

                ExecuteRefreshSelectedSaleLine();

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PushAsync(new SalesItemDetailView(this)));
            }
        }

        private void ExecuteDeleteSaleLine(object sender)
        {
            ((TrnSalesLine)sender).IsDeleted = true;
            ReloadSalesLines();
        }

        public void ExecuteSelectCustomer(object obj)
        {
            if (SelectedCustomer != null) SelectedSale.CustomerId = SelectedCustomer.Id;
        }

        private void ExecuteSave()
        {
            var isTaskRun = false;

            IsProcessingAPI = true;

            Task.Run(async () =>
            {
                Thread.Sleep(1000);

                RefreshSelectedSaleDuringSave();

                SelectedCollectionId = await ApiRequest<TrnSales, TrnSales>
                    .Save("TrnSales/Save", SelectedSale);

                if (SelectedSale.Id == 0)
                {
                    var sale = await ApiRequest<TrnSales, TrnSales>
                        .Read("TrnSales/Get", SelectedCollectionId);

                    SelectedSale = sale;

                    IsSaved = true;

                    OnPropertyChanged(nameof(Title));
                }

                IsProcessingAPI = false;

                IsCollectionChanged = false;
                SelectedSaleTracker?.ChangedProperties?.Clear();

                Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage.DisplayAlert(Title, "Record saved.", "Ok"));

                isTaskRun = true;
            });

            if (!isTaskRun)
                if ((SelectedSale.SalesNumber?.Length ?? 0) < 2)
                {
                    OnPropertyChanged(nameof(SelectedSale));

                    IsProcessingAPI = false;
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

                                    IsCollectionChanged = false;
                                    SelectedSaleTracker?.ChangedProperties?.Clear();

                                    Device.BeginInvokeOnMainThread(async () =>
                                        await Application.Current.MainPage.DisplayAlert(Title, "Record deleted.",
                                            "Ok"));
                                    Device.BeginInvokeOnMainThread(async () =>
                                        await Application.Current.MainPage.Navigation.PopAsync());
                                });
                            }
                        },
                        TaskScheduler.FromCurrentSynchronizationContext())
            );
        }

        public void ExecuteTender(object sender)
        {
            NewTender = new TrnCollection
            {
                CustomerId = SelectedSale.CustomerId,
                CollectionDate = DateTime.Now.Date,
                SalesId = SelectedSale.Id,
                SalesBalanceAmount = SelectedSale.Amount,
                Amount = SelectedSale.Amount,
                TenderAmount = 0,
                ChangeAmount = SelectedSale.Amount * -1,
                TrnCollectionLines = new List<TrnCollectionLine>()
            };

            foreach (var item in PayTypes) 
            {
                NewTender.TrnCollectionLines.Add(new TrnCollectionLine
                {
                    Amount = 0,
                    PayTypeId = item.Id,
                    MstPayType = item,
                    AccountId = (int)item.AccountId
                });
            }

            Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage.Navigation.PushAsync(new SalesTender(this)));
        }

        private void ExecuteSaveTender(object obj)
        {
            if (!IsSaved)
            {
                Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage
                        .DisplayAlert(Title, "Please save the invoice?", "OK")
                );

                return;
            }

            if (NewTender.TenderAmount > 0)
            {
                Task.Run(async () =>
                {
                    Thread.Sleep(1000);

                    SelectedCollectionId = await ApiRequest<TrnCollection, TrnCollection>
                        .Save("TrnSales/Tender", NewTender);

                    SelectedSale.IsNotTendered = false;

                    OnPropertyChanged(nameof(SelectedSale));

                    IsCollectionChanged = false;
                    SelectedSaleTracker?.ChangedProperties?.Clear();

                });

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PopAsync());

                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new SalesTenderPrintContainer(SelectedSale.Id));
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(Title, "Not applicable.", "Ok"));
            }
        }

        private void ExecuteSelectPayType(object obj)
        {
            SelectedCollectionLine = NewTender.TrnCollectionLines.FirstOrDefault(x => x.PayTypeId == (obj as MstPayType).Id);

            SelectedCollectionLine.IsCheckSelected = false;
            SelectedCollectionLine.IsCCSelected = false; 
            SelectedCollectionLine.IsGCSelected = false;
            SelectedCollectionLine.IsExchangeSelected = false;

            SelectedCollectionLine.IsOtherSelected = false;

            switch (SelectedCollectionLine.MstPayType.PayType)
            {
                case "Cash":
                    return;
                case "Check":
                    SelectedCollectionLine.IsCheckSelected = true;
                    SelectedCollectionLine.CheckNumber = "NA";
                    SelectedCollectionLine.CheckDate = DateTime.Now.Date;
                    SelectedCollectionLine.CheckBank = "NA";
                    break;
                case "Credit Card":
                    SelectedCollectionLine.IsCCSelected = true;
                    SelectedCollectionLine.CreditCardBank = "NA";
                    SelectedCollectionLine.CreditCardNumber = "NA";
                    SelectedCollectionLine.CreditCardType = "Master Card";
                    SelectedCollectionLine.CreditCardVerificationCode = "NA";
                    break;
                case "Gift Certificate":
                    SelectedCollectionLine.IsGCSelected = true;
                    SelectedCollectionLine.GiftCertificateNumber = "NA";
                    break;
                case "Exchange":
                    SelectedCollectionLine.IsExchangeSelected = true;
                    Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(Title, "Cannot accept exchange right now.", "Ok"));
                    return;
                case "Rewards":
                    SelectedCollectionLine.IsOtherSelected = true;
                    SelectedCollectionLine.OtherInformation = "NA";
                    break;
            }

            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new SalesTenderLine(this));
        }

        private void OnReprintOR(object obj)
        {
            int orId = 0;

            Task.Run(async () =>
            {
                orId = await APIOfficialReceipt.GetCollectionId((int)SelectedSale?.Id);

                if (orId > 0)
                {
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new SalesTenderPrintContainer(SelectedSale.Id));
                }

                if (orId == 0)
                {
                    Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(Title, $"Not applicable, {SelectedSale.SalesNumber} is not tendered.", "Ok"));
                }
            });

            
        }
        #endregion

        private async Task InitializeControlLookUpSource()
        {
            Items = await APISalesRequest.GetItems();
            SaleUnits = await APISalesRequest.GetUnits();
            Taxes = await APISalesRequest.GetTaxes();
            Discounts = await APISalesRequest.GetDiscounts();
            PayTypes = await APISalesRequest.GetPayTypes();
        }

        public decimal ComputeAmount()
        {
            return Math.Round(SelectedSaleLine.NetPrice, 2) * Math.Round(SelectedSaleLine.Quantity, 2);
        }

        public decimal ComputeVatAmount() 
        {
            decimal result;
            if (SelectedTax.Code == "INCLUSIVE")
            {
                result = Math.Round(SelectedSaleLine.Price / (1 + SelectedTax.Rate / 100) * (SelectedTax.Rate / 100), 2);
            }
            else
            {
                result = Math.Round(SelectedSaleLine.Price * (SelectedTax.Rate / 100), 2);
                SelectedSaleLine.Amount = Math.Round(SelectedSaleLine.Price + result, 2);
            }

            return result;
        }
        #endregion

        #region Other Methods
        public void ExecuteShowItems()
        {
            Device.BeginInvokeOnMainThread(async () =>
                await Application.Current.MainPage.Navigation.PushAsync(new SalesItemView(this)));
        }

        public void ExecuteSelectItemByBarCode()
        {
            if (!string.IsNullOrEmpty(SearchBarcode))
            {
                ItemFrom = ItemFrom.Item;

                IsBarcodeModalShown = false;

                IsChanged = false;
                SelectedItem = Items.SingleOrDefault(x => x.BarCode == SearchBarcode);
                SelectedTax = Taxes.SingleOrDefault(x => x.Id == SelectedItem.OutTaxId);

                var taxAmount = 0m;
                var amount = SelectedItem?.Price ?? 0;

                if (SelectedTax != null && SelectedTax.Code == "INCLUSIVE")
                {
                    taxAmount = Math.Round(SelectedItem?.Price ?? 0 / (1 + SelectedTax.Rate / 100) * (SelectedTax.Rate / 100), 2);
                }
                else
                {
                    taxAmount = Math.Round(SelectedItem?.Price ?? 0 * ((SelectedTax?.Rate ?? 0)/ 100), 2);
                    amount = Math.Round(SelectedItem?.Price ?? 0 + taxAmount, 2);
                }

                SelectedSaleLine = new TrnSalesLine
                {
                    ItemId = SelectedItem?.Id ?? 0,
                    ItemDescription = SelectedItem?.ItemDescription,
                    BarCode = SelectedItem?.BarCode,
                    UnitId = SelectedItem?.UnitId ?? 0,
                    UnitName = SaleUnits?.SingleOrDefault(x => x.Id == SelectedItem.UnitId)?.Unit ?? "Unit(s)",
                    Quantity = 1,
                    Price = SelectedItem?.Price ?? 0,
                    NetPrice = SelectedItem?.Price ?? 0,
                    TaxId = SelectedTax?.Id ?? 0,
                    TaxRate = SelectedTax?.Rate ?? 0,
                    TaxAmount = taxAmount,
                    TaxAccountId = SelectedTax?.AccountId ?? 0,
                    Amount = amount,
                    SalesLineTimeStamp = DateTime.Now
                };

                ExecuteRefreshSelectedSaleLine();

                Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage.Navigation.PushAsync(new SalesItemDetailView(this)));
            }
        }

        public void ExecuteSelectItemByContinuesBarCode()
        {
            if (!string.IsNullOrEmpty(SearchBarcode))
            {
                SelectedItem = Items.SingleOrDefault(x => x.BarCode == SearchBarcode);
                SelectedTax = Taxes.SingleOrDefault(x => x.Id == SelectedItem.OutTaxId);

                var isItemPunchedIn = SelectedSale.TrnSalesLines.Any(x => x.ItemId == SelectedItem.Id);

                if (!isItemPunchedIn)
                {
                    var taxAmount = 0m;
                    var amount = SelectedItem?.Price ?? 0;

                    if (SelectedTax?.Code == "INCLUSIVE")
                    {
                        taxAmount = Math.Round(SelectedItem?.Price ?? 0 / (1 + SelectedTax.Rate / 100) * (SelectedTax.Rate / 100), 2);
                    }
                    else
                    {
                        taxAmount = Math.Round(SelectedItem?.Price ?? 0 * ((SelectedTax?.Rate ?? 0) / 100), 2);
                        amount = Math.Round((SelectedItem?.Price ?? 0) + taxAmount, 2);
                    }

                    SelectedSaleLine = new TrnSalesLine
                    {
                        ItemId = SelectedItem?.Id ?? 0,
                        ItemDescription = SelectedItem?.ItemDescription,
                        BarCode = SelectedItem?.BarCode,
                        UnitId = SelectedItem?.UnitId ?? 0,
                        UnitName = SaleUnits?.SingleOrDefault(x => x.Id == SelectedItem.UnitId)?.Unit ?? "Unit(s)",
                        Quantity = 1,
                        Price = SelectedItem?.Price ?? 0,
                        NetPrice = SelectedItem?.Price ?? 0,
                        TaxId = SelectedTax?.Id ?? 0,
                        TaxRate = SelectedTax?.Rate ?? 0,
                        TaxAmount = taxAmount,
                        TaxAccountId = SelectedTax?.AccountId ?? 0,
                        Amount = amount,
                        SalesLineTimeStamp = DateTime.Now
                    };

                    SelectedSale.TrnSalesLines.Add(SelectedSaleLine);
                }
                else
                {
                    SelectedSaleLine = SelectedSale.TrnSalesLines.SingleOrDefault(x => x.ItemId == SelectedItem.Id);

                    if (SelectedSaleLine != null) 
                    {
                        var taxAmount = 0m;
                        var quantity = SelectedSaleLine.Quantity + 1;
                        var amount = quantity * SelectedSaleLine?.NetPrice ?? 0;

                        if (SelectedTax?.Code == "INCLUSIVE")
                        {
                            taxAmount = Math.Round(amount / (1 + SelectedTax.Rate / 100) * (SelectedTax.Rate / 100), 2);
                        }
                        else
                        {
                            taxAmount = Math.Round(amount * ((SelectedTax?.Rate ?? 0) / 100), 2);
                            amount = Math.Round(amount + taxAmount, 2);
                        }

                        if (SelectedSaleLine != null)
                        {
                            SelectedSaleLine.Quantity = quantity;
                            SelectedSaleLine.TaxAmount = taxAmount;
                            SelectedSaleLine.Amount = amount;
                        }
                    }
                }

                ExecuteRefreshSelectedSaleLine();
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

            Pager.EndPage = GetEndPage();

            SalesLines = GetSalesLines(Pager.Start, Pager.PageSize);
        }

        public void RefreshSelectedSaleDuringSave() 
        {
            SelectedSale.Amount = SelectedSale.TrnSalesLines.Sum(x => x.Amount);

            if (SelectedSale.TrnSalesLines != null && SelectedSale.TrnSalesLines.Count > 0)
            {
                SelectedSale.TrnSalesLines.ForEach(x => x.MstItem = null);
            }
        }

        public ObservableCollection<TrnSalesLine> GetSalesLines(int start, int pageSize)
        {
            var result = new ObservableCollection<TrnSalesLine>();
            var data = SelectedSale.TrnSalesLines.Skip(start).Take(pageSize);

            data.ForEach(salesLine => 
            {
                salesLine.ItemDescriptionDisplay = salesLine.ItemDescription.Truncate(30, "...");
                result.Add(salesLine);
            });

            return result;
        }

        public void ReloadSalesLines()
        {
            var saleLines = SelectedSale.TrnSalesLines
                .Where(x => x.IsDeleted == false)
                .ToList();
            SelectedSale.TrnSalesLines = new List<TrnSalesLine>();

            saleLines.ForEach(line => 
            {
                line.ItemDescription = line.ItemDescription.Truncate(30, "...");
                SelectedSale.TrnSalesLines.Add(line);
            });

            ExecuteRefreshSelectedSale(new object());

            Pager.EndPage = GetEndPage();

            SalesLines = GetSalesLines(Pager.Start, Pager.PageSize);

            OnPropertyChanged(nameof(ShowSalesLinesPagerButtons));
        }

        public void RefreshTender() 
        {
            OnPropertyChanged(nameof(NewTender));
        }

        public double GetEndPage()
        {
            var result = SelectedSale.TrnSalesLines.Any() ? 
                SelectedSale.TrnSalesLines.Count() / (double) Pager.PageSize : 
                0;

            return Math.Ceiling(result);
        }
        #endregion
    }
}