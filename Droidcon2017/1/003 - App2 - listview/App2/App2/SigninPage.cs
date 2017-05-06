using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace App2
{
    public class SigninPage : ContentPage
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        readonly Label signinHeaderLabel;
        readonly Entry emailEntry;
        readonly Entry passwordEntry;
        readonly Button loginButton;
        readonly Button demoLoginButton;

        public SigninPage()
        {
            signinHeaderLabel = new Label()
            {
                Text = "Signin",
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(10, 10, 10, 10),
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            emailEntry = new Entry()
            {
                Placeholder = "Email entry",
                Keyboard = Keyboard.Email
            };
            passwordEntry = new Entry()
            {
                IsPassword = true,
                Placeholder = "Password",
                Keyboard = Keyboard.Text,
            };
            loginButton = new Button()
            {
                Text = "Login",
                BackgroundColor = Color.Purple,
                TextColor = Color.White,
            };
            loginButton.Clicked += LoginButton_Clicked;
            demoLoginButton = new Button()
            {
                Text = "Demo login",
                BackgroundColor = Color.Green,
                TextColor = Color.White
            };
            demoLoginButton.Clicked += DemoLoginButton_Clicked;
            
            Content = new StackLayout
            {
                Children =
                {
                    signinHeaderLabel,
                    emailEntry,
                    passwordEntry,
                    loginButton,
                    demoLoginButton
                }
            };
        }

        private void DemoLoginButton_Clicked(object sender, EventArgs e)
        {
            emailEntry.Text = "admin@demo1.com";
            passwordEntry.Text = "Pass@123";
            LoginButton_Clicked(null, null);
        }

        private void LoginButton_Clicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                DisplayAlert("Error occurred", "empty fields", "Cancel");
                return;
            }

            try
            {
                var isValid =
                (Regex.IsMatch(emailEntry.Text, emailRegex, RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250)));
                if (!isValid)
                {
                    var title = "Error";
                    var msg = "Invalid email";
                    var btn = "Cancel";
                    DisplayAlert(title, msg, btn);
                    return;
                }
                
                //SigninService service = new SigninService();
                //try
                //{
                //    loginButton.IsEnabled = false;
                //    loginButton.Text = "Processing";
                //    LoggedInUser userInfo = service.Signin(emailEntry.Text, passwordEntry.Text);
                //    if (App.Current.Properties.ContainsKey("UserInfo"))
                //    {
                //        App.Current.Properties.Remove("UserInfo");
                //    }

                //    ApplicationDataFactory.UserData = userInfo;
                //    App.Current.Properties.Add("UserInfo", userInfo);
                //    App.Current.MainPage = new MainPage();
                //   // App.NavigateTo(new HomePage());
                //}
                //catch (Exception exception)
                //{
                //    loginButton.IsEnabled = true;
                //    loginButton.Text = "Login";
                //    DisplayAlert("Error occurred", "\n Details: " + exception.Message, "Ok");
                //}             
            }
            catch (Exception exception)
            {
                DisplayAlert("Error", exception.Message, "Cancel");
            }
        }
    }
}