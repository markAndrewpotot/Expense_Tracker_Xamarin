﻿using System;
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

        public string category_string { get; set; }

        public RecordEditPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            id_label.Text = E_id_label.Text = ID.ToString();
            I_amount.Text = E_amount.Text = amount.ToString();
            I_notes.Text = E_notes.Text = notes;
            I_CatID.Text = E_CatID.Text = category.id.ToString();
            I_date.Date = E_date.Date = date;
            I_recordtype.Text = E_recordtype.Text = record_type.ToString();
            I_time.Time = E_time.Time = DateTime.Now.TimeOfDay;

            category_string = Xamarin.Essentials.Preferences.Get("categoryname","");

            cat_income.Text = cat_expense.Text = category_string;
        }
    }
}
