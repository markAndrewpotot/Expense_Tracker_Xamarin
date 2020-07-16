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
        public NewRecordPage()
        {
            InitializeComponent();
            BindingContext = new NewRecordViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "catName.txt");
            string category_string = File.ReadAllText(filename);

            cat_income.Text = cat_expense.Text = category_string;
        }
    }
}
