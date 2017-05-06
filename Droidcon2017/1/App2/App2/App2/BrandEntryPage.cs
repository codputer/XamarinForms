using System.Collections.Generic;
using Xamarin.Forms;

namespace App2
{
    public class BrandEntryPage<T> : BaseEntryPage<T> where T : Brand
    {
        public BrandEntryPage(string id) : base(id)
        {

        }

        protected override void InitializeControls()
        {
            var nameEntry = ControlFactory.CreateEntryBox(nameof(_model.Name), _model);
            AddCommonControls(new List<View>() { nameEntry });
        }

        protected override void NavigateToList()
        {
            App.NavigateTo(new BrandListPage());
        }

        
    }
}