using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TodoAPP.Models;

namespace TodoAPP.Data
{
    public class TodoItemDataBase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<TodoItemDataBase> Instance =
            new AsyncLazy<TodoItemDataBase>(async () =>
            {
                var instance = new TodoItemDataBase();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<TodoItem>();
                }
                catch (Exception ex)
                {
                    throw;
                }
                return instance;
            });

        public TodoItemDataBase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<TodoItem>> GetItemsAsync()
        {
            return Database.Table<TodoItem>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemsNaoFeitoAsync()
        {
            return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [feito] = 0");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return Database.Table<TodoItem>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
