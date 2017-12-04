using CryptoFolio.ServiceHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMMainPage : VM, INotifyPropertyChanged
    {
        public String TotalOutputString { get { return "5 $"; } }
        public String TotalInputRatioString { get { return "0$ / +0$"; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public VMMainPage()
        {
            
        }
    }
}