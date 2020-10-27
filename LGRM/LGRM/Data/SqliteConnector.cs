using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using SQLite;
using Newtonsoft.Json;
using LGRM.XamF.Models;
using LGRM.XamF.Data.Utility;

namespace LGRM.XamF.Data
{
    public class SqliteConnector
    {
        public SQLiteAsyncConnection Db; //....................................................................was Readonly

        public SqliteConnector()
        {
            Db = new SQLiteAsyncConnection(Constants.DatabasePath);

            //Check if "Groceries" table exists
            if (TableExists("Groceries"))
            {

                try
                {
                    int gCount = Db.Table<Grocery>().CountAsync().Result;
                    Debug.WriteLine("~~~ Groceries found: " + gCount);

                    foreach (var t in GetAllTablesAsync().Result)
                    {
                        Debug.WriteLine("~~~ Table found: " + t.Name);
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Source......." + e.Source);
                    Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Message......" + e.Message);                    
                    Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ InnerEx......" + e.InnerException);
                    Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Data........." + e.Data);
                    Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ StackTrace..." + e.StackTrace);
                    Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" + e.StackTrace);
                }
            }
            else 
            {
                // Create and verify required tables...
                Db.CreateTableAsync<Grocery>();
                //Db.CreateTableAsync<Grub>();
                // Doublecheck tables we created !!!
                foreach (var t in GetAllTablesAsync().Result)
                {
                    Debug.WriteLine("~~~ Table found: " + t.Name);
                }

                //Deserialize the default catalog...
                var defaultJson = LocalFileConnector.ReadResource("DefaultCatalog201010.txt");  //This file belongs in the main Xamarin Forms shared project, and should be marked as "Embedded resource"
                List<Grocery> scratchList = JsonConvert.DeserializeObject<List<Grocery>>(defaultJson);

                var jsonCount = 0;
                var addedCount = 0;
                foreach (var g in scratchList)
                {
                    jsonCount++;
                    //Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");                    
                    //Debug.WriteLine("~~~ From Json: " + g.Id);
                    //Debug.WriteLine("~~~            " + g.Category);
                    //Debug.WriteLine("~~~            " + g.Name1);
                    //Debug.WriteLine("~~~            " + g.Name2);
                    //Debug.WriteLine("~~~            " + g.Desc1);
                    //Debug.WriteLine("~~~ jsonCount #" + jsonCount);
                    //Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                    if (jsonCount > 349)
                    {
                        Debug.WriteLine("~~~ WTF??? if (jsonCount > 349)");
                        Debug.WriteLine("~~~ jsonCount #" + jsonCount);
                        Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    }

                    AddDefaultGroceryAsync(g);
                    addedCount++;
                    if (addedCount > 349)
                    {

                        Debug.WriteLine("~~~ WTF??? AddDefaultGroceryAsync(g)");
                        Debug.WriteLine("~~~ addedCount #" + addedCount);
                        Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                        
                    }
                }
                // Doublecheck tables we created, again !!!
                foreach (var t in GetAllTablesAsync().Result)
                {
                    Debug.WriteLine("~~~ Table found: " + t.Name);
                }
                Debug.WriteLine($"~~~ Total freaking Groceries in table: {Db.Table<Grocery>().CountAsync().Result} !!!!! ");

                Debug.WriteLine($"~~~ ");


            }
            

        }


        #region Groceries Table...
        public Task<List<Grocery>> GetAllGroceriesAsync()
        {
            return Db.Table<Grocery>().ToListAsync();
        }

        public Task<Grocery> GetGroceryByIdAsync(int id)
        {
            return Db.Table<Grocery>().Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveGroceryAsync(Grocery grocery)
        {

            if(Db.Table<Grocery>().CountAsync().Result > 350)
            {
                Debug.WriteLine($"~~~ WTF???");
                Debug.WriteLine($"~~~ While adding defaults, Groceries Found... {Db.Table<Grocery>().CountAsync().Result}!");
                Debug.WriteLine($"~~~ ");
            }

            if (grocery.Id != 0)
            {
                return Db.UpdateAsync(grocery);
            }
            else
            {
                return Db.InsertAsync(grocery);
            }
        }

        public Task<int> AddDefaultGroceryAsync(Grocery grocery)
        {
            return Db.InsertAsync(grocery);

        }



        public Task<int> DeleteGroceryAsync(int id)
        {
            return Db.Table<Grocery>().Where(g => g.Id == id).DeleteAsync();
        }

        public Task<List<Grocery>> GetGroceriesByGTypeAsync(GType gType)
        {
            return Db.Table<Grocery>()
                .Where(g => g.GType == gType)
                .ToListAsync();
        }

        public Task<List<Grocery>> GetGroceriesByGTypeAndQuery(GType gType, string query)
        {
            return Db.Table<Grocery>()
                .Where(g =>
                g.GType == gType &&
                g.Name1.ToLower().Contains(query.ToLower()) == true)
                .ToListAsync();
        }

        public async Task<int> ClearGroceryDBAsync()
        {
            return await Db.DeleteAllAsync<Grocery>();
        }

        #endregion Groceries





        #region Plates Tables (Grub)...
        //public Task<List<Grub>> GetAllGrubAsync()
        //{
        //    return Db.Table<Grub>().ToListAsync();
        //}

        //public Task<List<Grub>> GetGrubByPlateAsync(int plateId)
        //{
        //    return Db.Table<Grub>()
        //        .Where(c =>
        //        c.PlateId == plateId)
        //        .ToListAsync();
        //}

        public Task<List<Grub>> GetGrubByPlateAndGTypeAsync(int plateId, GType gType)
        {
            return Db.Table<Grub>()
                .Where(c =>
                c.PlateId == plateId &&
                c.GType == gType)
                .ToListAsync();
        }

        public Task<int> SaveOrDeleteGrubAsync(Grub grub)
        {
            if (Db.Table<Grub>().Where(c => c.GrubId == grub.GrubId).CountAsync().Result > 0)
            {
                return Db.UpdateAsync(grub);
            }
            else
            {
                return Db.InsertAsync(grub);
            }

        }

        public Task<int> DeleteGrubAsync(int grubId)
        {
            return Db.Table<Grub>().Where(c => c.GrubId == grubId).DeleteAsync();
        }

        public void CleanPlate(int plateId)  // DeleteGrubByPlateAndIdAsync ? This is not Async ! ? ?!?!?!? ?! ?!? !? !?!? !?! !?
        {
            List<Grub> toBeD = Db.Table<Grub>()
                .Where(c => c.PlateId == plateId)
                .ToListAsync().Result;

            foreach (var c in toBeD)
            {
                Db.DeleteAsync(c);
            }

        }

        #endregion Plates Tables (Grub)




        #region Utility....
        public async Task<List<TableName>> GetAllTablesAsync() //... see SQLiteExtender
        {
            string queryString = $"SELECT name FROM sqlite_master WHERE type = 'table'";
            return await Db.QueryAsync<TableName>(queryString).ConfigureAwait(false);
        }


        //https://forums.xamarin.com/discussion/1460/sqlite-how-to-check-if-table-exists
        //public virtual bool TableExists(string tableName)
        //{
        //    bool sw = false;
        //    try
        //    {
        //        using (var connection = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), PathDataBase))
        //        {
        //            string query = string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}';", tableName);
        //            SQLiteCommand cmd = connection.CreateCommand(query);
        //            var item = connection.Query<object>(query);
        //            if (item.Count > 0)
        //                sw = true;
        //            return sw;
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        //Log.Info("SQLiteEx", ex.Message);
        //        throw;
        //    }
        //}


        public bool TableExists(string tableName)
        {

            string query = string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}';", tableName);
            var item = Db.QueryAsync<object>(query);
            if (item.Result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        #endregion Utility....


    }



}