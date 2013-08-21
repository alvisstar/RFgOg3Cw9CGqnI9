using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VietSearchWebService.Models.ModelObject;
using VietSearchWebService.Models.ModelManager;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace VietSearchWebService.Controllers
{
    public class AccountController : ApiController
    {
        VietSearchContext vietSearchContext;
        public AccountController()
        {
            vietSearchContext = new VietSearchContext();
            
        }
        public Account Get(string accountId)
        {
           

            Account account = new Account();
            account  =vietSearchContext.accounts.Where(r => r.accountId == accountId && r.isLock == false).FirstOrDefault();
            return account;
        }
        //public HttpResponseMessage Post(string accountJson)
        //{
        //    DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(Account));
        //    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(accountJson));
        //    Account value = (Account)dcjs.ReadObject(ms);  
        //    vietSearchContext.accounts.Add(value);
        //    vietSearchContext.SaveChanges();
        //    var response = Request.CreateResponse<Account>(HttpStatusCode.Created, value);
        //    string uri = Url.Link("DefaultApi", new { id = value.accountId });
        //    response.Headers.Location = new Uri(uri);
        //    return response;
        //}
        public HttpResponseMessage Post(Account value)
        {


            vietSearchContext.accounts.Add(value);
            vietSearchContext.SaveChanges();
            var response = Request.CreateResponse<Account>(HttpStatusCode.Created, value);
            string uri = Url.Link("DefaultApi", new { id = value.accountId });
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}
