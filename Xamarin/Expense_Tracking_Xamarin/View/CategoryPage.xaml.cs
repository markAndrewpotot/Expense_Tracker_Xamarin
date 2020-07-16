using System;
using System.Collections.ObjectModel;
using Expense_Tracking_Xamarin.Models;
using Expense_Tracking_Xamarin.ViewModel;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.View
{
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
            BindingContext = new CategoryViewModel();
        }
    }
}
