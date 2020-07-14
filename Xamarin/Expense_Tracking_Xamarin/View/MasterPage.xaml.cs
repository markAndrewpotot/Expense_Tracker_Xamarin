using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Expense_Tracking_Xamarin.View
{
    public partial class MasterPage : ContentPage
    {
        public event EventHandler<PageType> PageSelected;
        public MasterPage()
        {
            InitializeComponent();

            HomeBtn.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.Home);
            RecordBtn.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.Records);
            LogoutBtn.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.Logout);
        }
    }
}
