using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VietSearchWebService.Models.AutoCompleteProvider
{
    public class SuggestObject
    {
        int _ordinal;

        public int ordinal
        {
            get { return _ordinal; }
            set { _ordinal = value; }
        }
        string _suggestKeyword;

        public string suggestKeyword
        {
            get { return _suggestKeyword; }
            set { _suggestKeyword = value; }
        }
    }
}