using System;
using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using System.IO;
using Xamarin.Forms;


namespace Expense_Tracking_Xamarin.ViewModel
{
    public class LoginViewModel
    {
        public ApiServices apiServices = new ApiServices();

        public string email { get; set; }
        public string password { get; set; }
        public string message { get; set; }

        public LoginViewModel()
        {
            email = "name@domain.com";
            password = "secret";
        }

        public ICommand Login
        {
            get
            {
                return new Command(async () =>
                {
                    var response = await apiServices.ApiLogin(email, password);

                    if (response)
                    {
                        Application.Current.MainPage = new NavigationPage(new NavigationMasterDetail());
                    }
                });
            }
        }
    }
}