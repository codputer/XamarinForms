using SQLite;

namespace Todo.Repository
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

