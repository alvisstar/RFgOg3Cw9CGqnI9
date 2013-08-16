using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearch.Utility;
using VietSearchWebService.Models.ModelObject;
using VietSearchWebService.Models.ModelManager;

namespace VietSearchWebService.Models.SearchProvider
{
    public class SearchProvider
    {
        PlaceTypeSearchProvider placeTypeSearchProvider;
        List<SearchObject> listSearchObject;
        VietSearchContext vietSearchContext;

        const int MAX_RESULT =20;

        public SearchProvider()
        {
            placeTypeSearchProvider = new PlaceTypeSearchProvider();
            listSearchObject = new List<SearchObject>();
            vietSearchContext = new VietSearchContext();
        }


        public SearchResultObject Search(string keyword,string cityId,int index)
        {

            SearchResultObject searchResultObject = new SearchResultObject();
            List<Place> listPlace = new List<Place>();
            keyword = StringHelper.StandardizeString(keyword);
            listSearchObject.AddRange(placeTypeSearchProvider.SearchWithPlaceTypeKeyword(keyword, cityId));
            int numSearchObject = listSearchObject.Count;
             
            
            for (int i = 0; i < numSearchObject; i++)
            {
                string strCityId = listSearchObject[i].cityId;
                string strDistrictId = listSearchObject[i].districtId;
                string strStreetId = listSearchObject[i].streetId;
                string strPlaceTypeId = listSearchObject[i].placeTypeId;
                string strPlaceId = listSearchObject[i].placeId;
                List<Place> listTemp = new List<Place>();

                var count = vietSearchContext.places.Count(r => r.cityId.Contains(strCityId)
                    && r.districtId.Contains(strDistrictId)
                    && r.placeTypeId.Contains(strPlaceTypeId)
                    && r.streetId.Contains(strStreetId)
                    && r.placeId.Contains(strPlaceId)
                    && r.isLock == false);
                             

                var temp = vietSearchContext.places.Where(r => r.cityId.Contains(strCityId)
                    && r.districtId.Contains(strDistrictId)
                    && r.placeTypeId.Contains(strPlaceTypeId)
                    && r.streetId.Contains(strStreetId)
                    && r.placeId.Contains(strPlaceId)
                    &&r.isLock==false).OrderBy(m=>m.placeId).Skip(index*MAX_RESULT).Take(MAX_RESULT).Select(r => new {r.street,r.district,r.city, r.placeType, r.placeId, r.placeName,r.homeNumber,r.longitude,r.latitude }).ToList();
                for (int j = 0; j < temp.Count; j++)
                {
                    listTemp.Add(new Place { street = temp[j].street, district = temp[j].district,city = temp[j].city, placeType = temp[j].placeType, placeId = temp[j].placeId, placeName = temp[j].placeName, homeNumber = temp[j].homeNumber, longitude = temp[j].longitude, latitude = temp[j].latitude });
                }
                searchResultObject.numResultPlace += count;
                searchResultObject.listResultPlace.AddRange(listTemp);
            }
            return searchResultObject;
        }

        
    }
}