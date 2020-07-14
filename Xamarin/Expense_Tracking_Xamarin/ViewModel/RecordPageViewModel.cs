using System;
using System.Windows.Input;
using Expense_Tracking_Xamarin.Models;
using Expense_Tracking_Xamarin.Services;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class RecordPageViewModel
    {
        public ApiServices apiServices = new ApiServices();

        public ICommand GetRecord
        {
            get
            {
                return new Command(() =>
                {
                    var result = apiServices.GetRecord();
                });
            }
        }
    }
}
