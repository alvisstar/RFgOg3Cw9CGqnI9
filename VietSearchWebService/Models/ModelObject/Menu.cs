using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VietSearchWebService.Models.ModelObject
{
    public class Menu
    {
        public Menu()
        {
            menuItem = new MenuItem();
            place = new Place();
        }
        string _menuItemId;

        public string menuItemId
        {
            get { return _menuItemId; }
            set { _menuItemId = value; }
        }

        MenuItem _menuItem;

        public MenuItem menuItem
        {
            get { return _menuItem; }
            set { _menuItem = value; }
        }

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
        double _price;

        public double price
        {
            get { return _price; }
            set { _price = value; }
        }
        bool _isLock;

        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }
    }
}