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

namespace VietSearchWindowsPhone.ViewModels
{
    public class CityViewModel
    {
        string _cityName;

        public string cityName 
        {
            get { return _cityName; }
            set { _cityName = value; }
        }
    }
}
