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
    public class AccountViewModel
    {
        string _accountId ="";

        public string accountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
        string _accountName ="";

        public string accountName
        {
            get { return _accountName; }
            set { _accountName = value; }
        }
        bool _isLock =false;

        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }
    }
}
