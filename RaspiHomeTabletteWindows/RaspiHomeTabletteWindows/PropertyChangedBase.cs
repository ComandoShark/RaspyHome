using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI;
using Windows.UI.ApplicationSettings;

namespace RaspiHomeTabletteWindows
{
    public class PropertyChangedBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            //Raise the PropertyChanged event on the UI Thread, with the relevant propertyName parameter:
            //Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            //{
            //    PropertyChangedEventHandler handler = PropertyChanged;
            //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            //}));
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
