using System.Windows.Input;
using Expense_Tracking_Xamarin.Services;
using Expense_Tracking_Xamarin.View;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class HomePageViewModel
    {
        public ICommand StartTracking
        {
            get
            {
                return new Command(async () =>
                {
                    //Application.Current.MainPage = new NavigationPage(new NewRecordPage());
                    await Application.Current.MainPage.Navigation.PushAsync(new NewRecordPage());
                });
            }
        }
    }
}
