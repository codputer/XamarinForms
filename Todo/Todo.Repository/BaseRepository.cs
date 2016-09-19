using SQLite;

namespace Todo.Repository
{
    public class BaseRepository
    {
        protected static object locker = new object();
        protected SQLiteConnection database;
    }
}