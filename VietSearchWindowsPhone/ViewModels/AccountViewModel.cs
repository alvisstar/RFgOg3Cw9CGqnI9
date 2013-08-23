using System;
using System.ComponentModel;
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
    public class AccountViewModel:INotifyPropertyChanged
    {
        string _accountId;

        public string accountId
        {
            get { return _accountId; }
            set
            {
                if (value != _accountId)
                {
                    _accountId = value;
                    NotifyChanged("accountId");
                }
            }
        }
        string _accountName;

        public string accountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
                NotifyChanged("accountName");
            }
        }
        bool _isLock;

        public bool isLock
        {
            get { return _isLock; }
            set
            {
                _isLock = value;
                NotifyChanged("isLock");
            }
        }

        string _accountPicture;

        public string accountPicture
        {
            get { return _accountPicture; }
            set
            {
                _accountPicture = value;
                NotifyChanged("accountPicture");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
