using System;
using System.Collections.Generic;

namespace Expense_Tracking_Xamarin.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }

    }

    public class Record
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string notes { get; set; }
        public Category category { get; set; }
        public int amount { get; set; }
        public int record_type { get; set; }

    }

    public class Pagination
    {
        public string current_url { get; set; }
        public string next_url { get; set; }
        public string previous_url { get; set; }
        public int current { get; set; }
        public int per_page { get; set; }
        public int pages { get; set; }
        public int count { get; set; }

    }

    public class RecordClass
    {
        public List<Record> records { get; set; }
        public Pagination pagination { get; set; }
    }
}
