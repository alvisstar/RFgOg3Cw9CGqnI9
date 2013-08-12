using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace VietSearchWebService.Models.ModelObject
{
    public class PlacePicture
    {
        string _placeId;

        public string placeId
        {
            get { return _placeId; }
            set { _placeId = value; }
        }

        Place _place;

        public Place place
        {
            get { return _place; }
            set { _place = value; }
        }

        int _ordinal;

        public int ordinal
        {
            get { return _ordinal; }
            set { _ordinal = value; }
        }
        string _picture;

        public string picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
        string _description;

        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        string _isLock;

        public string isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }
    }
}
