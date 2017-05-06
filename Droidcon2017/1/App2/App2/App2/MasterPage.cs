using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App2
{
    public class MasterPage : ContentPage
    {
        ListView listView;

        public MasterPage()
        {
            Func<object> template = () =>
            {
                var cell = new TextCell { TextColor = Color.Black };
                cell.SetBinding(TextCell.TextProperty, "Title");
                return cell;
            };

            listView = new ListView()
            {
                ItemsSource = GetMasterPageItems(),
                ItemTemplate = new DataTemplate(template),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.Default, RowHeight = 40, HasUnevenRows = false, 
            };

            bool containsKey = App.Current.Properties.ContainsKey("UserInfo");
            LoggedInUser user = containsKey ? App.Current.Properties["UserInfo"] as LoggedInUser : null;
            Label label = new Label()
            {
                Text = user != null ? user.UserName : "Bizbook365User",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                Margin = new Thickness(10, 0, 0, 0)
            };
            Button logOut = new Button()
            {
                Text = "LogOut",
            };
            logOut.Clicked += LogOut_Clicked;

            Padding = new Thickness(0, 40, 0, 0);
            Icon = "hamburger.png";
            Title = "BizBook365";
            
            Content = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { label, listView, logOut }, Padding = new Thickness(20)
            };
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            //App.SettingService.Remove("UserInfo");
            //App.Current.Properties.Remove(AppKeys.IsLoggedIn.ToString());
            //App.Current.Properties.Remove(AppKeys.UserInfo.ToString());
            //AppConstants.User = null;

            App.Current.MainPage = new SigninPage();
        }

        public ListView ListView { get { return listView; } }



        private List<MasterPageItem> GetMasterPageItems()
        {
            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Home",
                    TargetType = typeof(HomePage)
                },
                new MasterPageItem()
                {
                    Title = "Brands",
                    TargetType = typeof(BrandListPage)
                },
                new MasterPageItem()
                {
                    Title = "Brand Entry",
                    TargetType = typeof(BrandEntryPage<Brand>)
                },
            };
            return masterPageItems;
        }
    }

    public class MasterPageItem
    {
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}