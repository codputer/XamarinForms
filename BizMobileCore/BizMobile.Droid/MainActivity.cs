using System;
using System.IO;
using Android.App;

namespace BizMobile.Droid
{
    [Activity(Label = "BizMobile.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MainTheme")]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
        

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BizMobileDb1.db");
            
            LoadApplication(new App(dbPath));
        }
    }
}

