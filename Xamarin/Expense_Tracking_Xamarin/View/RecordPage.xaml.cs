using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.View
{
    public partial class RecordPage : ContentPage
    {
        public RecordPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            listview.IsRefreshing = true;
            listview.BeginRefresh();
            listview.IsRefreshing = false;
        }
    }
}
