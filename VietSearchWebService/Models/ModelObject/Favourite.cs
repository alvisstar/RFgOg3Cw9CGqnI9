using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VietSearchWebService.Models.ModelObject
{
    public class Favourite
    {
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
    }
}