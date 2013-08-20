using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VietSearchWebService.Models.ModelManager;
using VietSearchWebService.Models.ModelObject;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace VietSearchWebService.Controllers
{
    public class CommentController : ApiController
    {
        VietSearchContext vietSearchContext ;
        public CommentController()
        {
            vietSearchContext = new VietSearchContext();
        }
        public string Get(string json)
        {
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(Comment));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            Comment value = (Comment)dcjs.ReadObject(ms);


            try
            {
                vietSearchContext.Database.SqlQuerySmart<Comment>("sp_InsertComment", new
                {
                    AccountId = value.accountId,
                    PlaceId = value.placeId,
                    CommentContent = value.commentContent,
                    IsLock = value.isLock
                }).FirstOrDefault();
            }
            catch
            {
            }
 

            
            return "";
        }
        public HttpResponseMessage Post(Comment value)
        {


            vietSearchContext.comments.Add(value);
            vietSearchContext.SaveChanges();
            var response = Request.CreateResponse<Comment>(HttpStatusCode.Created, value);
            string uri = Url.Link("DefaultApi", new { accountId = value.accountId,placeId = value.placeId });
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}
