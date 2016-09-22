using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;
using Todo.Repository;

namespace Todo
{
    public class TodoListPage : ContentPage
    {
        ListView listView;
        public TodoListPage()
        {
            Title = "Todo";
            var searchBar = new SearchBar()
            {
                Placeholder = "Type here to search", HeightRequest = 50
            };
            searchBar.HorizontalOptions = LayoutOptions.StartAndExpand;
            searchBar.TextChanged += SearchBar_TextChanged;


            listView = new ListView();
            listView.RowHeight = 50;
            listView.ItemTemplate = new DataTemplate
                    (typeof(TodoItemCell));
            listView.ItemSelected += (sender, e) =>
            {
                var todoItem = (TodoItem)e.SelectedItem;
                var todoPage = new TodoItemPage();
                todoPage.BindingContext = todoItem;

                ((App)App.Current).ResumeAtTodoId = todoItem.ID;
                Debug.WriteLine("setting ResumeAtTodoId = " + todoItem.ID);

                Navigation.PushAsync(todoPage);
            };

            var layout = new StackLayout();
            if (Device.OS == TargetPlatform.WinPhone)
            { // WinPhone doesn't have the title showing
                layout.Children.Add(new Label
                {
                    Text = "Todo",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                });
            }
            layout.Children.Add(searchBar);
            layout.Children.Add(listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            Content = layout;

            #region toolbar
            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                    {
                        var todoItem = new TodoItem();
                        var todoPage = new TodoItemPage();
                        todoPage.BindingContext = todoItem;
                        Navigation.PushAsync(todoPage);
                    }, 0, 0);
            }
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", () =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new TodoItemPage();
                    todoPage.BindingContext = todoItem;
                    Navigation.PushAsync(todoPage);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
            {
                tbi = new ToolbarItem("Add", "add.png", () =>
                    {
                        var todoItem = new TodoItem();
                        var todoPage = new TodoItemPage();
                        todoPage.BindingContext = todoItem;
                        Navigation.PushAsync(todoPage);
                    }, 0, 0);
            }

            ToolbarItems.Add(tbi);

            if (Device.OS == TargetPlatform.iOS)
            {
                var tbi2 = new ToolbarItem("?", null, () =>
                {
                    var todos = App.Repository.GetItemsNotDone();
                    var tospeak = "";
                    foreach (var t in todos)
                        tospeak += t.Name + " ";
                    if (tospeak == "") tospeak = "there are no tasks to do";

                    DependencyService.Get<ITextToSpeech>().Speak(tospeak);
                }, 0, 0);
                ToolbarItems.Add(tbi2);
            }
            #endregion
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue;
            List<TodoItem> listViewItemsSource = App.Repository.GetItems().ToList();
            if (keyword.Length>0)
            {
                listViewItemsSource = listViewItemsSource.Where(x => x.Name.Contains(keyword) || x.Notes.Contains(keyword)).ToList();
            }
            listView.ItemsSource = null;
            listView.ItemsSource = listViewItemsSource;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = App.Repository.GetItems();
        }
    }
}

