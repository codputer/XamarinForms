using System;
using System.IO;
using SQLite;
using Todo.Droid;
using Todo.Repository;
using Xamarin.Forms;

[assembly: Dependency (typeof (SqLiteAndroid))]

namespace Todo.Droid
{
	public class SqLiteAndroid : ISQLite
	{
	    #region ISQLite implementation
		public SQLiteConnection GetConnection ()
		{
			var sqliteFilename = "TodoDb201609201.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); 
            var path = Path.Combine(documentsPath, sqliteFilename);
            if (!File.Exists(path))
			{
                // we don't want to pre-populate our database 
                // didn't delete this line for clarity
                //Stream s = Forms.Context.Resources.OpenRawResource(Resource.Raw.TodoSQLite);


                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                // we don't want to pre-populate our database. hence commented. 
                // didn't delete this line for clarity
                //	ReadWriteStream(s, writeStream);
            }

            var conn = new SQLiteConnection(path);
            return conn;
		}
		#endregion
	 
	}
}
