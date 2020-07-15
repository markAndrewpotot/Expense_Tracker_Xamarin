using System.Collections.Generic;

namespace Expense_Tracking_Xamarin.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }

    }

    public class CategoryModel
    {
        public List<Category> categories { get; set; }

    }
}
