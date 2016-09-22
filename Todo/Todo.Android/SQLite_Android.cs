using System;
using System.IO;
using SQLite;
using Todo.Droid;
using Todo.Repository;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteAndroid))]

namespace Todo.Droid
{
    public class SqLiteAndroid : ISQLite
    {
        private static SQLiteConnection connection;
        public SQLiteConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    var sqliteFilename = "TodoDb201609201.db3";
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    var path = Path.Combine(documentsPath, sqliteFilename);
                    if (!File.Exists(path))
                    {
                        FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    }
                    connection = new SQLiteConnection(path);
                }
                return connection;
            }
        }     
    }
}
