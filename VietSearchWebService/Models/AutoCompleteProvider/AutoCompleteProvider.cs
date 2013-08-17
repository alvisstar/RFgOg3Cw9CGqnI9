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
        
        SortedList<string, string> sListPlaceType = null;
        SortedList<string, string> sListStreet = null;
        SortedList<string, string> sListDistrict = null;
        
        List<Street> listStreet = new List<Street>();
        List<District> listDistrict = new List<District>();
        List<PlaceType> listPlaceType = new List<PlaceType>();

        VietSearchContext vietSearchContext;

        private static AutoCompleteProvider autoCompleteProvider = null;

        public static AutoCompleteProvider getInstance()
        {
            if (autoCompleteProvider == null)
                autoCompleteProvider = new AutoCompleteProvider();
            return autoCompleteProvider;
        }

        private AutoCompleteProvider()
        {
            vietSearchContext = new VietSearchContext();
            listSuggest = new List<string>();

            var streetQuery = (from street in vietSearchContext.streets where street.isLock == false select new { street, street.district }).ToList();
           
            listStreet = streetQuery.Select(t => new Street { streetId = t.street.streetId, streetName=t.street.streetName,district = t.district }).ToList();
           
            listDistrict = (from district in vietSearchContext.districts where district.isLock == false select district).ToList();
            listPlaceType = (from placeType in vietSearchContext.placeTypes where placeType.isLock == false select placeType).ToList();

            sListStreet = new SortedList<string, string>();
            sListDistrict = new SortedList<string, string>();
            sListPlaceType = new SortedList<string, string>();

            foreach (Street street in listStreet)
            {
                try
                {
                    sListStreet.Add(StringHelper.StandardizeString(street.streetName), street.streetName);
                }
                catch
                {
                }
            }

            foreach (District district in listDistrict)
            {
                try
                {
                    sListDistrict.Add(StringHelper.StandardizeString(district.districtName), district.districtName);
                }
                catch
                {
                }
            }
            foreach (PlaceType placeType in listPlaceType)
            {
                try
                {
                    sListPlaceType.Add(StringHelper.StandardizeString(placeType.placeTypeName), placeType.placeTypeName);
                }
                catch
                {
                }
            }

        }

        public void AnalysisKeyword(string keyword, string placeTypeName, string streetName, string districtName)
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
                SuugestPlaceType(keyword, placeTypeName, streetName, districtName);
            }
            if (0 == streetName.Length)
            {
                SuugestStreet(keyword, placeTypeName, streetName, districtName);
                
            }
            if (0 == districtName.Length)
            {
              //  if(streetName.Length==0)
                    SuugestDistrict(keyword, placeTypeName, streetName, districtName);
            }
        }

        public void SuugestPlaceType(string keyword, string placeTypeName, string streetName, string districtName)
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

                AnalysisKeyword(strTemp, listPlaceType[i], streetName, districtName);
                //if (listPlaceType[i].ToLower() != StringHelper.StandardizeString(listPlaceType[i]))
                //{
                //    AnalysisKeyword(strTemp, StringHelper.StandardizeString(listPlaceType[i]), streetName,districtName);
                //}
            }
        }

        public void SuugestStreet(string keyword, string placeTypeName, string streetName, string districtName)
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

                AnalysisKeyword(strTemp, placeTypeName, listStreet[i], districtName);
                //if (listStreet[i].ToLower() != StringHelper.StandardizeString(listStreet[i]))
                //{
                //    AnalysisKeyword(strTemp, placeTypeName, StringHelper.StandardizeString(listStreet[i]), districtName);
                //}
            }
        }

        public void SuugestDistrict(string keyword, string placeTypeName, string streetName, string districtName)
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

                AnalysisKeyword(strTemp,placeTypeName, streetName, listDistrict[i]);
               // if (listDistrict[i].ToLower() != StringHelper.StandardizeString(listDistrict[i]))
               // {
                   // AnalysisKeyword(strTemp, placeTypeName, streetName, StringHelper.StandardizeString(listDistrict[i]));
                //}
            }
        }
        public List<string> Suggest(string keyword)
        {
            List<string> listResult = null;
            try
            {
                if (null != listSuggest)
                {
                    listSuggest.Clear();
                }
                else
                {
                    listSuggest = new List<string>();
                }

                keyword = StringHelper.StandardizeString(keyword);
                this.AnalysisKeyword(keyword, String.Empty, String.Empty, String.Empty);
                listResult = listSuggest;
            }
            catch 
            {
                listResult = new List<string>();
            }

            return listResult;
        }


    }
}