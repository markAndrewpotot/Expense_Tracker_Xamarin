using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Expense_Tracking_Xamarin.Models;
using Expense_Tracking_Xamarin.View;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class RecordPageViewModel : INotifyPropertyChanged
    {
        private string token;
        private const int PageSize = 10;
        public RecordClass record;
        public Pagination pagination;
        public static ObservableCollection<Record> thislist = new ObservableCollection<Record>();
        ObservableCollection<Record> listrecord = new ObservableCollection<Record>();
        ObservableCollection<Record> Records { get => listrecord; set { listrecord = value; OnPropertyChange(nameof(Records)); } }

        public InfiniteScrollCollection<Record> thisRecord { set; get; }

        #region Constructor
        public RecordPageViewModel()
        {
            searchnot = "Search";
            enable = false;
            dispTitle = true;
            isbusy = false;

            thisRecord = new InfiniteScrollCollection<Record>
            {
                OnLoadMore = async () =>
                {
                    load = true;

                    // load the next page
                    var page = thisRecord.Count / PageSize;

                    var items = GetItems(page, PageSize);

                    load = false;

                    // return the items that need to be added
                    return thisRecord;
                },
                OnCanLoadMore = () =>
                {
                    return thisRecord.Count < 44;
                }
            };

            DownloadDataAsync();

        }
        #endregion

        /*------------------------------------------------------------------------------------------------------------------------------*/

        #region On Property Change Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChange(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion

        #region PassRecords()
        public ObservableCollection<Record> PassRecords()
        {
            return thislist;
        }
        #endregion

        #region DisplayRecords
        public async void displayRecords()
        {
            listrecord.Clear();
            Records.Clear();
            thislist.Clear();

            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.txt");
            token = File.ReadAllText(filename);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("http://expenses.koda.ws/api/v1/records/");

            var result = await response.Content.ReadAsStringAsync();
            record = JsonConvert.DeserializeObject<RecordClass>(result);

            pagination = record.pagination;

            for(int i = 1; i <= pagination.pages; i++)
            {
                GetRecords(token, i);
            }

            isbusy = false;
        }
        #endregion

        #region Get Records
        public async void GetRecords(string token, int page)
        {
            string textColor = string.Empty;
            string icon = string.Empty;

            string uri = string.Concat("http://expenses.koda.ws/api/v1/records?page=",page);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(uri);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                record = JsonConvert.DeserializeObject<RecordClass>(result);

                for (int i = 0; i < record.records.Count; i++)
                {
                    if (record.records[i].record_type == 1)
                        textColor = "Red";
                    else
                        textColor = "Green";

                    icon = GetIconString(record.records[i].category.name);

                    listrecord.Add(new Record
                    {
                        notes = record.records[i].notes,
                        amount = record.records[i].amount,
                        category = record.records[i].category,
                        date = record.records[i].date,
                        record_type = record.records[i].record_type,
                        id = record.records[i].id,
                        txtcolor = textColor,
                        iconstring = icon
                    }) ;

                    thislist.Add(new Record
                    {
                        notes = record.records[i].notes,
                        amount = record.records[i].amount,
                        category = record.records[i].category,
                        date = record.records[i].date,
                        record_type = record.records[i].record_type,
                        id = record.records[i].id,
                        txtcolor = textColor,
                        iconstring = icon
                    });
                }
            }
        }
        #endregion

        #region Get Icon Using its String
        public string GetIconString(string name)
        {
            string retval = string.Empty;

            switch(name)
            {
                case "Food & Drinks":
                    retval = "ic_food_drinks.png";
                    break;
                case "Groceries":
                    retval = "ic_groceries.png";
                    break;
                case "Clothes":
                    retval = "ic_clothes.png";
                    break;
                case "Electronics":
                    retval = "ic_electronics.png";
                    break;
                case "Healthcare":
                    retval = "ic_healthcare.png";
                    break;
                case "Gifts":
                    retval = "ic_gifts.png";
                    break;
                case "Transportation":
                    retval = "ic_transportation.png";
                    break;
                case "Education":
                    retval = "ic_education.png";
                    break;
                case "Entertainment":
                    retval = "ic_entertainment.png";
                    break;
                case "Utilities":
                    retval = "ic_utilities.png";
                    break;
                case "Rent":
                    retval = "ic_rent.png";
                    break;
                case "Household Supplies":
                    retval = "ic_household.png";
                    break;
                case "Investments":
                    retval = "ic_Investment.png";
                    break;
                case "Other":
                    retval = "ic_other.png";
                    break;
            }

            return retval;
        }
        #endregion

        #region Handle On Selection in Listview
        private Record _rec;

        public Record EditRec
        {
            set
            {
                if (_rec == value)
                    return;
                _rec = value;
                HandleSelectedRecord();
            }
            get
            {
                return _rec;
            }
        }

        private void HandleSelectedRecord()
        {
            Application.Current.MainPage.Navigation.PushAsync(new RecordEditPage()
            {
                ID = EditRec.id,
                date = EditRec.date,
                notes = EditRec.notes,
                category = EditRec.category,
                amount = EditRec.amount,
                record_type = EditRec.record_type
            });
        }

        #endregion

        #region for title bar, search bar
        private bool _enable;
        public bool enable
        {
            get { return _enable; }
            set
            {
                if (_enable == value)
                    return;
                _enable = value;
                OnPropertyChange(nameof(enable));
            }
        }

        private bool _dispTitle;
        public bool dispTitle
        {
            get { return _dispTitle; }
            set
            {
                if (_dispTitle == value)
                    return;
                _dispTitle = value;
                OnPropertyChange(nameof(dispTitle));
            }
        }

        private string _searchnot;
        public string searchnot
        {
            get { return _searchnot; }
            set
            {
                if (_searchnot == value)
                    return;
                _searchnot = value;
                OnPropertyChange(nameof(searchnot));
            }
        }

        public ICommand showSearch
        {
            get
            {
                return new Command(()=>
                {
                    if (!enable)
                    { 
                        searchnot = "Cancel";
                        enable = true;
                        dispTitle = false;
                    }
                    else
                    {
                        searchnot = "Search";
                        enable = false;
                        dispTitle = true;
                    }
                });
            }
        }

        #endregion

        #region pull to refresh
        private bool _isbusy;
        public bool isbusy
        {
            set
            {
                if (_isbusy == value)
                    return;
                _isbusy = value;
                OnPropertyChange(nameof(isbusy));
                
            }
            get
            {
                return _isbusy;
            }
        }

        public ICommand refresh
        {
            get
            {
                return new Command(()=>
                {
                    displayRecords();
                });
            }
        }
        #endregion

        public ObservableCollection<Record> GetItems(int pageIndex, int pageSize)
        {
            return (ObservableCollection<Record>)Records.Skip(pageIndex * pageSize).Take(pageSize);
        }

        private async Task DownloadDataAsync()
        {
            var items = GetItems(pageIndex: 0, pageSize: PageSize);

            thisRecord.AddRange(items);
        }

        private bool _load;
        public bool load
        {
            get =>_load;
            set
            {
                _load = value;
                OnPropertyChange(nameof(load));
            }
        }
    }
}