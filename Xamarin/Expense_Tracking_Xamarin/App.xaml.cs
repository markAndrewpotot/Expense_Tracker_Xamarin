using System;
using Expense_Tracking_Xamarin.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracking_Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
