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
        public SearchProvider()
        {
            placeTypeSearchProvider = new PlaceTypeSearchProvider();
            listSearchObject = new List<SearchObject>();
            vietSearchContext = new VietSearchContext();
        }


        public List<Place> Search(string keyword,string cityId)
        {
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
                var a = vietSearchContext.places.Where(r => r.cityId.Contains(strCityId)
                    && r.districtId.Contains(strDistrictId)
                    && r.placeTypeId.Contains(strPlaceTypeId)
                    && r.streetId.Contains(strStreetId)
                    && r.placeId.Contains(strPlaceId)
                    &&r.isLock==false).Select(r => new {r.street, r.placeType, r.placeId, r.placeName,r.homeNumber,r.longitude,r.latitude }).ToList();
                for (int j = 0; j < a.Count; j++)
                {
                    listTemp.Add(new Place { street=a[j].street, placeType = a[j].placeType, placeId = a[j].placeId,placeName=a[j].placeName,homeNumber = a[j].homeNumber,longitude=a[j].longitude,latitude=a[j].latitude });
                }

                listPlace.AddRange(listTemp);
            }
            return listPlace;
        }

        
    }
}