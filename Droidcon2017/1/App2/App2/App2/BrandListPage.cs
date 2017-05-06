using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace App2
{
    public class BrandListPage : ContentPage
    {
        public ListView _listView;
        protected SearchBar searchBar;
        protected ActivityIndicator activityIndicator;

        public void PrepareListView()
        {
           _listView = CreateListView();
            _listView.ItemSelected += ListView_ItemSelected;
            searchBar = new SearchBar { Placeholder = "Keyword", Margin = new Thickness(5, 0, 5, 0) };
            searchBar.TextChanged += Search_TextChanged;
        }

        private ListView CreateListView()
        {
            Func<object> template = () =>
            {
                var cell = new TextCell { TextColor = Color.Black };
                cell.SetBinding(TextCell.TextProperty, "Name");
                return cell;
            };

            ListView listView = new ListView
            {
                IsPullToRefreshEnabled = true,
                ItemTemplate = new DataTemplate(template),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.Default,
                Margin = new Thickness(0)
            };
            return listView;
        }

        protected void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                var searchedList = list.Where(x => x.Name.ToLower().Contains(keyword));
                _listView.ItemsSource = searchedList;
            }
            else
            {
                _listView.ItemsSource = list;
            }
        }

        protected void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var x = e.SelectedItem;
        }

        protected void NavigateToChildPage(string id)
        {

        }

        public BrandListPage()
        {
            Title = "Brands";
            PrepareListView();

            activityIndicator = new ActivityIndicator
            {
                Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsRunning = true
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(0),
                Children =
                {
                    searchBar,
                    activityIndicator,
                    _listView
                }
            };
        }
         
        List<Brand> list;
        protected override void OnAppearing()
        {
            list = new List<Brand>();
            list.Add(new Brand()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Microsoft"
            });
            list.Add(new Brand()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Code Coopers"
            });
            list.Add(new Brand()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Google"
            });
            list.Add(new Brand()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "DreamOgrammerS"
            });

            _listView.ItemsSource = list;
            activityIndicator.IsRunning = false;
        }
    }
}