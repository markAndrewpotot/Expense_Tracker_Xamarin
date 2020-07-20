using System;
namespace Expense_Tracking_Xamarin.Models
{
    public class Overview
    {
        public float income { get; set; }
        public float expenses { get; set; }
    }
    public class OverviewGraph
    {
        public float amount { get; set; }
        public string category { get; set; }
    }
}
