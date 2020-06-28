using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using mPOS.POCO;
using mPOSv2.Services;
using mPOSv2.Views.Setup;
using mPOSv2.Views.Setup.Item;
using Xamarin.Forms;

namespace mPOSv2.ViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        #region Initialize
        public void Load(object sender = null)
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

                ItemUnits = await APIItemRequest.GetUnits();
                ItemCategories = await APIItemRequest.GetItemCategories();

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

        public string SearchItemEntry
        {
            get => _SearchItemEntry;
            set => SetProperty(ref _SearchItemEntry, value);
        }
        private string _SearchItemEntry;

        public bool IsChanged { get; set; }

        public string Title => $"{SelectedItem.ItemDescription ?? "New Item"}'s Detail";

        public long SelectedItemId
        {
            get => _SelectedItemId == 0 ? SelectedItem.Id : _SelectedItemId;
            set => SetProperty(ref _SelectedItemId, value);
        }
        private long _SelectedItemId;

        public MstUnit SelectedUnit
        {
            get => _SelectedUnit;
            set => SetProperty(ref _SelectedUnit, value);

        }
        private MstUnit _SelectedUnit;

        public string SelectedCategory
        {
            get => _SelectedCategory;
            set => SetProperty(ref _SelectedCategory, value);

        }
        private string _SelectedCategory;

        public ObservableCollection<MstUnit> ItemUnits
        {
            get => _ItemUnits;
            set => SetProperty(ref _ItemUnits, value);
        }
        private ObservableCollection<MstUnit> _ItemUnits;

        public ObservableCollection<string> ItemCategories
        {
            get => _ItemCategories;
            set => SetProperty(ref _ItemCategories, value);
        }
        private ObservableCollection<string> _ItemCategories;

        public MstItem SelectedItem
        {
            get => _SelectedItem;
            set => _SelectedItem = value;

        }
        private MstItem _SelectedItem;

        public ObservableCollection<MstItem> Items
        {
            get => _Items;
            set => SetProperty(ref _Items, value);
        }
        private ObservableCollection<MstItem> _Items;
        #endregion

        #region Commands
        public Command RefreshItems
        {
            get => _RefreshItems ?? (_RefreshItems = new Command(Load, (x) => true));
            set => SetProperty(ref _RefreshItems, value);
        }
        private Command _RefreshItems;

        public Command RefreshSelectedItem
        {
            get => _RefreshSelectedItem ?? (_RefreshSelectedItem = new Command(ExecuteRefreshSelectedItem, (x) => true));
            set => SetProperty(ref _RefreshSelectedItem, value);
        }
        private Command _RefreshSelectedItem;

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

        public Command SelectUnit
        {
            get => _SelectUnit ?? (_SelectUnit = new Command(ExecuteSelectUnit, (x) => true));
            set => SetProperty(ref _SelectUnit, value);
        }
        private Command _SelectUnit;

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
        public void ExecuteRefreshSelectedItem(object sender)
        {
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(Title));
        }

        private void ExecuteAdd(object sender)
        {
            var newItem = new MstItem();

            Items.Add(newItem);
            SelectedItem = newItem;

            Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PushAsync(new ItemDetailView(this)));
        }

        private void ExecuteSearch(object sender)
        {
            Load(SearchItemEntry);
        }

        public void ExecuteSelectUnit(object sender)
        {
            if (SelectedUnit != null)
            {
                SelectedItem.UnitId = SelectedUnit.Id;
            }
        }

        private void ExecuteSelectItem(object sender)
        {
            if (sender is MstItem selectedItem)
            {
                IsChanged = false;
                SelectedItem = selectedItem;
                SelectedUnit = ItemUnits.SingleOrDefault(y => y.Id == SelectedItem.UnitId);

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PushAsync(new ItemDetailView(this)));
            }
        }

        private void ExecuteSave()
        {
            var isTaskRun = false;

            IsProcessingAPI = true;

            Task.Run(async () =>
            {
                Thread.Sleep(1000);

                SelectedItemId = await ApiRequest<MstItem, MstItem>
                    .Save("MstItem/Save", SelectedItem);

                IsProcessingAPI = false;

                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(Title, "Record saved.", "Ok"));

                isTaskRun = true;
            });

            if (!isTaskRun)
            {
                if ((SelectedItem.ItemCode?.Length ?? 0) < 2)
                {
                    SelectedItem.ItemCode = "NA";
                    OnPropertyChanged(nameof(SelectedItem));

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
                                    .Delete("MstItem/Delete", SelectedItem.Id);

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
