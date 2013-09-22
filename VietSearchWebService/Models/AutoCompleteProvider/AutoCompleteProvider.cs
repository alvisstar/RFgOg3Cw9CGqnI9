using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearchWebService.Models.ModelObject;
using VietSearchWebService.Models.ModelManager;
using VietSearch.Utility;

namespace VietSearchWebService.Models.AutoCompleteProvider
{
    public class AutoCompleteProvider
    {
        const int MAX_SUGGTION = 8;

        List<string> listSuggest;
        SortedList<string, int> listSuggestHeristic = null;

        SortedList<int, string> sListPlaceType = null;
        SortedList<int, string> sListStreet = null;
        SortedList<int, string> sListDistrict = null;
        SortedList<int, string> sListPlace = null;
        
        List<Street> listStreet = new List<Street>();
        List<District> listDistrict = new List<District>();
        List<PlaceType> listPlaceType = new List<PlaceType>();
        List<Place> listPlace = new List<Place>();

        VietSearchContext vietSearchContext;

        private static AutoCompleteProvider autoCompleteProvider = null;

        public static AutoCompleteProvider getInstance()
        {
            if (autoCompleteProvider == null)
                autoCompleteProvider = new AutoCompleteProvider();
            return autoCompleteProvider;
        }

        public AutoCompleteProvider()
        {
            vietSearchContext = new VietSearchContext();
            listSuggest = new List<string>();

            var streetQuery = (from street in vietSearchContext.streets where street.isLock == false select new { street, street.district }).ToList();
           
            //listStreet = streetQuery.Select(t => new Street { streetId = t.street.streetId, streetName=t.street.streetName,district = t.district }).ToList();
            listStreet = (from street in vietSearchContext.streets where street.isLock == false select street).ToList();
            listDistrict = (from district in vietSearchContext.districts where district.isLock == false select district).ToList();
            listPlaceType = (from placeType in vietSearchContext.placeTypes where placeType.isLock == false select placeType).ToList();
            listPlace = (from place in vietSearchContext.places where place.isLock == false select place).ToList();

            sListStreet = new SortedList<int, string>();
            sListDistrict = new SortedList<int, string>();
            sListPlaceType = new SortedList<int, string>();
            sListPlace = new SortedList<int, string>();
            int id = 0;
            foreach (Street street in listStreet)
            {
                try
                {
                    sListStreet.Add(id++, StringHelper.StandardizeString(street.streetName));
                }
                catch
                {
                }
            }
            id = 0;
            foreach (District district in listDistrict)
            {
                try
                {
                    sListDistrict.Add(id++, StringHelper.StandardizeString(district.districtName));
                }
                catch
                {
                }
            }
            id = 0;
            foreach (PlaceType placeType in listPlaceType)
            {
                try
                {
                    sListPlaceType.Add(id++, StringHelper.StandardizeString(placeType.placeTypeName));
                }
                catch
                {
                }
            }
            id = 0;
            foreach (Place place in listPlace)
            {
                try
                {
                    sListPlace.Add(id++, StringHelper.StandardizeString(place.placeName));
                }
                catch
                {
                }
            }

        }
        /*
        public void AnalysisKeyword(string keyword, string placeTypeName, string placeName, string streetName, string districtName)
        {
            if (MAX_SUGGTION == listSuggest.Count)
            {
                return;
            }
            if (0 == keyword.Length)
            {
                string strTemp = String.Empty;
                if (placeTypeName.Length > 0)
                {
                    strTemp += placeTypeName;
                    strTemp += " ";
                }
                
                    
                
                if (streetName.Length > 0)
                {
                    strTemp += streetName;
                    strTemp += " ";
                }

                strTemp = strTemp.Trim();
                strTemp = StringHelper.ConvertToASCII(strTemp);
                listSuggest.Add(strTemp);
                if (districtName.Length > 0)
                {
                    if (listSuggest.Count > 0)
                    {
                        listSuggest.Remove(listSuggest.Last());
                       
                    }
                    List<string> listDistrictString = this.listStreet.Where(x => x.streetName == streetName).Select(x => x.district.districtName).ToList<string>();
                    if (streetName.Length > 0)
                    {
                        for (int i = 0; i < listDistrictString.Count; i++)
                        {
                            if (listDistrictString[i].CompareTo(districtName) == 0)
                            {
                                string strLastTemp = strTemp;
                                strLastTemp += " ";
                                strLastTemp += listDistrictString[i];
                                strLastTemp += " ";
                                strLastTemp = strLastTemp.Trim();
                                strTemp = StringHelper.ConvertToASCII(strTemp);
                                listSuggest.Add(strLastTemp);
                            }
                        }
                    }
                    else
                    {
                        strTemp += " ";
                        strTemp += districtName;
                        strTemp += " ";
                        strTemp = strTemp.Trim();
                        strTemp = StringHelper.ConvertToASCII(strTemp);
                        listSuggest.Add(strTemp);
                    }
                    
                }
               
                
                
            }
            if (0 == placeTypeName.Length)
            {
                SuugestPlaceType(keyword, placeTypeName, placeName, streetName, districtName);
            }
            if (0 == streetName.Length)
            {
                SuugestStreet(keyword, placeTypeName, placeName, streetName, districtName);
                
            }
            if (0 == districtName.Length)
            {
              //  if(streetName.Length==0)
                SuugestDistrict(keyword, placeTypeName, placeName, streetName, districtName);
            }
        }

        public void SuugestPlaceType(string keyword, string placeTypeName, string placeName, string streetName, string districtName)
        {
            List<string> listPlaceType = this.sListPlaceType.Where(x => x.Key.StartsWith(keyword) | keyword.StartsWith(x.Key)).Select(x => x.Value).ToList<string>();
            string strTemp = String.Empty;
            for (int i = 0; i < listPlaceType.Count; i++)
            {
                string strPlaceType = StringHelper.StandardizeString(listPlaceType[i]);
                if (strPlaceType.StartsWith(keyword))
                {
                    strTemp = String.Empty;
                }
                else
                {
                    strTemp = keyword.Replace(strPlaceType, String.Empty).Trim();
                }

                AnalysisKeyword(strTemp, listPlaceType[i], placeName, streetName, districtName);
            }
        }
        public void SuugestPlaceName(string keyword, string placeTypeName, string placeName, string streetName, string districtName)
        {
            List<string> listPlaceName = this.sListPlace.Where(x => x.Key.StartsWith(keyword) | keyword.StartsWith(x.Key)).Select(x => x.Value).ToList<string>();
            string strTemp = String.Empty;
            for (int i = 0; i < listPlaceName.Count; i++)
            {
                string strPlaceName = StringHelper.StandardizeString(listPlaceName[i]);
                if (strPlaceName.StartsWith(keyword))
                {
                    strTemp = String.Empty;
                }
                else
                {
                    strTemp = keyword.Replace(strPlaceName, String.Empty).Trim();
                }

                AnalysisKeyword(strTemp, listPlaceName[i], placeName, streetName, districtName);
            }
        }
        public void SuugestStreet(string keyword, string placeTypeName, string placeName, string streetName, string districtName)
        {
            List<string> listStreet = new List<String>();
            if(keyword=="")
            {
                if (districtName != "")
                {
                    //List<string> listStreetInDistrict = new List<string>();
                    //listStreetInDistrict = this.listStreet.Where(x => x.district.districtName == districtName).Take(MAX_SUGGTION).Select(x => x.streetName).ToList<string>();
                    //for (int i = 0; i < listStreetInDistrict.Count; i++)
                    //{
                    //    listStreet.Add(this.sListStreet.Where(x => x.Value == listStreetInDistrict[i]).Select(x => x.Value).First<string>());
                    //}
                }
                else
                    listStreet = this.sListStreet.Select(x => x.Value).Take(MAX_SUGGTION).ToList<string>();
            }
            else
                listStreet = this.sListStreet.Where(x => x.Key.StartsWith(keyword) | keyword.StartsWith(x.Key)).Select(x => x.Value).ToList<string>();
            string strTemp = String.Empty;
            for (int i = 0; i < listStreet.Count; i++)
            {
                string strStreet = StringHelper.StandardizeString(listStreet[i]);
                if (strStreet.StartsWith(keyword))
                {
                    strTemp = String.Empty;
                }
                else
                {
                    strTemp = keyword.Replace(strStreet, String.Empty).Trim();
                }

                AnalysisKeyword(strTemp, placeTypeName, placeName, listStreet[i], districtName);
            }
        }

        public void SuugestDistrict(string keyword, string placeTypeName, string placeName, string streetName, string districtName)
        {
            List<string> listDistrict = new List<string>();
            if(keyword=="")
                listDistrict = this.sListDistrict.Select(x => x.Value).Take(MAX_SUGGTION).ToList<string>();
            else
                listDistrict = this.sListDistrict.Where(x => x.Key.StartsWith(keyword) | keyword.StartsWith(x.Key)).Select(x => x.Value).ToList<string>();
            string strTemp = String.Empty;
            for (int i = 0; i < listDistrict.Count; i++)
            {
                string strDistrict = StringHelper.StandardizeString(listDistrict[i]);
                if (strDistrict.StartsWith(keyword))
                {
                    strTemp = String.Empty;
                }
                else
                {
                    strTemp = keyword.Replace(strDistrict, String.Empty).Trim();
                }

                AnalysisKeyword(strTemp, placeTypeName, placeName, streetName, listDistrict[i]);
               // if (listDistrict[i].ToLower() != StringHelper.StandardizeString(listDistrict[i]))
               // {
                   // AnalysisKeyword(strTemp, placeTypeName, streetName, StringHelper.StandardizeString(listDistrict[i]));
                //}
            }
        }
         */
        public List<string> Suggest(string keyword)
        {
            List<string> listResult = null;
            try
            {
                if (null != listSuggestHeristic)
                {
                    //listSuggest.Clear();
                    listSuggestHeristic.Clear();
                }
                else
                {
                   // listSuggest = new List<string>();
                    listSuggestHeristic = new SortedList<string, int>();
                }

                keyword = StringHelper.StandardizeString(keyword);
                this.AnalysisKeyword(keyword, -1, -1, -1, -1);
                listResult = listSuggestHeristic.OrderBy(x => x.Value).Select(x => x.Key).Take(MAX_SUGGTION).ToList<string>();
                //listResult = listSuggest;
            }
            catch 
            {
                listResult = new List<string>();
            }

            return listResult;
        }


