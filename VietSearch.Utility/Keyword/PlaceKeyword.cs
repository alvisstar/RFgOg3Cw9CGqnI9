using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VietSearch.Utility.Keyword
{
    public class PlaceKeyword
    {
        string _placeKeywordId;

        public string placeKeywordId
        {
            get { return _placeKeywordId; }
            set { _placeKeywordId = value; }
        }
        string _placeKeywordName;

        public string placeKeywordName
        {
            get { return _placeKeywordName; }
            set { _placeKeywordName = value; }
        }
    }
}
