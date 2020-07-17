using System;
using System.Collections.Generic;
using System.IO;
using Expense_Tracking_Xamarin.Models;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.View
{
    public partial class RecordEditPage : TabbedPage
    {
        public int ID { set; get; }

        public DateTime date { get; set; }

        public string notes { get; set; }

        public RecCategory category { get; set; }

        public float amount { get; set; }

        public int record_type { get; set; }

        public RecordEditPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            id_label.Text = ID.ToString();
            I_amount.Text = amount.ToString();
            I_notes.Text = notes;
            I_CatID.Text = category.id.ToString();
            I_date.Date = date;
            I_recordtype.Text = record_type.ToString();
            I_time.Time = DateTime.Now.TimeOfDay;
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "catName.txt");
            string category_string = File.ReadAllText(filename);

            cat_income.Text = category_string;
        }
    }
}
