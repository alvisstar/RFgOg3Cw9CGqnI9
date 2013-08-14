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
    public class PlaceViewModel
    {
        string _placeName;

        public string placeName
        {
            get { return _placeName; }
            set { _placeName = value; }
        }

        double _rate;

        public double rate
        {
            get { return _rate; }
            set { _rate = value; }
        }


        int _ordinal;

        public int ordinal
        {
            get { return _ordinal; }
            set { _ordinal = value; }
        }

        string _homeNumber;

        public string homeNumber
        {
            get { return _homeNumber; }
            set { _homeNumber = value; }
        }
        PlaceTypeModel _placeType;

        public PlaceTypeModel placeType
        {
            get { return _placeType; }
            set { _placeType = value; }
        }
        StreetViewModel _street;

        public StreetViewModel street
        {
            get { return _street; }
            set { _street = value; }
        }
        DistrictViewModel _district;

        public DistrictViewModel district
        {
            get { return _district; }
            set { _district = value; }
        }
        CityViewModel _city;

        public CityViewModel city
        {
            get { return _city; }
            set { _city = value; }
        }

        string _fullAddress;

        public string fullAddress
        {
            get { return _fullAddress; }
            set { _fullAddress = value; }
        }

        double _longitude;

        public double longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        double _latitude;

        public double latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
    }
}
