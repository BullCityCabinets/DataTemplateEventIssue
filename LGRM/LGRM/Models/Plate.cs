using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace LGRM.XamF.Models
{
    public class Plate : BindableBase
    {
        ///    MEMBERS    \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        private byte _id { get; set; }
        public byte Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        private string _name { get; set; } = "MyPlate";
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }


        private ObservableCollection<Grub> _grubList { get; set; }
        public ObservableCollection<Grub> GrubList
        {
            get => _grubList;
            set
            {
                _grubList = value;
                RaisePropertyChanged(nameof(GrubList));
            }
        }

        private float _totalGs { get; set; }
        public float TotalGs
        {
            get => _totalGs;
            set
            {
                float x = 0;
                foreach (var g in GrubList)
                {
                    if (g.GType == GType.Green)
                    {
                        x += g.QtyPortion;
                    }
                }

                _totalGs = x;
                RaisePropertyChanged("TotalGs");
            }

        }



        ///    CTOR       \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public Plate(byte id)
        {
            this.Id = id;
            this.Name = "My Plate's Name";

        }



    }
}