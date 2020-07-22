using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using Xamarin.Forms;
using System.ComponentModel;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public ApiServices apiServices = new ApiServices();

        public string email { get; set; }
        public string password { get; set; }
        public string message { get; set; }

        public LoginViewModel()
        {
            email = "name@domain.com";
            password = "secret";

            loading = false;
            enable = true;
        }

        private bool _loading;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

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

        public bool _enable;
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

        public ICommand Login
        {
            get
            {
                return new Command(async () =>
                {
                    loading = true;
                    enable = false;
                    var response = await apiServices.ApiLogin(email, password);

                    if (response)
                    {
                        Application.Current.MainPage = new NavigationPage(new NavigationMasterDetail());
                    }
                    loading = false;
                    enable = true;
                });
            }
        }
    }
}