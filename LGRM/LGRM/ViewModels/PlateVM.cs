using LGRM.XamF.Models;
using LGRM.XamF.ViewModels.Framework;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace LGRM.XamF.ViewModels
{
    public class PlateVM : BaseVM
    {
        ///    MEMBERS    \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\  

        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        public ICommand TestCommand { get; set; }
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!

        //~~ For Binding Static Lists on this View... 
        public ObservableCollection<Grub> MyGreens { get; set; }

        #region //~~ For adjusting View's collection heights per items added...
        public int GrubLabelHeight = 400; //Standard Grub label height
        public int CollViewMargin = 20; //Standard Collection Margins
        public int EmptyHeight = 60; //Empty Height

        private int _heightG { get; set; }
        public int HeightG
        {
            get
            {
                if (MyGreens.Count > 0)
                {
                    return (_heightG * GrubLabelHeight) + CollViewMargin;
                }
                else
                {
                    return EmptyHeight;
                }
            }
            set
            {
                _heightG = value;
                OnPropertyChanged("HeightG");
            }
        }

        #endregion //~~ For adjusting View's collection heights per items added...

        #region //~~ For plate summary header...   
        private float _totalGs { get; set; }
        public float TotalGs
        {
            get => _totalGs;
            set
            {
                float x = 0;
                foreach (var g in MyGreens)
                {
                    x += g.QtyPortion;
                }

                _totalGs = x;

                //MessagingCenter.Send<PlateVM, float>(this, "UpdatePortionL", x);  //BAD IDEA!
                OnPropertyChanged("TotalGs");
            }

        }
        #endregion




        ///     CTOR      \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public PlateVM()
        {

            //~~ For Updates from Lists of Groceries...
            MessagingCenter.Subscribe<ListGroceriesByTypeVM, object>(this, "UpdatePlateLists", OnUpdatePlateItems);

            //~~ For Binding Static App.MyPlate list in View... 
            MyGreens = new ObservableCollection<Grub>();

            //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
            //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
            //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
            TestCommand = new Command(OnTestCommand);
            //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
            //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
            //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!


        }






        ///    METHODS    \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        ///               \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        ///               

        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        public void OnTestCommand()
        {
            UpdateTotalPortions(GType.Lean);
            Debug.WriteLine("~~~ OnTestCommand called from PlateVM !!!");
            Debug.WriteLine("~~~ OnTestCommand called from PlateVM !!!");
            Debug.WriteLine("~~~ OnTestCommand called from PlateVM !!!");
        }

        public void UpdateTotalPortions(GType gType)
        {
            TotalGs = App.MyPlate.TotalGs;
        }
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!
        //THIS IS THE TARGET FROM THE DATATEMPLATE !!!!









        public void OnUpdatePlateItems(object sender, object objectChanged)
        {
            var grubChanged = new Grub((Grocery)objectChanged);

            bool contains = MyGreens.Any(g => g.Id == grubChanged.Id);
            if (!contains)
            {
                MyGreens.Add(grubChanged);
            }
            else
            {
                MyGreens.Remove(MyGreens.FirstOrDefault(g => g.Id == grubChanged.Id));
                
            }

            HeightG = MyGreens.Count;

        }





    }


}