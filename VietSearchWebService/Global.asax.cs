using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using VietSearchWebService.Models.ModelManager;
using VietSearch.Utility;

namespace VietSearchWebService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<VietSearchContext>(null);
            //InitXml();
        }

        public void InitXml()
        {
            XMLHelper.InitDocStreet(AppDomain.CurrentDomain.BaseDirectory + "\\streetkeyword.xml");
            XMLHelper.InitDocPlaceType(AppDomain.CurrentDomain.BaseDirectory + "\\placetypekeyword.xml");
            XMLHelper.InitDocPlace(AppDomain.CurrentDomain.BaseDirectory + "\\placekeyword.xml");
            XMLHelper.InitDocDistrict(AppDomain.CurrentDomain.BaseDirectory + "\\districtkeyword.xml");
        }

       
    }
}