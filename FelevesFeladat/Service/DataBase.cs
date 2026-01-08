using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.Service
{
    public interface IDataBaseService
    {
        Task<List<MenuItem>> GetReviewsAsync();
        Task<MenuItem> GetReviewAsync(int id);
        Task<int> SaveReviewAsync(MenuItem item);
        Task<int> DeleteReviewAsync(MenuItem item);
    }
    public partial class DataBase : IDataBaseService
    {
        
        SQLiteAsyncConnection database;

        async Task Init()
        {
            if (database is not null)
                return;

            
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyFoodReviews.db3");

            database = new SQLiteAsyncConnection(dbPath);
            await database.CreateTableAsync<MenuItem>();
        }
        public async Task<List<MenuItem>> GetReviewsAsync()
        {
            await Init();
            return await database.Table<MenuItem>().ToListAsync();
        }

        // 2. Egy elem lekérése ID alapján (READ)
        public async Task<MenuItem> GetReviewAsync(int id)
        {
            await Init();
            return await database.Table<MenuItem>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        // 3. Hozzáadás/Frissítés (CREATE / UPDATE)
        public async Task<int> SaveReviewAsync(MenuItem item)
        {
            await Init();
            if (item.id != 0)
            {
                return await database.UpdateAsync(item);
            }
            else
            {
                // Ha nincs ID (0), akkor adjon hozzá
                return await database.InsertAsync(item);
            }
        }

        // 4. Törlés (DELETE)
        public async Task<int> DeleteReviewAsync(MenuItem item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }
    }
}
