using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelObject
{
   
    public class City
    {
        public City()
        {
            listDistrict = new List<District>();            
            listStreet = new List<Street>();
            listPlace = new List<Place>();
        }

        string _cityId;       
        public string cityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }

        string _cityName;       
        public string cityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }
        
        bool _isLock;
        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }
        
        ICollection<District> _listDistrict;
        public ICollection<District> listDistrict
        {
            get { return _listDistrict; }
            set { _listDistrict = value; }
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