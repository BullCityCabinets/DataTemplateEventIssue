using LGRM.XamF.Models;
using LGRM.XamF.Services;
using LGRM.XamF.ViewModels;
using LGRM.XamF.Views;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace LGRM
{
    public partial class App : Application
    {
        //public static NavigationService NavigationService { get; } = new NavigationService();
        public static SqliteDataService SqliteDataService { get; } = new SqliteDataService();
        public static ObservableCollection<Grocery> AllGroceries { get; } = 
            new ObservableCollection<Grocery>(SqliteDataService.GetAllGroceriesAsync().Result.OrderBy(g => g.Name1));


        public static Plate MyPlate { get; set; } = new Plate(1);

        public static PlateVM MyPlateVM = new PlateVM();
        public static PlateView MyPlateView = new PlateView();
                
        public static ListGroceriesByTypeVM MyListOfGreensVM = new ListGroceriesByTypeVM(GType.Green);
        public static ListOfGreensView MyListOfGreensView = new ListOfGreensView();

        

        public App()
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental" });  //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/internals/experimental-flags

            InitializeComponent();

            MyPlate.GrubList = new ObservableCollection<Grub>();
            MainPage = new NavigationPage(MyPlateView);


        }


        


    }

}
