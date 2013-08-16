using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace VietSearchWebService.Models.ModelObject
{
    public class Rate
    {
        public Rate()
        {
            account = new Account();
            place = new Place();
        }
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

        int _mark;

        public int mark
        {
            get { return _mark; }
            set { _mark = value; }
        }
        
        bool _isLock;

        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }
    }
}
