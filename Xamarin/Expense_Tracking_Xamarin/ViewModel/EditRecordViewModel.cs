using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using Expense_Tracking_Xamarin.View;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class EditRecordViewModel : INotifyPropertyChanged
    {
        public ApiServices api = new ApiServices();

        public int id { get; set; }

        public DateTime date { get; set; }

        public string notes { get; set; }

        public string category_string { get; set; }

        public int category_id { get; set; }

        public float amount { get; set; }

        public int record_type { get; set; }

        public EditRecordViewModel()
        {
            loading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public ICommand delete_this
        {
            get
            {
                return new Command(async () =>
                {
                    loading = true;
                    await api.DeleteRecord(id);
                });
            }
        }

        public ICommand update_this_income
        {
            get
            {
                return new Command(async () =>
                {
                    category_id = getCat_id(category_string);

                    loading = true;
                    await api.PatchRecords(id, date, notes, category_id, amount, 0);
                });
            }
        }

        public ICommand update_this_expense
        {
            get
            {
                return new Command(async () =>
                {
                    category_id = getCat_id(category_string);

                    loading = true;
                    await api.PatchRecords(id, date, notes, category_id, amount, 1);
                });
            }
        }

        public ICommand changeCat
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CategoryPage());
                });
            }
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

        private bool _loading;
        public bool loading
        {
            get { return _loading; }
            set
            {
                if (_loading == value)
                    return;
                _loading = value;
                OnPropertyChanged(nameof(loading));
            }
        }
    }
}
