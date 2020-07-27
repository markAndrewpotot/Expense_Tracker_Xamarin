using System;
using System.Collections.Generic;

namespace Expense_Tracking_Xamarin.Models
{
    public class RecCategory
    {
        public int id { get; set; }

        public string name { get; set; }
    }

    public class Record
    {
        public int id { get; set; }

        public DateTime date { get; set; }

        public string notes { get; set; }

        public RecCategory category { get; set; }

        public float amount { get; set; }

        public int record_type { get; set; }

        public string txtcolor { get; set; } //for red and green indicator

        public string iconstring { get; set; } //for displaying icons

        public string newDetail
        {
            get
            {
                return string.Format("{0} -- {1} : {2}", category.name, notes, date);
            }
        }

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
