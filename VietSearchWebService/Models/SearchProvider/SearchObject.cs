using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VietSearchWebService.Models.SearchProvider
{
    public class SearchObject
    {
        public SearchObject()
        {
            placeId ="";
            cityId = "";
            districtId = "";
            streetId = "";
            placeTypeId = "";
        }
        string _placeId;

        public string placeId 
        {
            get { return _placeId; }
            set { _placeId = value; }
        }


        string _streetId;

        public string streetId
        {
            get { return _streetId; }
            set { _streetId = value; }
        }


        string _placeTypeId;

        public string placeTypeId
        {
            get { return _placeTypeId; }
            set { _placeTypeId = value; }
        }

        string _districtId;

        public string districtId
        {
            get { return _districtId; }
            set { _districtId = value; }
        }

        string _cityId;

        public string cityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }

        


       
    }
}