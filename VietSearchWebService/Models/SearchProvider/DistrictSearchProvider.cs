using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearch.Utility;
using VietSearch.Utility.Keyword;

namespace VietSearchWebService.Models.SearchProvider
{
    public class DistrictSearchProvider
    {
        StreetSearchProvider streetSearchProvider;
        public DistrictSearchProvider()
        {
            streetSearchProvider = new StreetSearchProvider();
        }

        public List<SearchObject> SearchWithDistrictKeyword(string keyword,string cityId,string placeTypeId)
        {
            XMLHelper.InitDocDistrict(AppDomain.CurrentDomain.BaseDirectory + "\\districtkeyword.xml");
            List<DistrictKeyword> listDistrictKeyword = XMLHelper.GetListDistrictId(keyword);
            List<SearchObject> listSearchObject = new List<SearchObject>();
            int numDistrictKeyword = listDistrictKeyword.Count;
            if (numDistrictKeyword > 0)
            {
                string strTemp = string.Empty;
                for (int i = 0; i < numDistrictKeyword; i++)
                {
                    strTemp = StringHelper.Erase(keyword, listDistrictKeyword[i].districtKeywordName);

                    if (strTemp.Length > 0)
                    {
                        listSearchObject.AddRange(streetSearchProvider.SearchWithStreetKeyword(strTemp, cityId,placeTypeId, listDistrictKeyword[i].districtKeywordId));
                    }
                    else
                    {
                        SearchObject searchObject = new SearchObject();
                        searchObject.cityId = cityId;                     
                        searchObject.placeTypeId = placeTypeId;
                        searchObject.streetId = listDistrictKeyword[i].districtKeywordId;
                        listSearchObject.Add(searchObject);         

                    }
                }
            }
            else
            listSearchObject.AddRange(streetSearchProvider.SearchWithStreetKeyword(keyword, cityId, placeTypeId, string.Empty));
            return listSearchObject;
        }
    }
}