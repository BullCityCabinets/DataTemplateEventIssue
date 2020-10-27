using System;
using System.Diagnostics;

namespace LGRM.XamF.Models
{
    //[Table("Plates")]
    public class Grub : Grocery
    {
        private int _plateId { get; set; }
        private int _grubId { get; set; }
        
        public int PlateId
        {
            get => _plateId;
            set
            {
                _plateId = value;
                RaisePropertyChanged(nameof(PlateId));
            }
        }

        public int GrubId
        {
            get => _grubId;
            set
            {
                _grubId = value;
                RaisePropertyChanged(nameof(GrubId));
            }
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ PORTION
        private float _qtyPortion { get; set; }
        public bool _qtyPortionCalled;
        public float QtyPortion
        {
            get => _qtyPortion;
            set
            {
                if ((_qtyPortion != value) && (!_qtyPortionCalled))
                {
                    _qtyPortionCalled = true;
                    _qtyPortion = value;
                    
                    QtyWeight = CalculateWeight();
                    QtyVolume = CalculateVolume();
                    QtyCount = CalculateCount();
                    _qtyPortionCalled = false;
                    RaisePropertyChanged(nameof(QtyPortion));  // Raise PropertyChanged event as normal here, using base class or Invoke
                }
            }
        }

        private float CalculatePortion()
        {
            if (_qtyWeightCalled)
            {
                return (float)Math.Round((QtyWeight / BaseWeight), 3);
            }
            if (_qtyVolumeCalled)
            {
                return (float)Math.Round((QtyVolume / BaseVolume), 3);
            }
            if (_qtyCountCalled)
            {
                return (float)Math.Round((QtyCount / BaseVolume), 3);
            }
            else return 999;
        }

        private float CalculateWeight()
        {
            if (_qtyPortionCalled || _qtyVolumeCalled || _qtyCountCalled)
            {
                return (float)Math.Round((BaseWeight * QtyPortion), 3);
            }
            else return 999;
        }

        private float CalculateVolume()
        {
            if (_qtyPortionCalled || _qtyWeightCalled || _qtyCountCalled)
            {
                return (float)Math.Round((BaseVolume * QtyPortion), 3);
            }
            else return 999;
        }

        private float CalculateCount()
        {
            if (_qtyPortionCalled || _qtyWeightCalled || _qtyVolumeCalled)
            {
                return (float)Math.Round((BaseCount * QtyPortion), 3);
            }
            else return 999;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ WEIGHT
        private float _qtyWeight { get; set; }
        public bool _qtyWeightCalled;
        public float QtyWeight
        {
            get => _qtyWeight;
            set
            {
                if ((_qtyWeight != value) && (!_qtyWeightCalled))
                {
                    _qtyWeightCalled = true;
                    _qtyWeight = value;                    
                    
                    QtyPortion = CalculatePortion();
                    QtyVolume = CalculateVolume();
                    QtyCount = CalculateCount();
                    _qtyWeightCalled = false;
                    RaisePropertyChanged(nameof(QtyWeight)); // Raise PropertyChanged event as normal here, using base class or Invoke
                }
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ VOLUME
        private float _qtyVolume{ get; set; }
        public bool _qtyVolumeCalled;
        public float QtyVolume
        {
            get => _qtyVolume;
            set
            {
                if ((_qtyVolume != value) && (!_qtyVolumeCalled))
                {
                    _qtyVolumeCalled = true;
                    _qtyVolume = value;
                    
                    QtyPortion = CalculatePortion();
                    QtyWeight = CalculateWeight();
                    QtyCount = CalculateCount();
                    _qtyVolumeCalled = false;
                    RaisePropertyChanged(nameof(QtyVolume)); // Raise PropertyChanged event as normal here, using base class or Invoke
                }
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ COUNT
        private float _qtyCount { get; set; }
        public bool _qtyCountCalled;
        public float QtyCount
        {
            get => _qtyCount;
            set
            {
                if ((_qtyCount != value) && (!_qtyCountCalled))
                {
                    _qtyCountCalled = true;
                    _qtyCount = value;
                    RaisePropertyChanged(nameof(QtyCount)); // Raise PropertyChanged event as normal here, using base class or Invoke
                    //_qtyPortion = CalculatePortion();
                    //_qtyWeight = CalculateWeight();
                    //_qtyVolume = CalculateWeight();
                    _qtyCountCalled = false;
                }                
            }
        }


        //SQLite needs a parameterless CTOR... this is not really a useful constructor, since every Grub will be based on an existing Grocery instance
        public Grub() : base()
        {
            PlateId = App.MyPlate.Id;
            //QtyPortion = 1.0f;
        }

        //Used to artificially "Cast" from base to derived class...
        public Grub(Grocery g) 
        {
            PlateId = App.MyPlate.Id;
            QtyPortion = 1.0f;

            this.Id = g.Id;

            this.Category = g.Category;
            this.GType = g.GType;

            this.IconName = g.IconName;
            this.IconColor1 = g.IconColor1;

            this.Name1 = g.Name1;
            this.Name2 = g.Name2;
            
            this.Desc1 = g.Desc1;
            this.Info1 = g.Info1;

            this.BaseWeight = g.BaseWeight;
            this.BaseVolume = g.BaseVolume;
            this.BaseCount= g.BaseCount;

            this.QtyWeight = g.BaseWeight;
            this.UomWeight = g.UomWeight;
            this.QtyVolume = g.BaseVolume;
            this.UomVolume = g.UomVolume;
            this.QtyCount = g.BaseCount;
            this.UomCount = g.UomCount;



        }



        //private float CalculateVolume()
        //{
        //    var v = QtyPortion 




        //}













    }
}
