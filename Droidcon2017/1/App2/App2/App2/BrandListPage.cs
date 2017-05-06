using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        HttpClient client;
        public BrandListPage()
        {
            this.client = new HttpClient();
            string a = ApplicationDataFactory.Url;
            string t = ApplicationDataFactory.UserData.AccessToken;
            this.SetHttpProperties(a, t);

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
            Tuple<List<Brand>, int> result = SearchAsync();
            list = result.Item1;
            _listView.ItemsSource = list;
            activityIndicator.IsRunning = false;
        }

        public Tuple<List<Brand>, int> SearchAsync()
        {
            string requestUri = ApplicationDataFactory.Url + "/api/BrandQuery/Search";
            var data = new
            {
                Page = -1,
                Keyword = ""
            };
            StringContent value = this.PrepareStringContent(data);
            HttpResponseMessage response;
            response = client.PostAsync(requestUri, value).Result;
            response.EnsureSuccessStatusCode();
            string content = response.Content.ReadAsStringAsync().Result;
            int count = 0;
            IEnumerable<string> values;
            bool valueFound = response.Headers.TryGetValues("Count", out values);
            Tuple<List<Brand>, int> searchAsync = null;
            if (valueFound)
            {
                count = Convert.ToInt32(values.First());
                searchAsync = new Tuple<List<Brand>, int>(new List<Brand>(), count);
                if (count > 0)
                {
                    searchAsync = JsonConvert.DeserializeObject<Tuple<List<Brand>, int>>(content);
                }
            }

            return searchAsync;
        }

        private StringContent PrepareStringContent(object data)
        {
            string serializeObject = JsonConvert.SerializeObject(data);
            var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");
            return content;
        }

        private bool SetHttpProperties(string address, string token)
        {
            this.client.BaseAddress = new Uri(address);
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

    }
}