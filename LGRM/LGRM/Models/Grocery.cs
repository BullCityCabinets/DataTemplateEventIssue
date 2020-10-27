using SQLite;
using Newtonsoft.Json;

namespace LGRM.XamF.Models
{
    [Table("Groceries")]
    public class Grocery : BindableBase
    {
        #region Private Members...
        private int _id { get; set; }
        private GType _gType { get; set; }
        private string _category { get; set; }
        private string _iconName { get; set; }
        private string _iconColor1 { get; set; }
        private string _name1 { get; set; }
        private string _name2 { get; set; }
        private string _desc1 { get; set; }
        private int _info1 { get; set; }
                
        private float _baseWeight { get; set; }
        private string _uomWeight { get; set; }

        private float _baseVolume { get; set; }
        private string _uomVolume { get; set; }

        private float _baseCount { get; set; }
        private string _uomCount { get; set; }

        #endregion Members, Private


        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        public GType GType
        {
            get => _gType;
            set
            {
                _gType = value;
                RaisePropertyChanged(nameof(GType));
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                RaisePropertyChanged(nameof(Category));
            }
        }

        public int Info1
        {
            get => _info1;
            set
            {
                _info1 = value;
                RaisePropertyChanged(nameof(Info1));
            }
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ICONS
        public string IconName
        {
            get => _iconName;
            set
            {
                _iconName = value;
                RaisePropertyChanged(nameof(IconName));
            }
        }
        public string IconColor1
        {
            get => _iconColor1;
            set
            {
                _iconColor1 = value;
                RaisePropertyChanged(nameof(IconColor1));
            }
        }  

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ NAME & DESC
        public string Name1
        {
            get => _name1;
            set
            {
                _name1 = value;
                RaisePropertyChanged(nameof(Name1));
            }
        }
        public string Name2
        {
            get => _name2;
            set
            {
                if (_name2 == "")
                {
                    _name2 = null;
                }
                else
                {
                    _name2 = value;
                    RaisePropertyChanged(nameof(Name2));
                }
                
            }
        }
        public string Desc1
        {
            get => _desc1;
            set
            {
                _desc1 = value;
                RaisePropertyChanged(nameof(Desc1));
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ WEIGHT
        [JsonProperty("QtyWeight")]
        public float BaseWeight
        {
            get => _baseWeight;
            set
            {
                _baseWeight = value;
                RaisePropertyChanged(nameof(BaseWeight));
            }
        }
        public string UomWeight
        {
            get => _uomWeight;
            set
            {
                _uomWeight = value;
                RaisePropertyChanged(nameof(UomWeight));
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ VOLUME
        [JsonProperty("QtyVolume")]
        public float BaseVolume
        {
            get => _baseVolume;
            set
            {   _baseVolume = value;
                RaisePropertyChanged(nameof(BaseVolume));
            }
        }
        public string UomVolume
        {
            get => _uomVolume;
            set
            {
                _uomVolume = value;
                RaisePropertyChanged(nameof(UomVolume));
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ COUNT
        [JsonProperty("QtyCount")]
        public float BaseCount
        {
            get => _baseCount;
            set
            {
                _baseCount = value;
                RaisePropertyChanged(nameof(BaseCount));
            }
        }
        public string UomCount
        {
            get => _uomCount;
            set
            {
                _uomCount = value;
                RaisePropertyChanged(nameof(UomCount));
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Junk to concatentate for bindings...
        [Ignore] [JsonIgnore]
        public string WeightServing
        {
            get
            {
                if (BaseWeight > 0)
                {
                    return BaseWeight.ToString() + " " + UomWeight;
                }
                else { return ""; }
            }
        }

        [Ignore] [JsonIgnore]
        public string VolumeServing
        {
            get
            {
                if (BaseVolume > 0)
                {
                    return BaseVolume.ToString() + " " + UomVolume;
                }
                else { return ""; }
            }
        }

        [Ignore] [JsonIgnore]
        public string CountServing
        {
            get
            {
                if (BaseCount > 0)
                {
                    return BaseCount.ToString() + " " + UomCount;
                }
                else { return ""; }
            }
        }

        [Ignore] [JsonIgnore]
        public string Info1String
        {
            get
            {
                if (GType == GType.Lean)
                {
                    switch (Info1)
                    {
                        case 1:
                            return "Lean ";
                        case 2:
                            return "Leaner ";
                        case 3:
                            return "Leanest ";
                        default:
                            return "";
                    }
                }
                else if (GType == GType.Green)
                {
                    switch (Info1)
                    {
                        case 1:
                            return "Low Carb ";
                        case 2:
                            return "Mod. Carb ";
                        case 3:
                            return "High Carb ";
                        default:
                            return "";
                    }
                }
                else return "";
            }
        } // Maybe make a converter ?

        [Ignore] [JsonIgnore]
        public string EtcString
        {
            get
            {
                var myString = "";

                if (Info1String != "")
                {
                    myString += ( Info1String + " ");
                }

                myString += Category + " ";

                if (Desc1 != "")
                {
                    myString += ( Desc1 + " ");
                }
                return myString;

            }

        }





    }
}


