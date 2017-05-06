namespace App2
{
    public class BrandListPage : BaseListPage <Brand, BrandRequestModel>
    {
        public BrandListPage() : base("Brands", "Name")
        {
        }

        protected override void OnAppearing()
        {
            activityIndicator.IsRunning = true;
            //var dbSet = App.Db.Set<T>().AsQueryable();
            var db = new BusinessDbContext(App.path);
            var list = db.Brands.ToList();
            _listView.ItemsSource = list;
            activityIndicator.IsRunning = false;
        }

        protected override void NavigateToChildPage(string id)
        {
            App.NavigateTo(new BrandEntryPage<Brand>(id));
        }
    }
}
