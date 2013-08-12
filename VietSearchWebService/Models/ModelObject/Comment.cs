using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace VietSearchWebService.Models.ModelObject
{
    public class Comment
    {
        string _accountId;

        public string accountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        Account _account;

        public Account account
        {
            get { return _account; }
            set { _account = value; }
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

        int _ordinal;

        public int ordinal
        {
            get { return _ordinal; }
            set { _ordinal = value; }
        }
        string _commentContent;

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
