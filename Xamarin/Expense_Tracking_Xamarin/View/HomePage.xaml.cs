using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Expense_Tracking_Xamarin.Models;
using Expense_Tracking_Xamarin.ViewModel;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace Expense_Tracking_Xamarin.View
{
    public partial class HomePage : ContentPage
    {
        private string token;
        public static Overview getOV;

        private HomePageViewModel temp;

        List<Entry> entries;

        public int Counter { get; private set; }

        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomePageViewModel();
            temp = BindingContext as HomePageViewModel;

            listview.Refreshing += Listview_Refreshing;
            listview.ItemAppearing += Listview_ItemAppearing;
        }

        private async void Listview_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            int pages = temp.Pages();

            if (temp.IsBusy || temp.Records.Count == 0)
                return;

            //hit bottom!
            if (e.Item == temp.Records[temp.Records.Count - 1])
            {
                if (Counter < pages)
                {
                    //load here
                    Counter++;
                    footer.IsVisible = true;
                    await Task.Delay(2000);
                    await temp.PagingRecords(Counter);
                    footer.IsVisible = false;
                }
            }
        }

        private void Listview_Refreshing(object sender, EventArgs e)
        {
            Counter = 1;
        }

        private async void getOverview()
        {
            token = Preferences.Get("token", "");

            string uri = "http://expenses.koda.ws/api/v1/records/overview";

            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(uri);

            var result = await response.Content.ReadAsStringAsync();

            getOV = JsonConvert.DeserializeObject<Overview>(result);

            entries = new List<Entry>()
            {
                new Entry(getOV.income)
                {
                    Color=SKColor.Parse("#008000"),
                    Label =$"{getOV.income}",
                    TextColor = SKColor.Parse("238823")
                },
                new Entry(getOV.expenses)
                {
                    Color=SKColor.Parse("#FF0000"),
                    Label =$"{getOV.expenses}",
                    TextColor = SKColor.Parse("D2222d")
                }
            };

            charts.Chart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 24
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Counter = 1;
            listview.BeginRefresh();
            getOverview();
        }
    }
}
