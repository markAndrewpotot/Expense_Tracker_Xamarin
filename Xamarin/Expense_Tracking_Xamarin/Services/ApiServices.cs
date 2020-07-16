using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Expense_Tracking_Xamarin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.Services
{
    public class ApiServices
    {
        public string token;

        public async Task<bool> Signup(string name, string email, string password)
        {
            var client = new HttpClient();

            var model = new Registration
            {
                Name = name,
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://expenses.koda.ws/api/v1/sign_up", content);

            token = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        public List<RecordClass> RecordClasses { get; set; }

        public async Task GetRecord()
        {
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.txt");
            token = File.ReadAllText(filename);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("http://expenses.koda.ws/api/v1/records");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var record = JsonConvert.DeserializeObject<List<RecordClass>>(result);

                RecordClasses = new List<RecordClass>(record);
            }
        }

        public async Task AddRecords(double amount, string notes, int record_type, DateTime date, int category_id)
        {
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.txt");
            token = File.ReadAllText(filename);

            string uri = "http://expenses.koda.ws/api/v1/records";

            var client = new HttpClient();
            var model = new AddRecord
            {
                amount = amount,
                notes = notes,
                record_type = record_type, 
                date = date,
                category_id = category_id
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

            string json = JsonConvert.SerializeObject(model);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(uri, content);

            if(response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(null, "Fail", "OK");
            }
        }

        public async Task<string> ApiLogin(string email, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("password", password)
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "http://expenses.koda.ws/api/v1/sign_in")
            {
                Content = new FormUrlEncodedContent(keyValues)
            };

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var jwt = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);

            var accessToken = jwtDynamic.Value<string>("token");

            Console.WriteLine(jwt);

            return accessToken;
        }
    }
}