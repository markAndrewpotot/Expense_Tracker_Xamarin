using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Expense_Tracking_Xamarin.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.ViewModel
{
    public class CategoryViewModel : INotifyPropertyChanged
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
            token = Xamarin.Essentials.Preferences.Get("token", "");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("http://expenses.koda.ws/api/v1/categories");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<CategoryModel>(result);

                for (int i = 0; i < category.categories.Count; i++)
                {
                    string ic = string.Empty;
                    char[] arrays = category.categories[i].icon.ToCharArray();

                    for(int c = 8; c < arrays.Length; c++)
                    {
                        ic += arrays[c];
                    }

                    catmodel.Add(new Category
                    {
                        name = category.categories[i].name,
                        id = category.categories[i].id,
                        icon = ic
                    });
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Models.Category _category_string;
        public Models.Category category_string
        {
            set
            {
                if (_category_string == value)
                    return;

                _category_string = value;
                OnPropertyChanged(nameof(category_string));
                HandleSelectedItem();
            }
            get
            {
                return _category_string;
            }
        }

        private void HandleSelectedItem()
        {
            Xamarin.Essentials.Preferences.Set("categoryname", category_string.name);

            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
