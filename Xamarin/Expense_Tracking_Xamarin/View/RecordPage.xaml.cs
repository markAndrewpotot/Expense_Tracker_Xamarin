using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Expense_Tracking_Xamarin.Models;
using Expense_Tracking_Xamarin.ViewModel;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.View
{
    public partial class RecordPage : ContentPage
    {
        public RecordPage()
        {
            InitializeComponent();
            sb.Unfocused += Sb_Unfocused;
            BindingContext = new RecordPageViewModel();
        }

        private void Sb_Unfocused(object sender, FocusEventArgs e)
        {
            tbi.Text = "Search";
            sb.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listview.BeginRefresh();
        }

        void sb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var temp = BindingContext as RecordPageViewModel;

            ObservableCollection<Record> container = temp.PassRecords();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                listview.ItemsSource = container;
            else
                listview.ItemsSource = container.Where(i => i.notes.Contains(e.NewTextValue));
        }
    }
}
