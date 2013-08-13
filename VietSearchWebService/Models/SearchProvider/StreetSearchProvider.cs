using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearch.Utility.Keyword;
using VietSearch.Utility;

namespace VietSearchWebService.Models.SearchProvider
{
    public class StreetSearchProvider
    {
        PlaceSearchProvider placeSearchProvider;
        public StreetSearchProvider()
        {
            placeSearchProvider = new PlaceSearchProvider();
        }

        public List<SearchObject> SearchWithStreetKeyword(string keyword,string cityId,string placeTypeId,string districtId)
        {
            XMLHelper.InitDocStreet(AppDomain.CurrentDomain.BaseDirectory + "\\streetkeyword.xml");
            List<StreetKeyword> listStreetKeyword = XMLHelper.GetListStreetId(keyword);
            List<SearchObject> listSearchObject = new List<SearchObject>();
            int numStreetKeyword = listStreetKeyword.Count;
            if (numStreetKeyword > 0)
            {
                string strTemp = string.Empty;
                for (int i = 0; i < numStreetKeyword; i++)
                {
                    strTemp = StringHelper.Erase(keyword, listStreetKeyword[i].streetKeywordName);

                    if (strTemp.Length > 0)
                    {
                        listSearchObject.AddRange(placeSearchProvider.SearchWithPlaceKeyword(strTemp, cityId,placeTypeId,districtId,listStreetKeyword[i].streetKeywordId));
                    }
                    else
                    {
                        SearchObject searchObject = new SearchObject();
                        searchObject.cityId = cityId;
                        searchObject.districtId = districtId;
                        searchObject.placeTypeId = placeTypeId;
                        searchObject.streetId = listStreetKeyword[i].streetKeywordId;
                        listSearchObject.Add(searchObject);              
                    }
                }
            }
            else
                listSearchObject.AddRange(placeSearchProvider.SearchWithPlaceKeyword(keyword, cityId, placeTypeId, districtId, string.Empty));
            return listSearchObject;
        }
    }
}