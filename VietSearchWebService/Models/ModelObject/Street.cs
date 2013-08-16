using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace VietSearchWebService.Models.ModelObject
{
    public class Street
    {
        public Street()
        {
            listPlace = new List<Place>();
            district = new District();
            city = new City();
        }
        
        string _streetId;
        public string streetId
        {
            get { return _streetId; }
            set { _streetId = value; }
        }
        
        string _streetName;
        public string streetName
        {
            get { return _streetName; }
            set { _streetName = value; }
        }
        
        string _districtId;
        public string districtId
        {
            get { return _districtId; }
            set { _districtId = value; }
        }

        District _district;
        public District district
        {
            get { return _district; }
            set { _district = value; }
        }
        
        string _cityId;
        public string cityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }

        City _city;
        public City city
        {
            get { return _city; }
            set { _city = value; }
        }

        bool _isLock;
        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }

        ICollection<Place> _listPlace;
        public ICollection<Place> listPlace
        {
            get { return _listPlace; }
            set { _listPlace = value; }
        }
    }
}
