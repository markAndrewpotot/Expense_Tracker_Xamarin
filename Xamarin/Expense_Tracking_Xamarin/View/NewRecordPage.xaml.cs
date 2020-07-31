using System;
using System.Collections.Generic;
using System.IO;
using Expense_Tracking_Xamarin.ViewModel;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.View
{
    public partial class NewRecordPage : TabbedPage
    {
        public string token { set; get; }
        public string category_string { set; get; }

        public NewRecordPage()
        {
            InitializeComponent();
            BindingContext = new NewRecordViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            category_string = Xamarin.Essentials.Preferences.Get("categoryname", "");

            cat_income.Text = cat_expense.Text = category_string;
        }
    }
}
