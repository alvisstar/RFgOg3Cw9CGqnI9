using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VietSearchWebService.Models.ModelObject
{
    public class MenuItemType
    {
        public MenuItemType()
        {
            listMenuItem = new List<MenuItem>();
        }

        string _menuItemTypeId;

        public string menuItemTypeId
        {
            get { return _menuItemTypeId; }
            set { _menuItemTypeId = value; }
        }
        string _menuItemTypeName;

        public string menuItemTypeName
        {
            get { return _menuItemTypeName; }
            set { _menuItemTypeName = value; }
        }

        bool _isLock;

        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }

        ICollection<MenuItem> _listMenuItem;

        public ICollection<MenuItem> listMenuItem
        {
            get { return _listMenuItem; }
            set { _listMenuItem = value; }
        }
    }
}