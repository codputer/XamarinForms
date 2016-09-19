using Xamarin.Forms;

namespace App1
{
    public class ContactsPageCS : ContentPage
    {
        public ContactsPageCS()
        {
            var table = new TableView {Intent = TableIntent.Settings};
            var layout = new StackLayout() { Orientation = StackOrientation.Horizontal };
            Button button = new Button(){ Text = "Click me"};
            button.Clicked += Button_Clicked;

            layout.Children.Add(button);
            layout.Children.Add(new Label()
            {
                Text = "left",
                TextColor = Color.FromHex("#f35e20"),
                VerticalOptions = LayoutOptions.Center
            });
            layout.Children.Add(new Label()
            {
                Text = "right",
                TextColor = Color.FromHex("#503026"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            });
            table.Root = new TableRoot()
            {
                new TableSection("Getting Started")
                {
                    new ViewCell() {View = layout}
                }
            };

            Content = table;

            Title = "Contacts Page";
           
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Hello guys", "Welcome to xamarin", "Thanks");
        }
    }
}