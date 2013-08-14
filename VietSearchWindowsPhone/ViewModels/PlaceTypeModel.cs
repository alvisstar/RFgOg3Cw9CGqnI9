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
    public class PlaceTypeModel
    {
        string _placeTypeId;

        public string placeTypeId
        {
            get { return _placeTypeId; }
            set { _placeTypeId = value; }
        }
        string _placeTypeName;

        public string placeTypeName
        {
            get { return _placeTypeName; }
            set { _placeTypeName = value; }
        }
    }
}
