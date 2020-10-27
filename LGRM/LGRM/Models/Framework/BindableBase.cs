using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LGRM.XamF.Models

{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }









}