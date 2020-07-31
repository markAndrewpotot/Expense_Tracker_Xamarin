using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Essentials;
using System;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public ApiServices apiServices = new ApiServices();

        public bool Response { get; set; }

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
                    var current = Connectivity.NetworkAccess;
                    if (current == NetworkAccess.Internet)
                    {
                        loading = true;
                        enable = false;

                        if(email != null && password != null)
                        {
                            Response = await apiServices.ApiLogin(email, password);
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(null, "Don't leave Empty Entry", "OK");
                        }


                        if (Response)
                        {
                            Application.Current.MainPage = new NavigationPage(new NavigationMasterDetail());
                        }
                        loading = false;
                        enable = true;
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("No Internet Access", "Please Check the Network", "OK");
                    
                });
            }
        }
    }
}