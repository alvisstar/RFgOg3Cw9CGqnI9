using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using VietSearchWebService.Models.ModelManager;
using VietSearchWebService.Models.ModelObject;

namespace VietSearchWebService.Controllers
{
    public class ServiceController : ApiController
    {
        //
        // GET: /Service/

        VietSearchContext vietSearchContext;

        public ServiceController()
        {
            vietSearchContext = new VietSearchContext();
        }

        public string Get()
        {
           
            var roles = vietSearchContext.streets.Where(r => r.district.city.cityId == "CI100000049").Select(r => new { r.district,r.district.city,r.districtId,r.streetName }).ToList();         
            return "";
        }

    }
}
