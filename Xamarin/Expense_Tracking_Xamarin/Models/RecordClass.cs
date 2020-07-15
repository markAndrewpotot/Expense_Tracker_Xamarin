using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Expense_Tracking_Xamarin.Models
{
    public class RecCategory
    {
        //[JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        //[JsonProperty(PropertyName = "name")]
        public string name { get; set; }

    }

    public class Record
    {
        //[JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        //[JsonProperty(PropertyName = "date")]
        public DateTime date { get; set; }

        //[JsonProperty(PropertyName = "notes")]
        public string notes { get; set; }

        //[JsonProperty(PropertyName = "category")]
        public RecCategory category { get; set; }

        //[JsonProperty(PropertyName = "amount")]
        public float amount { get; set; }

        //[JsonProperty(PropertyName = "record_type")]
        public int record_type { get; set; }

        public string newDetail
        {
            get
            {
                return string.Format("{0} --- {1} ", category.name, notes);
            }
        }

    }

    public class Pagination
    {
        //[JsonProperty(PropertyName = "current_url")]
        public string current_url { get; set; }

        //[JsonProperty(PropertyName = "next_url")]
        public string next_url { get; set; }

        //[JsonProperty(PropertyName = "previous_url")]
        public string previous_url { get; set; }

        //[JsonProperty(PropertyName = "current")]
        public int current { get; set; }

        //[JsonProperty(PropertyName = "per_page")]
        public int per_page { get; set; }

        //[JsonProperty(PropertyName = "pages")]
        public int pages { get; set; }

        //[JsonProperty(PropertyName = "count")]
        public int count { get; set; }

    }

    public class RecordClass
    {
        //[JsonProperty(PropertyName = "records")]
        public List<Record> records { get; set; }

        //[JsonProperty(PropertyName = "pagination")]
        public Pagination pagination { get; set; }
    }
}
