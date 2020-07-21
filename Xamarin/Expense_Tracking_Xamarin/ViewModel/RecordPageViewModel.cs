using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using Expense_Tracking_Xamarin.Models;
using Expense_Tracking_Xamarin.View;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class RecordPageViewModel : INotifyPropertyChanged
    {
        public RecordPageViewModel()
        {
            GetRecords();
        }
        private string token;

        public RecordClass record;

        ObservableCollection<Record> listrecord = new ObservableCollection<Record>();

        public ObservableCollection<Record> Records { get { return listrecord; } }

        public async void GetRecords()
        {
            listrecord.Clear();
            Records.Clear();

            string textColor = string.Empty;
            string icon = string.Empty;

            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.txt");
            token = File.ReadAllText(filename);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("http://expenses.koda.ws/api/v1/records/");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
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
            isbusy = false;
        }

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

        private bool _isbusy;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChange(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

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
                    GetRecords();
                });
            }
        }
    }
}
