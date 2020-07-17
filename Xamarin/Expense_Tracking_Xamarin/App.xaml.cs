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

            //MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new RecordPage()); //for debugging
        }
    }
}
