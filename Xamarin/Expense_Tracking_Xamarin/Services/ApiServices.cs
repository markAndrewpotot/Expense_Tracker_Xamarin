using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Expense_Tracking_Xamarin.Models;
using Newtonsoft.Json;

namespace Expense_Tracking_Xamarin.Services
{
    public class ApiServices
    {
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

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ApiLogin(string email, string password)
        {
            var client = new HttpClient();
            var model = new Login{email = email, password = password};
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("http://expenses.koda.ws/api/v1/sign_in", content);
            return response.IsSuccessStatusCode;
        }
    }
}