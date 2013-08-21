using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
namespace VietSearchWindowsPhone.ViewModels
{
    public class DistrictViewModel
    {
        string _districtName;

        public string districtName
        {
            get { return _districtName; }
            set
            {
                if (value != _districtName)
                {
                    _districtName = value;
                    NotifyChanged("districtName");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
