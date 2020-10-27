using System;
using System.Threading.Tasks;

namespace LGRM.XamF.Data
{


    //This is to help count tables in the database.. see public async Task<List<TableName>> GetAllTablesAsync()
    //https://forums.xamarin.com/discussion/122320/how-can-i-get-a-collection-of-all-tables-in-a-sqlite-database
    public class TableName
    {
        public TableName() { }
        public string Name { get; set; }
    }




    public static class TaskExtensions //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/databases
    {
        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        public static async void SafeFireAndForget(this Task task,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }

            // if the provided action is not null, catch and
            // pass the thrown exception
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }



}
