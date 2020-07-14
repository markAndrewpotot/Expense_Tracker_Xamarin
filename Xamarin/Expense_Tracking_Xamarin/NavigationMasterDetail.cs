using System;
using Expense_Tracking_Xamarin.Services;
using Expense_Tracking_Xamarin.View;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin
{
    public class NavigationMasterDetail : MasterDetailPage
    {
        public NavigationMasterDetail()
        {
            var master = new MasterPage();
            this.Master = master;
            this.MasterBehavior = MasterBehavior.Popover;

            master.PageSelected += Master_PageSelected;

            PresentDetailPage(PageType.Home);
        }

        void PresentDetailPage(PageType pageType)
        {
            switch (pageType)
            {
                case PageType.Home:
                    this.Detail = new NavigationPage(new HomePage());
                    break;
                case PageType.Logout:
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                    break;
                case PageType.Records:
                    this.Detail = new NavigationPage(new RecordPage());
                    break;
                default:
                    this.Detail = new NavigationPage(new HomePage());
                    break;
            }
            try
            {
                IsPresented = false;
            }
            catch { }
        }

        void Master_PageSelected(object sender, PageType e)
        {
           PresentDetailPage(e);
        }
    }
}
