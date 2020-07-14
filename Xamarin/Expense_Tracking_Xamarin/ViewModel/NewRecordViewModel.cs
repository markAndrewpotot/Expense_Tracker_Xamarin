using System;
using System.ComponentModel;
using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using Expense_Tracking_Xamarin.View;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class NewRecordViewModel 
    {
        public ApiServices apiServices = new ApiServices();

        public event PropertyChangedEventHandler PropertyChanged;

        public double amount { get; set; }
        public string notes { get; set; }
        public DateTime date { get; set; }
        public int category_id { get; set; }

        public NewRecordViewModel()
        {
            date = DateTime.Today;
        }

        public ICommand addRecord
        {
            get
            {
                return new Command(async () =>
                {
                    await apiServices.AddRecords(amount, notes, 1, date, 1);
                });
            }
        }
        public ICommand changeCat
        {
            get
            {
                return new Command(() =>
                {
                    Application.Current.MainPage.Navigation.PushAsync(new CategoryPage());
                });
            }
        }

    }
}
