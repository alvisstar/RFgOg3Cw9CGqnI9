using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using VietSearch.Utility.Keyword;

namespace VietSearch.Utility
{
    public class XMLHelper
    {
        private static XmlDocument docStreet;
        private static XmlDocument docPlaceType; 
        private static XmlDocument docPlace;
        private static XmlDocument docDistrict;
        public static void InitDocStreet(string xmlFile)
        {
            docStreet = new XmlDocument();
            docStreet.Load(xmlFile);
            
        }
        public static void InitDocPlaceType(string xmlFile)
        {
            docPlaceType = new XmlDocument();
            docPlaceType.Load(xmlFile);

        }
        public static void InitDocPlace(string xmlFile)
        {
            docPlace = new XmlDocument();
            docPlace.Load(xmlFile);

        }
        public static void InitDocDistrict(string xmlFile)
        {
            docDistrict = new XmlDocument();
            docDistrict.Load(xmlFile);

        }
        public static List<StreetKeyword> GetListStreetId(string strInput)
        {
           
           
            List<StreetKeyword> listStreetKeyword = new List<StreetKeyword>();
            if (docStreet == null)
            {
                InitDocStreet("ex.xml");
            }
            XmlElement e = docStreet.DocumentElement;
            XmlNodeList notes = e.SelectNodes("/Streets/Street/Name[contains('" + 
                strInput + "',text())"+
                "and not(@Para=preceding-sibling::Streets/Street/Name/@Para)]");
            
            
            int n = notes.Count;
            string[] listStrId = new string[n];
            for(int i = 0 ; i < n ; i++)
            {
                StreetKeyword streetKeyword = new StreetKeyword();
                streetKeyword.streetKeywordName = notes.Item(i).InnerText;
                XmlElement note =(XmlElement) notes.Item(i).ParentNode ;
                streetKeyword.streetKeywordId = note.GetAttribute("id");
                listStreetKeyword.Add(streetKeyword);
            }
            return listStreetKeyword;            
        }

        public static List<PlaceTypeKeyword> GetListPlaceTypeId(string strInput)
        {
            List<PlaceTypeKeyword> listPlaceTypeKeyword = new List<PlaceTypeKeyword>();
            if (docPlaceType == null)
            {
                InitDocPlaceType("ex.xml");
            }
            XmlElement e = docPlaceType.DocumentElement;
            XmlNodeList notes = e.SelectNodes("/PlaceTypes/PlaceType/Name[contains('" +
                strInput + "',text())" +
                "and not(@Para=preceding-sibling::PlaceTypes/PlaceType/Name/@Para)]");


            int n = notes.Count;
            string[] listStrId = new string[n];
            for (int i = 0; i < n; i++)
            {
                PlaceTypeKeyword placeTypeKeyword = new PlaceTypeKeyword();
                placeTypeKeyword.placeTypeKeywordName = notes.Item(i).InnerText;
                XmlElement note = (XmlElement)notes.Item(i).ParentNode;
                placeTypeKeyword.placeTypeKeywordId = note.GetAttribute("id");
                listPlaceTypeKeyword.Add(placeTypeKeyword);
            }
            return listPlaceTypeKeyword;
        }
        public static List<PlaceKeyword> GetListPlaceId(string strInput)
        {
            List<PlaceKeyword> listPlaceKeyword = new List<PlaceKeyword>();
            if (docPlace == null)
            {
                InitDocPlace("ex.xml");
            }
            XmlElement e = docPlace.DocumentElement;
            XmlNodeList notes = e.SelectNodes("/Places/Place/Name[contains('" +
                strInput + "',text())" +
                "and not(@Para=preceding-sibling::Places/Place/Name/@Para)]");


            int n = notes.Count;
            string[] listStrId = new string[n];
            for (int i = 0; i < n; i++)
            {
                PlaceKeyword placeKeyword = new PlaceKeyword();
                placeKeyword.placeKeywordName = notes.Item(i).InnerText;
                XmlElement note = (XmlElement)notes.Item(i).ParentNode;
                placeKeyword.placeKeywordId = note.GetAttribute("id");
                listPlaceKeyword.Add(placeKeyword);
            }
            return listPlaceKeyword;
        }

        public static List<DistrictKeyword> GetListDistrictId(string strInput)
        {
            List<DistrictKeyword> listDistrictKeyword = new List<DistrictKeyword>();
            if (docDistrict == null)
            {
                InitDocDistrict("ex.xml");
            }
            XmlElement e = docDistrict.DocumentElement;
            XmlNodeList notes = e.SelectNodes("/Districts/District/Name[contains('" +
                strInput + "',text())" +
                "and not(@Para=preceding-sibling::Districts/District/Name/@Para)]");


            int n = notes.Count;
            string[] listStrId = new string[n];
            for (int i = 0; i < n; i++)
            {
                DistrictKeyword districtKeyword = new DistrictKeyword();
                districtKeyword.districtKeywordName = notes.Item(i).InnerText;
                XmlElement note = (XmlElement)notes.Item(i).ParentNode;
                districtKeyword.districtKeywordId = note.GetAttribute("id");
                listDistrictKeyword.Add(districtKeyword);
            }
            return listDistrictKeyword;
        }
    }
}