        public void AnalysisKeyword(string keyword, int placeType, int place, int street, int district)
        {
            if (0 == keyword.Length)
            {
                if (place != -1)
                {
                    string strSuggest;
                    if (placeType == -1)
                    {
                        strSuggest = listPlaceType.Where(x => x.placeTypeId == listPlace[place].placeTypeId).First().placeTypeName;
                    }
                    else
                    {
                        strSuggest = listPlaceType[placeType].placeTypeName;
                    }
                    strSuggest += " " + listPlace[place].placeName;
                    if (street == -1)
                    {
                        strSuggest += ", " + listStreet.Where(x => x.streetId == listPlace[place].streetId).First().streetName;
                    }
                    else
                    {
                       strSuggest += ", " + listStreet[street].streetName;
                    }
                    if (district != -1)
                    {
                        strSuggest += ", " + listDistrict[district].districtName;
                    }
                    try
                    {

                        listSuggestHeristic.Add(strSuggest.Trim(), 10);
                    }
                    catch
                    {
                        int a = 0;
                    }

                }
                else
                {
                    string strSuggest = string.Empty;
                    int heristic = 0;
                    if (placeType != -1)
                    {
                        strSuggest += listPlaceType[placeType].placeTypeName + " ";
                        heristic += 3;
                    }
                    if (street != -1)
                    {
                        strSuggest += listStreet[street].streetName + " ";
                        heristic += 2;
                    }
                    if (district != -1)
                    {
                        strSuggest += listDistrict[district].districtName;
                        heristic++;
                    }
                    if (strSuggest.Length > 0)
                    {
                        listSuggestHeristic.Add(strSuggest.Trim(),heristic);
                    }
                }
                return;
            }
            if (-1 == placeType)
            {
                SuugestPlaceType(keyword, placeType, place, street, district);
            }
            if (-1 == place)
            {
                SuugestPlaceName(keyword, placeType, place, street, district);
            }
            if (-1 == street)
            {
                SuugestStreet(keyword, placeType, place, street, district);
            }
            if (-1 == district)
            {
                SuugestDistrict(keyword, placeType, place, street, district);
            }
        }

