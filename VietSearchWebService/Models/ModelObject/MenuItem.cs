using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace VietSearchWebService.Models.ModelObject
{
    public class MenuItem
    {
        public MenuItem()
        {
            listMenu = new List<Menu>();
            menuItemType = new MenuItemType();
        }

        string _menuItemId;

        public string menuItemId
        {
            get { return _menuItemId; }
            set { _menuItemId = value; }
        }

        string _menuItemName;

        public string menuItemName
        {
            get { return _menuItemName; }
            set { _menuItemName = value; }
        }

        string _menuItemTypeId;

        public string menuItemTypeId
        {
            get { return _menuItemTypeId; }
            set { _menuItemTypeId = value; }
        }


        MenuItemType _menuItemType;

        public MenuItemType menuItemType
        {
            get { return _menuItemType; }
            set { _menuItemType = value; }
        }

        ICollection<Menu> _listMenu;

        public ICollection<Menu> listMenu
        {
            get { return _listMenu; }
            set { _listMenu = value; }
        }
        
    }
}
