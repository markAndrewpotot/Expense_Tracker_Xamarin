using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using Expense_Tracking_Xamarin.View;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class NewRecordViewModel : INotifyPropertyChanged
    {
        public ApiServices apiServices = new ApiServices();

        public event PropertyChangedEventHandler PropertyChanged;

        public double amount { get; set; }
        public string notes { get; set; }
        public DateTime date { get; set; }
        public int category_id { get; set; }

        public TimeSpan timePicker { get; set; }

        public NewRecordViewModel()
        {
            date = DateTime.Today;

            timePicker = DateTime.Now.TimeOfDay;
        }

        public ICommand addRecord_income
        {
            get
            {
                return new Command(async () =>
                {
                    category_id = getCat_id(category_string);
                    await apiServices.AddRecords(amount, notes, 0, date, category_id);
                });
            }
        }

        public ICommand addRecord_expense
        {
            get
            {
                return new Command(async () =>
                {
                    category_id = getCat_id(category_string);
                    await apiServices.AddRecords(amount, notes, 1, date, category_id); 
                });
            }
        }
        public ICommand changeCat
        {
            get
            {
                return new Command( () =>
                {
                     Application.Current.MainPage.Navigation.PushAsync(new CategoryPage());
                });
            }
        }

        private string _catString;
        public string category_string
        {
            get { return _catString;  }
            set
            {
                if (_catString == value)
                    return;
                _catString = value;
                OnPropertyChanged(category_string);
            }
        }
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        int getCat_id(string cat)
        {
            int retval = 0;
            switch (cat)
            {
                case "Food & Drinks":
                    retval = 1;
                    break;
                case "Groceries":
                    retval = 2;
                    break;
                case "Clothes":
                    retval = 3;
                    break;
                case "Electronics":
                    retval = 4;
                    break;
                case "Healthcare":
                    retval = 5;
                    break;
                case "Gifts":
                    retval = 6;
                    break;
                case "Transportation":
                    retval = 7;
                    break;
                case "Education":
                    retval = 8;
                    break;
                case "Entertainment":
                    retval = 9;
                    break;
                case "Utilities":
                    retval = 10;
                    break;
                case "Rent":
                    retval = 11;
                    break;
                case "Household Supplies":
                    retval = 12;
                    break;
                case "Investments":
                    retval = 13;
                    break;
                case "Other":
                    retval = 14;
                    break;
            }

            return retval;
        }
    }
}
