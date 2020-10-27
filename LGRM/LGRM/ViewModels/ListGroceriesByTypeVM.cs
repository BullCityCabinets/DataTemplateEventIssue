using LGRM.XamF.Models;
using LGRM.XamF.ViewModels.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace LGRM.XamF.ViewModels
{
    public class ListGroceriesByTypeVM : BaseVM
    {     
        ///    MEMBERS      \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\       
        ///                 \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        //~~ Footer Text...
        public string footerText { get; set; } = "LEAN & GREEN RECIPE MACHINE - BETA 201018";

        //~~ Data Source...
        public GType MyGType;
        private ObservableCollection<Grocery> _groceries;
        public ObservableCollection<Grocery> Groceries
        {
            get => _groceries;
            set
            {
                _groceries = value;
                OnPropertyChanged("Groceries");
            }
        }

        //~~ Selecting Items...
        ObservableCollection<object> _mySelectedItems; //To capture View's binding input
        public ObservableCollection<object> MySelectedItems
        {
            get
            {
                return _mySelectedItems;
            }
            set
            {
                if (_mySelectedItems != value)
                {
                    _mySelectedItems = value;
                }
            }
        }
        public ICommand MySelectionChangedCommand { get; set; }                        

        //~~ Search Text...
        public ICommand SearchCommand { get; }

        //~~ Filter by Category...
        public List<string> Categories { get; set; }
        private object _selectedCategory { get; set; }
        public object SelectedCategory 
        {
            get => _selectedCategory;
            set
            {
                if (Groceries != null)
                {
                    _selectedCategory = value;
                    Groceries = GetGroceriesByGTypeAndCategory();
                    OnPropertyChanged("SelectedCategory");
                }
                else
                {
                    _selectedCategory = value;
                    OnPropertyChanged("SelectedCategory");
                }
            }
        }

                
        ///      CTOR       \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        ///                 \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public ListGroceriesByTypeVM(GType gType)
        {
            //~~ Filter by Category...
            Categories = new List<string>();            
            //SelectedCategory = Categories[0]; // = "All Categories" must be included to get initial list of Groceries

            //~~ Data Source...
            MyGType = gType;
            Groceries = GetGroceriesByGTypeAndCategory();
            Categories = Groceries.Select(g => g.Category).Distinct().ToList();
            Categories.Insert(0, "All Categories");            

            //~~ Selecting Items...
            MySelectedItems = new ObservableCollection<object>() { };
            MySelectionChangedCommand = new Command<object>(OnMySelectionChangedCommand);
            
            //~~ Search Text...
            SearchCommand = new Command<string>(OnSearchCommand);


            
        }


        ///    METHODS      \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        ///                 \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        
        public int OnMySelectionChangedCommandCount;
        private void OnMySelectionChangedCommand(object obj)
        {
            OnMySelectionChangedCommandCount++;
            Debug.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Debug.WriteLine("~~~ Base VM OnMySelectionChangedCommand ..." + OnMySelectionChangedCommandCount);
             
            var current = MySelectedItems;
            var previous = new List<Grub>();

            
            switch (MyGType)  
            {
                case GType.Lean:
                    previous = App.MyPlate.GrubList.Where(g => g.GType == GType.Lean).ToList();                    
                    break;
                case GType.Green:
                    previous = App.MyPlate.GrubList.Where(g => g.GType == GType.Green).ToList();                    
                    break;
                case GType.HealthyFat:
                    previous = App.MyPlate.GrubList.Where(g => g.GType == GType.HealthyFat).ToList();                    
                    break;
                case GType.Condiment:
                    previous = App.MyPlate.GrubList.Where(g => g.GType == GType.Condiment).ToList();
                    break;
                default:
                    break;
            }
            // previous = App.MyPlate.GrubList.Where(g => g.GType == GType.****).ToList();
            if (previous.Count != current.Count) // Like, when nagivating to the page
            {
                List<object> ObjectsChanged = new List<object>();
                object changed = new object();

                if (previous.Count < current.Count)
                {                    
                    ObjectsChanged = current.Except(previous).ToList();
                    var TheObjectChanged = ObjectsChanged[ObjectsChanged.Count-1];
                    //App.MyPlate.GrubList.Add(new Grub((Grocery)TheObjectChanged));
                    MessagingCenter.Send<ListGroceriesByTypeVM, object>(this, "UpdatePlateLists", TheObjectChanged);
                                        
                }
                else if (previous.Count > current.Count)
                {                    
                    ObjectsChanged = previous.Except(current).ToList();
                    var TheObjectChanged = ObjectsChanged[ObjectsChanged.Count - 1];
                    var grub = new Grub((Grocery)TheObjectChanged);
                    //App.MyPlate.GrubList.Remove(App.MyPlate.GrubList.FirstOrDefault(g => g.Name1 == grub.Name1));
                    MessagingCenter.Send<ListGroceriesByTypeVM, object>(this, "UpdatePlateLists", TheObjectChanged);
                
                }

            }

        }






        public virtual void MessagePlate() { } //must be defined in each VM (because I don't know how to use a variable in place of a type)
        public virtual void MessagePlate(object changed) { } 

        //~~ Search Text...
        private string _searchQuery { get; set; }
        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                _searchQuery = value;
                OnPropertyChanged("SearchQuery");
                OnSearchCommand(_searchQuery);
            }
        }

        public void OnSearchCommand(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                try
                {                    
                    //Groceries = new ObservableCollection<Grocery>(                        
                    //    App.AllGroceries.Where(g => g.GType == MyGType));
                    Groceries = GetGroceriesByGTypeAndCategory();

                    var normalizedQuery = query?.ToLower() ?? "";
                    var myList = Groceries.Where(g => 
                        g.Name1.ToLowerInvariant().Contains(normalizedQuery) &&
                        g.Category == SelectedCategory.ToString()).ToList();

                    var myOC = new ObservableCollection<Grocery>();
                    foreach (var g in myList)
                    {
                        myOC.Add(g);
                    }
                    Groceries = myOC;
                }
                catch (Exception e)
                {
                    ReportException(e);

                }
            }
            else
            {
                try
                {
                    //this.Groceries = new ObservableCollection<Grocery>(App.AllGroceries.Where(g => g.GType == MyGType));
                    Groceries = GetGroceriesByGTypeAndCategory();

                }
                catch (Exception e)
                {

                    ReportException(e);
                }

            }
        }

        public ObservableCollection<Grocery> GetGroceriesByGTypeAndCategory()
        {
            if (Categories.Count > 0 && SelectedCategory.ToString() != Categories[0])
            {
                return new ObservableCollection<Grocery>(App.AllGroceries.Where(g => g.GType == MyGType && g.Category == SelectedCategory.ToString()).ToList());
            }
            else
            {
                return new ObservableCollection<Grocery>(App.AllGroceries.Where(g => g.GType == MyGType).ToList());
            }
        }



    }
}