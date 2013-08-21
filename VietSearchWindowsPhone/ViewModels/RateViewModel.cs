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
    public class RateViewModel
    {


        public RateViewModel()
        {
            place = new PlaceViewModel();
            account = new AccountViewModel();
        }
        string _placeId;

        public string placeId
        {
            get { return _placeId; }
            set { _placeId = value; }
        }
        string _accountId;

        public string accountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
        double _mark;

        public double mark
        {
            get { return _mark; }
            set { _mark = value; }
        }

        PlaceViewModel _place;

        public PlaceViewModel place
        {
            get { return _place; }
            set { _place = value; }
        }
        AccountViewModel _account;

        public AccountViewModel account
        {
            get { return _account; }
            set { _account = value; }
        }

        bool _isLock;

        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }

        
    }
}
