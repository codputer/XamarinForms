using System;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : MasterDetailPage
    {
        private MasterPage masterPage;

        public MainPage()
        {
            masterPage = new MasterPage();
            Master = masterPage;
            this.MasterBehavior = MasterBehavior.Split;
            Detail = new NavigationPage(new HomePage());
            masterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                var page =
                    (Page)(!item.TargetType.Name.Contains("Entry") ?
                    Activator.CreateInstance(item.TargetType)
                        : Activator.CreateInstance(item.TargetType, string.Empty));

                Detail = new NavigationPage(page);
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}