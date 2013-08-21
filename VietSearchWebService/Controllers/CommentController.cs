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
        public List<Comment> Get (string placeId,string parameter)
        {
            List<Comment> listComment = new List<Comment>();
            if (parameter.CompareTo("place") == 0)
            {
               
                var temp = (from comment in vietSearchContext.comments where comment.placeId == placeId && comment.isLock == false select new { comment, comment.account }).ToList();

                listComment = temp
                    .Select(p => new Comment
                    {
                        placeId = p.comment.placeId,
                        accountId = p.comment.accountId,
                        commentContent = p.comment.commentContent,
                        createDate = p.comment.createDate,
                        account = p.account
                    }).OrderByDescending(p=>p.createDate).ToList();
               
            }
            return listComment;
                            
        }
       
        public HttpResponseMessage Post(Comment value)
        {

            try
            {
                vietSearchContext.Database.ExecuteSqlCommandSmart("sp_InsertComment", new
                {
                    AccountId = value.accountId,
                    PlaceId = value.placeId,
                    CommentContent = value.commentContent,
                    IsLock = value.isLock
                });
            }
            catch
            {
            }
 
            var response = Request.CreateResponse<Comment>(HttpStatusCode.Created, value);
            string uri = Url.Link("DefaultApi", new { accountId = value.accountId,placeId = value.placeId });
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}
