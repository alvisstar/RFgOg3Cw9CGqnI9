using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearch.Utility.Keyword;
using VietSearch.Utility;

namespace VietSearchWebService.Models.SearchProvider
{
    public class PlaceTypeSearchProvider
    {
        DistrictSearchProvider districtearchProvider;
        public PlaceTypeSearchProvider()
        {
            districtearchProvider = new DistrictSearchProvider();
        }

        public List<SearchObject> SearchWithPlaceTypeKeyword(string keyword,string cityId)
        {

            XMLHelper.InitDocPlaceType(AppDomain.CurrentDomain.BaseDirectory + "\\placetypekeyword.xml");
            List<PlaceTypeKeyword> listPlaceTypeKeyword = XMLHelper.GetListPlaceTypeId(keyword);
            List<SearchObject> listSearchObject = new List<SearchObject>();
            int numPlaceTypeKeyword = listPlaceTypeKeyword.Count;
            if (numPlaceTypeKeyword > 0)
            {
                string strTemp = string.Empty;
                for (int i = 0; i < numPlaceTypeKeyword; i++)
                {
                    strTemp = StringHelper.Erase(keyword, listPlaceTypeKeyword[i].placeTypeKeywordName);

                    if (strTemp.Length > 0)
                    {
                        listSearchObject.AddRange(districtearchProvider.SearchWithDistrictKeyword(strTemp, cityId, listPlaceTypeKeyword[i].placeTypeKeywordId));
                    }
                    else
                    {
                        SearchObject searchObject = new SearchObject();
                        searchObject.cityId = cityId;
                        searchObject.placeTypeId = listPlaceTypeKeyword[i].placeTypeKeywordId;
                        listSearchObject.Add(searchObject);     
                    }
                }
            }
            else
            listSearchObject.AddRange(districtearchProvider.SearchWithDistrictKeyword(keyword, cityId,string.Empty));
            return listSearchObject;
        }
    }
}