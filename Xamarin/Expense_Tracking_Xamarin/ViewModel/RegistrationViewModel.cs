using System.ComponentModel;
using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public ApiServices apiServices = new ApiServices();

        public bool response { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string message { get; set; }

        public RegistrationViewModel()
        {
            loading = false;
            enable = true;
        }

        public ICommand SignUp
        {
            get
            {
                return new Command(async () =>
                {
                    var current = Connectivity.NetworkAccess;
                    if (current == NetworkAccess.Internet)
                    {
                        if (password == confirmpassword)
                        {
                            loading = true;
                            enable = false;

                            if (name != null && email != null && password != null)
                            {
                                response = await apiServices.Signup(name, email, password);
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(null, "Don't leave Empty Entry", "OK");
                            }

                            if (response)
                            {
                                Application.Current.MainPage = new NavigationPage(new NavigationMasterDetail());
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(null, "Password Mismatch", "OK");
                        }
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("No Internet Access", "Please Check the Network", "OK");
                   
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
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

        private bool _enable;
        public bool enable
        {
            get { return _enable; }
            set
            {
                if (_enable == value)
                    return;
                _enable = value;
                OnPropertyChanged(nameof(enable));
            }
        }
    }
}