        public void SuugestPlaceType(string keyword, int placeType, int place, int street, int district)
        {
            List<int> listPlaceType = this.sListPlaceType.Where(x => x.Value.StartsWith(keyword) | keyword.StartsWith(x.Value)).Select(x => x.Key).Take(MAX_SUGGTION).ToList<int>();
            string strTemp = String.Empty;
            for (int i = 0; i < listPlaceType.Count; i++)
            {
                string strPlaceType;
                sListPlaceType.TryGetValue(listPlaceType[i], out strPlaceType);
                 //   ElementAt(listPlaceType[i]).Key;
             //       StringHelper.StandardizeString(listPlaceType[i]);
                if (strPlaceType.StartsWith(keyword))
                {
                    strTemp = String.Empty;
                }
                else
                {
                    strTemp = keyword.Replace(strPlaceType, String.Empty).Trim();
                }

                AnalysisKeyword(strTemp, listPlaceType[i], place, street, district);
            }
        }
        public void SuugestPlaceName(string keyword, int placeType, int place, int street, int district)
        {
            List<int> listPlaceIndex = this.sListPlace.Where(x => x.Value.StartsWith(keyword) | keyword.StartsWith(x.Value)).Select(x => x.Key).Take(MAX_SUGGTION).ToList<int>();
            string strTemp = String.Empty;
            for (int i = 0; i < listPlaceIndex.Count; i++)
            {
                int placeIndex = listPlaceIndex[i];
                if (
                    (placeType != -1 && listPlaceType[placeType].placeTypeId != listPlace[placeIndex].placeTypeId) ||
                    (street != -1 && listPlace[placeIndex].streetId != listStreet[street].streetId) ||
                    (district != -1 && listPlace[placeIndex].districtId != listDistrict[district].districtId)
                    )
                {
                    continue;
                }
                string strPlaceName;
                sListPlace.TryGetValue( placeIndex, out strPlaceName);
                    //StringHelper.StandardizeString(listPlaceName[i]);
                if (strPlaceName.StartsWith(keyword))
                {
                    strTemp = String.Empty;
                }
                else
                {
                    strTemp = keyword.Replace(strPlaceName, String.Empty).Trim();
                }

                AnalysisKeyword(strTemp, placeType, placeIndex, street, district);
            }
        }
        public void SuugestStreet(string keyword, int placeType, int place, int street, int district)
        {
            List<int> listStreetIndex = new List<int>();
           /* if (keyword == "")
            {
                if (-1 == district)
                {
                    //List<string> listStreetInDistrict = new List<string>();
                    //listStreetInDistrict = this.listStreet.Where(x => x.district.districtName == districtName).Take(MAX_SUGGTION).Select(x => x.streetName).ToList<string>();
                    //for (int i = 0; i < listStreetInDistrict.Count; i++)
                    //{
                    //    listStreet.Add(this.sListStreet.Where(x => x.Value == listStreetInDistrict[i]).Select(x => x.Value).First<string>());
                    //}
                }
                else
                    listStreet = this.sListStreet.Select(x => x.Key).Take(MAX_SUGGTION).ToList<int>();
            }
            else*/
            listStreetIndex = this.sListStreet.Where(x => x.Value.StartsWith(keyword) | keyword.StartsWith(x.Value)).Select(x => x.Key).Take(MAX_SUGGTION).ToList<int>();
            string strTemp = String.Empty;
            for (int i = 0; i < listStreetIndex.Count; i++)
            {
                int streetIndex = listStreetIndex[i];
                if ((district != -1 && listStreet[streetIndex].districtId != listDistrict[district].districtId) ||
                    (place != -1 && listPlace[place].streetId != listStreet[streetIndex].streetId))
                {
                    continue;
                }
                string strStreet;
                sListStreet.TryGetValue(streetIndex, out strStreet);
                    //StringHelper.StandardizeString(listStreet[i]);
                if (strStreet.StartsWith(keyword))
                {
                    strTemp = String.Empty;
                }
                else
                {
                    strTemp = keyword.Replace(strStreet, String.Empty).Trim();
                }

                AnalysisKeyword(strTemp, placeType, place, streetIndex, district);
            }
        }

