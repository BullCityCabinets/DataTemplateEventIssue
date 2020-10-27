using LGRM.XamF.Data;
using LGRM.XamF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LGRM.XamF.Services
{
    public class SqliteDataService : IGroceryDataService
    {
        private static SqliteConnector db;
        public static SqliteConnector Db
        {
            get
            {
                if (db == null)
                {
                    db = new SqliteConnector();
                }
                return db;
            }
        }


        public Task<List<Grocery>> GetAllGroceriesAsync()
        {
            return Db.GetAllGroceriesAsync();
        }

        public Task<List<Grocery>> GetGroceriesByGTypeAsync(GType gType)
        {
            return Db.GetGroceriesByGTypeAsync(gType);
        }

        public Task<List<Grocery>> GetGroceriesByGTypeAndQueryAsync(GType gType, string query)
        {
            return Db.GetGroceriesByGTypeAndQuery(gType, query);
        }

        public Task<Grocery> GetGroceryByIdAsync(int id)
        {
            return Db.GetGroceryByIdAsync(id);
        }

        public Task<int> SaveGroceryAsync(Grocery grocery)
        {
            return Db.SaveGroceryAsync(grocery);
        }

        public Task<int> DeleteGroceryAsync(int id)
        {
            return Db.DeleteGroceryAsync(id);
        }






    }
}
