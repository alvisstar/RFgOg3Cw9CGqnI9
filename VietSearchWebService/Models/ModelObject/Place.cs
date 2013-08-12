using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace VietSearchWebService.Models.ModelObject
{
    public class Place
    {

        public Place()
        {
            listPlacePicture = new List<PlacePicture>();
            listAppoint = new List<Appoint>();
            listComment = new List<Comment>();
            listRate = new List<Rate>();
            listMenu = new List<Menu>();
        }

        string _placeId;
        public string placeId
        {
            get { return _placeId; }
            set { _placeId = value; }
        }

        string _placeName;
        public string placeName
        {
            get { return _placeName; }
            set { _placeName = value; }
        }

        string _placeTypeId;
        public string placeTypeId
        {
            get { return _placeTypeId; }
            set { _placeTypeId = value; }
        }

        PlaceType _placeType;
        public PlaceType placeType
        {
            get { return _placeType; }
            set { _placeType = value; }
        }

        string _homeNumber;
        public string homeNumber
        {
            get { return _homeNumber; }
            set { _homeNumber = value; }
        }

        string _streetId;
        public string streetId
        {
            get { return _streetId; }
            set { _streetId = value; }
        }

        Street _street;
        public Street street
        {
            get { return _street; }
            set { _street = value; }
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

        string _placeContact;
        public string placeContact
        {
            get { return _placeContact; }
            set { _placeContact = value; }
        }

        string _placeIntroduce;
        public string placeIntroduce
        {
            get { return _placeIntroduce; }
            set { _placeIntroduce = value; }
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

        string _phone;
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        string _website;
        public string website
        {
            get { return _website; }
            set { _website = value; }
        }

        bool _isLock;
        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }

        ICollection<PlacePicture> _listPlacePicture;

        public ICollection<PlacePicture> listPlacePicture
        {
            get { return _listPlacePicture; }
            set { _listPlacePicture = value; }
        }

        ICollection<Appoint> _listAppoint;

        public ICollection<Appoint> listAppoint
        {
            get { return _listAppoint; }
            set { _listAppoint = value; }
        }

        ICollection<Comment> _listComment;
        
        public ICollection<Comment> listComment
        {
            get { return _listComment; }
            set { _listComment = value; }
        }

        ICollection<Rate> _listRate;

        public ICollection<Rate> listRate
        {
            get { return _listRate; }
            set { _listRate = value; }
        }

        ICollection<Menu> _listMenu;

        public ICollection<Menu> listMenu
        {
            get { return _listMenu; }
            set { _listMenu = value; }
        }
    }
}
