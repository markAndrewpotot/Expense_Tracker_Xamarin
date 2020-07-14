using System;
namespace Expense_Tracking_Xamarin.Models
{
    public class AddRecord
    {
        public double amount { get; set; }
        public string notes { get; set; }
        public int record_type { get; set; }
        public DateTime date { get; set; }
        public int category_id { get; set; }
    }
}
