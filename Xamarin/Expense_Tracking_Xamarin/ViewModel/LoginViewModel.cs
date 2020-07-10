using System;
using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class LoginViewModel
    {
        public ApiServices apiServices = new ApiServices();

        public string email { get; set; }
        public string password { get; set; }

        public ICommand Login
        {
            get
            {
                return new Command(async () =>
                {
                    var response = await apiServices.ApiLogin(email, password);
                    Console.WriteLine($"--> {response}");
                });
            }
        }
    }
}
