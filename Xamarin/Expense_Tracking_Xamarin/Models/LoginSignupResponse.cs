using System;
namespace Expense_Tracking_Xamarin.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }

    }

    public class Response
    {
        public string message { get; set; }
        public string token { get; set; }
        public User user { get; set; }

    }

    public class ErrorMessage
    {
        public string error { get; set; }

    }
}
