using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using VietSearchWebService.Models.ModelManager;
using VietSearchWebService.Models.ModelObject;
using VietSearch.Utility;
using VietSearch.Utility.Keyword;
using VietSearchWebService.Models.SearchProvider;

namespace VietSearchWebService.Controllers
{
    public class ServiceController : ApiController
    {
        //
        // GET: /Service/

        
        SearchProvider searchProvider;
        public ServiceController()
        {

            
            searchProvider = new SearchProvider();
            
        }

        public List<Place> Get(string keyword,string cityId)
        {
            List<Place> listPlace = new List<Place>();
            keyword = StringHelper.StandardizeString(keyword);
            listPlace = searchProvider.Search(keyword, cityId);
            //List<District> listDistrict = new List<District>();

           
            //XMLHelper.InitDocStreet(AppDomain.CurrentDomain.BaseDirectory + "\\streetkeyword.xml");
            //List<StreetKeyword> list = XMLHelper.GetListStreetId(keyword);

            //var roles = vietSearchContext.streets.Where(r => r.district.city.cityId == "CI100000049").Select(r => new { r.district,r.district.city,r.districtId,r.streetName }).ToList();         
            return listPlace;
        }

    }
}
