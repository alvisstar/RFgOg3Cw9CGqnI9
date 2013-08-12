using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace VietSearchWebService.Models.ModelObject
{

    public class District
    {
        public District()
        {
            listStreet = new List<Street>();
            listPlace = new List<Place>();
        }

        string _districtId;       
        public string districtId
        {
            get { return _districtId; }
            set { _districtId = value; }
        }
        
        string _districtName;        
        public string districtName
        {
            get { return _districtName; }
            set { _districtName = value; }
        }

        string _cityId;        
        public string cityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }

        bool _isLock;
        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }

        City _city;
        public City city
        {
            get { return _city; }
            set { _city = value; }
        }

        ICollection<Street> _listStreet;
        public ICollection<Street> listStreet
        {
            get { return _listStreet; }
            set { _listStreet = value; }
        }

        ICollection<Place> _listPlace;
        public ICollection<Place> listPlace
        {
            get { return _listPlace; }
            set { _listPlace = value; }
        }
    }
}
