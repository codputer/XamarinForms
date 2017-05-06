using System;

namespace App2
{
    public static class ApplicationDataFactory
    {
        static ApplicationDataFactory()
        {
            UserData = new LoggedInUser()
                           {
                               ShopId = new Guid().ToString(), UserName = "foyzulkarim@gmail.com"
                           };
            string url = "http://bizbookdemoapi.azurewebsites.net";
            Url = url;
        }

        public static string Url { get; set; }

        public static LoggedInUser UserData { get; set; }     
    }
}