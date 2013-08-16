using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using VietSearchWebService.Models.AutoCompleteProvider;

namespace VietSearchWebService.Controllers
{
    public class HandleInputController : ApiController
    {
        //
        // GET: /HandleInput/

        AutoCompleteProvider autoCompleteProvider;

        public HandleInputController()
        {
            autoCompleteProvider = AutoCompleteProvider.getInstance();
        }

        public List<string> Get(string keyword)
        {
            List<string> listSuggest = new List<string>();
            listSuggest = autoCompleteProvider.Suggest(keyword);
            return listSuggest;
        }

        public void Get()
        {
            autoCompleteProvider = AutoCompleteProvider.getInstance();
        }

    }
}
