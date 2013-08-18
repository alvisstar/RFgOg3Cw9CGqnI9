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
    public class PlaceViewModel
    {

        public PlaceViewModel()
        {
            placeType = new PlaceTypeViewModel();
            street = new StreetViewModel();
            district = new DistrictViewModel();
            city = new CityViewModel();
        }
        string _placeId = "";

        public string placeId
        {
            get { return _placeId; }
            set
            {
                if (value != _placeId)
                {
                    _placeId = value;
                  //  NotifyChanged("placeId");
                }
            }
        }

        string _placeName = "";

        public string placeName
        {
            get { return _placeName; }
            set
            {
                if (value != _placeName)
                {
                    _placeName = value;
                   // NotifyChanged("placeName");
                }
            }
        }

        double _rate =0;

        public double rate
        {
            get { return _rate; }
            set { _rate = value; }
        }


        int _ordinal =1;

        public int ordinal
        {
            get { return _ordinal; }
            set
            {
                if (value != _ordinal)
                {
                    _ordinal = value;
                    //NotifyChanged("ordinal");
                }
            }
        }

        string _homeNumber = "";

        public string homeNumber
        {
            get { return _homeNumber; }
            set
            {
                if (value != _homeNumber)
                {
                    _homeNumber = value;
                    //NotifyChanged("homeNumber");
                }
            }
        }
        PlaceTypeViewModel _placeType;

        public PlaceTypeViewModel placeType
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

        string _fullAddress = ""; 

        public string fullAddress
        {
            get { return _fullAddress; }
            set
            {
                if (value != _fullAddress)
                {
                    _fullAddress = value;
                   // NotifyChanged("fullAddress");
                }
            }
        }

        double _longitude =0;

        public double longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        double _latitude =0;

        public double latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        

        //public event PropertyChangedEventHandler PropertyChanged;
        //public void NotifyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}