        public void SuugestDistrict(string keyword, int placeType, int place, int street, int district)
        {
            List<int> listDistrictIndex = new List<int>();
            /*if (keyword == "")
                listDistrict = this.sListDistrict.Select(x => x.Key).Take(MAX_SUGGTION).ToList<int>();
            else*/
            listDistrictIndex = this.sListDistrict.Where(x => x.Value.StartsWith(keyword) | keyword.StartsWith(x.Value)).Select(x => x.Key).Take(MAX_SUGGTION).ToList<int>();
            string strTemp = String.Empty;
            for (int i = 0; i < listDistrictIndex.Count; i++)
            {
                int districtIndex = listDistrictIndex[i];

                if ((street != -1 && listStreet[street].districtId != listDistrict[districtIndex].districtId) ||
                    (place != -1 && listPlace[place].districtId != listDistrict[districtIndex].districtId))
                {
                    continue;
                }

                string strDistrict;
                sListDistrict.TryGetValue(districtIndex, out strDistrict);
                    //StringHelper.StandardizeString(listDistrict[i]);
                if (strDistrict.StartsWith(keyword))
                {
                    strTemp = String.Empty;
                }
                else
                {
                    strTemp = keyword.Replace(strDistrict, String.Empty).Trim();
                }

                AnalysisKeyword(strTemp, placeType, place, street, districtIndex);
                // if (listDistrict[i].ToLower() != StringHelper.StandardizeString(listDistrict[i]))
                // {
                // AnalysisKeyword(strTemp, placeTypeName, streetName, StringHelper.StandardizeString(listDistrict[i]));
                //}
            }
        }


    }
}

