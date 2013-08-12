using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace VietSearch.Utility
{
    public class XMLHelper
    {
        private static XmlDocument docStreet;
        public static void Init(string xmlFile)
        {
            docStreet = new XmlDocument();
            docStreet.Load(xmlFile);
        }
        public static string[] GetListStreetIds(string strInput)
        {
            //XmlNode node = docStreet.FirstChild;
            //node.
            List<string> listStreetIds = new List<string>();
            if (docStreet == null)
            {
                Init("ex.xml");
            }
            XmlElement e = docStreet.DocumentElement;
            XmlNodeList notes = e.SelectNodes("/Streets/Street/Name[contains('" + 
                strInput + "',text())"+
                "and not(@City=preceding-sibling::Streets/Street/Name/@City)]/../@id");
            
            
            int n = notes.Count;
            string[] listStrIds = new string[n];
            for(int i = 0 ; i < n ; i++)
            {
                XmlNode note = notes.Item(i);
                listStrIds[i] = note.Value;
            }


            return listStrIds;
        }
    }
}
