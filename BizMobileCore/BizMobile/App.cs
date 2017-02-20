using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace BizMobile
{
    public class App : Application
    {
        public App(string dbPath)
        {
            // Create Database & Tables
            var db = BusinessDbContext.Create(dbPath);            
            //var count = db.Brands.Count() + 1;
            string id = Guid.NewGuid().ToString();
            Brand brand = new Brand()
            {
                Id = id,
                Name = DateTime.Now.ToString(),
                Url = "codecoopers.com/" + id
            };
            db.Brands.Add(brand);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            List<Brand> itemSource = db.Brands.ToList();

            // Show Data
            MainPage = new ContentPage()
            {
                Content = new ListView()
                {
                    ItemsSource = itemSource
                }
            };

        }
    }
}
