using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class RegistrationViewModel
    {
        public ApiServices apiServices = new ApiServices();

        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string message { get; set; }

        public ICommand SignUp
        {
            get
            {
                return new Command(async () =>
                {
                    if (password == confirmpassword)
                    {

                        var response = await apiServices.Signup(name, email, password);
                        Application.Current.MainPage = new NavigationMasterDetail();
                    }
                });
            }
        }
    }
}
