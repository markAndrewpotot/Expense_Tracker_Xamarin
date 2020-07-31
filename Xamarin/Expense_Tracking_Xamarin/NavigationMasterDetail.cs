using System;
using System.IO;
using Expense_Tracking_Xamarin.View;
using Xamarin.Forms;

namespace Expense_Tracking_Xamarin
{
    public class NavigationMasterDetail : MasterDetailPage
    {
        public NavigationMasterDetail()
        {
            var master = new MasterPage();
            Master = master;
            MasterBehavior = MasterBehavior.Popover;

            NavigationPage.SetHasNavigationBar(this, false);

            master.PageSelected += Master_PageSelected;

            PresentDetailPage(PageType.Home);
        }

        void PresentDetailPage(PageType pageType)
        {
            Page page = new Page();
            switch (pageType)
            {
                case PageType.Home:
                    page = new NavigationPage(new HomePage());
                    break;
                case PageType.Logout:
                    Xamarin.Essentials.Preferences.Remove("token");
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                    break;
                case PageType.Records:
                    page = new NavigationPage(new RecordPage());
                    break;
                default:
                    page = new NavigationPage(new HomePage());
                    break;
            }
            Detail = page;
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
