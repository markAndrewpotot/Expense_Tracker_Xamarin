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
        public RecordClass record;
        public Pagination pagination;
        public ObservableCollection<Record> thislist = new ObservableCollection<Record>();
        ObservableCollection<Record> listrecord = new ObservableCollection<Record>();
        public ObservableCollection<Record> Records
        { get => listrecord; set { listrecord = value; OnPropertyChange(nameof(Records)); } }

        private const int PageSize = 10;

        #region Constructor
        public RecordPageViewModel()
        {
            searchnot = "Search";
            enable = false;
            dispTitle = true;
            isbusy = false;
            IsBusy = false;
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

        #region Pass data to code behind
        public ObservableCollection<Record> PassRecords()
        {
            return thislist; // fix this
        }

        public ObservableCollection<Record> ItemSourceRecord()
        {
            return Records;
        }

        public int Pages()
        {
            return pagination.pages;
        }
        #endregion

        #region DisplayRecords
        public async Task displayRecords(int pages)
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

            for(int i = 1; i <= pages; i++)
            {
                GetRecords(i);
            }

            thislist.Clear();
            GetallRecords();

            isbusy = false;
            IsBusy = false;
        }
        #endregion

        #region Paging
        public async Task PagingRecords(int page)
        {
            string textColor = string.Empty;
            string icon = string.Empty;

            string uri = string.Concat("http://expenses.koda.ws/api/v1/records?page=", page);

            isbusy = false;

            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.txt");
            token = File.ReadAllText(filename);

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
                    });
                }
            }
            IsBusy = false;
        }
        #endregion

        #region Get Records
        public async void GetRecords(int page)
        {
            string textColor = string.Empty;
            string icon = string.Empty;

            string uri = string.Concat("http://expenses.koda.ws/api/v1/records?page=",page);

            listrecord.Clear();
            Records.Clear();
            thislist.Clear();
            isbusy = false;

            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.txt");
            token = File.ReadAllText(filename);

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
                        //HideSearchListview = true;
                        //HideListview = false;
                    }
                    else
                    {
                        searchnot = "Search";
                        enable = false;
                        dispTitle = true;
                        //HideSearchListview = false;
                        //HideListview = true;
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
                return new Command(async ()=>
                {
                    await displayRecords(2);
                    //GetRecords(1);

                });
            }
        }
        #endregion

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChange(nameof(IsBusy));
            }
        }

        

        public async void GetallRecords()
        {
            RecordClass record;
            string textColor = string.Empty;
            string icon = string.Empty;

            thislist.Clear();

            for (int j = 1; j <= pagination.pages; j++)
            {

                string uri = string.Concat("http://expenses.koda.ws/api/v1/records?page=", j);

                string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.txt");
                token = File.ReadAllText(filename);

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
        }
    }
}