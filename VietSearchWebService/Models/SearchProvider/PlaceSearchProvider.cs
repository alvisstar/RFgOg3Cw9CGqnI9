using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearch.Utility.Keyword;
using VietSearch.Utility;

namespace VietSearchWebService.Models.SearchProvider
{
    public class PlaceSearchProvider
    {
        public PlaceSearchProvider()
        {
        }
        public List<SearchObject> SearchWithPlaceKeyword(string keyword, string cityId, string placeTypeId, string districtId, string streetId)
        {
            XMLHelper.InitDocPlace(AppDomain.CurrentDomain.BaseDirectory + "\\placekeyword.xml");
            List<PlaceKeyword> listPlaceKeyword = XMLHelper.GetListPlaceId(keyword);
            List<SearchObject> listSearchObject = new List<SearchObject>();
            int numPlaceKeyword = listPlaceKeyword.Count;
            if (numPlaceKeyword > 0)
            {
                string strTemp = String.Empty;
                for (int i = 0; i < numPlaceKeyword; i++)
                {
                    strTemp = StringHelper.Erase(keyword, listPlaceKeyword[i].placeKeywordName);
                    if (0 == strTemp.Length)
                    {
                        SearchObject searchObject = new SearchObject();
                        searchObject.cityId = cityId;
                        searchObject.districtId = districtId;
                        searchObject.streetId = streetId;
                        searchObject.placeTypeId = placeTypeId;
                        searchObject.placeId = listPlaceKeyword[i].placeKeywordId;
                        listSearchObject.Add(searchObject);                        
                    }
                }
            }

            return listSearchObject;
        }
    }
}