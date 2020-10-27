using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LGRM.XamF.Data
{
    public static class Constants //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/databases
    {
        public const string DatabaseFilename = "LGRM201003.db3";
        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |      // open the database in read/write mode
            SQLite.SQLiteOpenFlags.Create |         // create the database if it doesn't exist            
            SQLite.SQLiteOpenFlags.SharedCache;     // enable multi-threaded database access

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

    } 
}