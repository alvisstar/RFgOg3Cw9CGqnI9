using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

namespace VietSearchWebService.Models.ModelObject
{
    public class PlaceType
    {
        public PlaceType()
        {
            listPlace = new List<Place>();
        }
        
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
       
        string _picture;
        public string picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
        
        bool _isLock;
        public bool  isLock
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
