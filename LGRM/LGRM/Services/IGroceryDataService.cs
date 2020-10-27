using LGRM.XamF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LGRM.XamF.Services
{
    public interface IGroceryDataService
    {

        Task<List<Grocery>> GetAllGroceriesAsync();
        Task<List<Grocery>> GetGroceriesByGTypeAsync(GType gType);

        Task<List<Grocery>> GetGroceriesByGTypeAndQueryAsync(GType gType, string query);

        Task<Grocery> GetGroceryByIdAsync(int id);
        Task<int> SaveGroceryAsync(Grocery grocery);
        Task<int> DeleteGroceryAsync(int id);
    }
}