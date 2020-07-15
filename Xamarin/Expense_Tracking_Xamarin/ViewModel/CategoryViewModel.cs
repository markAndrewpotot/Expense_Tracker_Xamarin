using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Expense_Tracking_Xamarin.Models;
using Newtonsoft.Json;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            GetCategory();
        }
        private string token;

        public CategoryModel category;

        ObservableCollection<Category> catmodel = new ObservableCollection<Category>();

        public ObservableCollection<Category> Category { get { return catmodel; } }

        public async void GetCategory()
        {
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.txt");
            token = File.ReadAllText(filename);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("http://expenses.koda.ws/api/v1/categories");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<CategoryModel>(result);

                for (int i = 0; i < category.categories.Count; i++)
                {
                    catmodel.Add(new Category
                    {
                        name = category.categories[i].name,
                        id = category.categories[i].id,
                        icon = category.categories[i].icon
                    });
                }
            }
        }
    }
}
