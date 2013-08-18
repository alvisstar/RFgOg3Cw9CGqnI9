using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace VietSearchWindowsPhone.ViewModels
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            account = new AccountViewModel();
            place = new PlaceViewModel();
        }
        string _accountId = "";

        public string accountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        AccountViewModel _account ;

        public AccountViewModel account
        {
            get { return _account; }
            set { _account = value; }
        }

        string _placeId = "";

        public string placeId
        {
            get { return _placeId; }
            set { _placeId = value; }
        }

        PlaceViewModel _place;

        public PlaceViewModel place
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
        string _commentContent = "";

        public string commentContent
        {
            get { return _commentContent; }
            set { _commentContent = value; }
        }
        bool _isLock;

        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }
    }
}
