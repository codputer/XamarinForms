using System.Collections.Generic;
using System.Linq;
using Model;
using Xamarin.Forms;

namespace Todo.Repository
{
    public class TodoItemRepository : BaseRepository
    {
        public TodoItemRepository()
        {
            database = DependencyService.Get<ISQLite>().Connection;
            database.CreateTable<TodoItem>();
        }

        public IEnumerable<TodoItem> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<TodoItem>() select i).ToList();
            }
        }

        //public IEnumerable<TodoItem> GetItemsNotDone()
        //{
        //    lock (locker)
        //    {
        //        return database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //    }
        //}

        public IEnumerable<TodoItem> GetItemsNotDone()
        {
            lock (locker)
            {
                var tableQuery = database.Table<TodoItem>();
                return tableQuery.Where(x => x.Done == false).AsEnumerable();
            }
        }


        public TodoItem GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<TodoItem>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveItem(TodoItem item)
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    database.Update(item);
                    return item.ID;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<TodoItem>(id);
            }
        }
    }
}

