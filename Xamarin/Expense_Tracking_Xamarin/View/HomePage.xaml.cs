using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Expense_Tracking_Xamarin.Models;
using Expense_Tracking_Xamarin.ViewModel;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace Expense_Tracking_Xamarin.View
{
    public partial class HomePage : ContentPage
    {
        private string token;
        public static Overview getOV;

        List<Entry> entries;

        public HomePage()
        {
            InitializeComponent();
            getOverview();
        }

        private async void getOverview()
        {
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.txt");
            token = File.ReadAllText(filename);

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
                    Label ="Income",
                    ValueLabel = $"{getOV.income}"
                },
                new Entry(getOV.expenses)
                {
                    Color=SKColor.Parse("#FF0000"),
                    Label ="Expense",
                    ValueLabel = $"{getOV.expenses}"
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
            getOverview();
        }
    }
}
