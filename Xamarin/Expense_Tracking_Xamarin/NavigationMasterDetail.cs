using System;
using Expense_Tracking_Xamarin.View;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin
{
    public class NavigationMasterDetail : MasterDetailPage
    {
        public NavigationMasterDetail()
        {
            this.Master = new MasterPage();
            this.Detail = new HomePage();
            this.Detail = new NavigationPage(new HomePage());
        }
    }
}
