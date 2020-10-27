using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LGRM.XamF.Data.Utility
{
    public static class LocalFileConnector
    {

        public static string ReadResource(string fileNameAndExtention) // fileNameAndExtention= "Document.txt" for example... be sure to mark file "Embedded Resource"
        {

            var assembly = Assembly.GetExecutingAssembly();

            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(fileNameAndExtention)); 

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                Debug.WriteLine(result);
                Console.WriteLine("~~~");

                return result;

            }
        }



        public static void SerializeExistingCatalog() // Serialize installed catalog for future deployment...
        {            
            Console.WriteLine("~~~");
            string json = JsonConvert.SerializeObject(App.AllGroceries, Formatting.Indented);
            var outName = "DefaultCatalog" + DateTime.Now.ToString("yyMMdd") + ".txt"; // ...eg : DefaultCatalog201010.txt
            Console.WriteLine("~~~ " + outName);
            var outPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), outName);
            System.IO.File.WriteAllText(outPath, json);
            Console.WriteLine(outPath);

            // note for pull...
            // adb pull /data/user/0/com.companyname.lgrm/files/DefaultCatalog******.txt C:\Users\ScumS\Desktop

            Console.WriteLine("~~~");
        }






    }
}
