using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace VietSearchWebService.Models.ModelObject
{
    public class Account
    {

        public Account()
        {
            listAppoint = new List<Appoint>();
            listComment = new List<Comment>();
            listRate = new List<Rate>();
            listFavourite = new List<Favourite>();
            listRecentlySearch = new List<RecentlySearch>();
        }

        string _accountId;

        public string accountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
        string _accountName;

        public string accountName
        {
            get { return _accountName; }
            set { _accountName = value; }
        }
        bool _isLock;

        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }

        ICollection<Appoint> _listAppoint;

        public ICollection<Appoint> listAppoint
        {
            get { return _listAppoint; }
            set { _listAppoint = value; }
        }

        ICollection<Comment> _listComment;

        public ICollection<Comment> listComment
        {
            get { return _listComment; }
            set { _listComment = value; }
        }

        ICollection<Rate> _listRate;

        public ICollection<Rate> listRate
        {
            get { return _listRate; }
            set { _listRate = value; }
        }
        ICollection<Favourite> _listFavourite;
        public ICollection<Favourite> listFavourite
        {
            get { return _listFavourite; }
            set { _listFavourite = value; }
        }

        ICollection<RecentlySearch> _listRecentlySearch;

        public ICollection<RecentlySearch> listRecentlySearch
        {
            get { return _listRecentlySearch; }
            set { _listRecentlySearch = value; }
        }

        private string _accountPicture;

        public string accountPicture
        {
            get { return _accountPicture; }
            set { _accountPicture = value; }
        }
    }
}
