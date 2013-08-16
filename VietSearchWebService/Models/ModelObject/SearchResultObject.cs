using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VietSearchWebService.Models.ModelObject
{
    public class SearchResultObject
    {
        public SearchResultObject()
        {
            numResultPlace = 0;
            listResultPlace = new List<Place>();
        }
        int _numResultPlace;

        public int numResultPlace
        {
            get { return _numResultPlace; }
            set { _numResultPlace = value; }
        }

        List<Place> _listResultPlace;

        public List<Place> listResultPlace
        {
            get { return _listResultPlace; }
            set { _listResultPlace = value; }
        }

    }
}