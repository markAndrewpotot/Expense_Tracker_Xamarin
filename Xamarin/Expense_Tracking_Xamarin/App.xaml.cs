using Expense_Tracking_Xamarin.View;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new HomePage()); //for debugging
        }
    }
}
