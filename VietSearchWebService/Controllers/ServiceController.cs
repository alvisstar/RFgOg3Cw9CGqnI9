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
using VietSearchWebService.Models.AutoCompleteProvider;

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

        public SearchResultObject Get(string keyword,string cityId,int index)
        {
            SearchResultObject searchResultObject = new SearchResultObject();
            keyword = StringHelper.StandardizeString(keyword);
            searchResultObject = searchProvider.Search(keyword, cityId,index);           
            return searchResultObject;
        }

        
        

    }
}
