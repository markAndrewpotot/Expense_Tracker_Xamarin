using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Expense_Tracking_Xamarin.Models;
using Expense_Tracking_Xamarin.ViewModel;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.View
{
    public partial class RecordPage : ContentPage
    {
        private RecordPageViewModel temp;
        
        private int Counter { get; set; }

        private ObservableCollection<Record> container;

        public RecordPage()
        {
            InitializeComponent();
            BindingContext = new RecordPageViewModel();
            temp = BindingContext as RecordPageViewModel;

            footer.IsVisible = false;

            sb.Unfocused += Sb_Unfocused;
            listview.Refreshing += Listview_Refreshing;
            listview.ItemAppearing += listview_ItemAppearing;
        }

        private void Listview_Refreshing(object sender, EventArgs e)
        {
            Counter = 2;
        }

        private async void listview_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            int pages = temp.Pages();

            if (temp.IsBusy || temp.Records.Count == 0)
                return;

            //hit bottom!
            if (e.Item == temp.Records[temp.Records.Count - 1])
            {
                if(Counter < pages)
                {
                    //load here
                    Counter++;
                    footer.IsVisible = true;
                    await Task.Delay(500);
                    await temp.PagingRecords(Counter);
                    footer.IsVisible = false;
                }
            }
        }
        private async void Sb_Unfocused(object sender, FocusEventArgs e)
        {
            tbi.Text = "Search";
            sb.IsVisible = false;

            Counter = 2;

            container.Clear();

            await temp.displayRecords(2);

            listview.ItemsSource = temp.ItemSourceRecord();

            //SearchListview.IsVisible = false;
            //listview.IsVisible = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Counter = 2;
            listview.BeginRefresh();
        }

        void sb_TextChanged(object sender, TextChangedEventArgs e)
        {
            container = temp.PassRecords();

            //SearchListview.IsVisible = true;
            //listview.IsVisible = false;

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                listview.ItemsSource = container;
            else
                listview.ItemsSource = container.Where(i => i.notes.Contains(e.NewTextValue));
        }
    }
}
