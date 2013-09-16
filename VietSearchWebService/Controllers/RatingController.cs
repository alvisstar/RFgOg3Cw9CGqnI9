using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VietSearchWebService.Models.ModelManager;
using VietSearchWebService.Models.ModelObject;

namespace VietSearchWebService.Controllers
{
    public class RatingController : ApiController
    {
        VietSearchContext vietSearchContext;
        public RatingController()
        {
            vietSearchContext = new VietSearchContext();
        }
        public string Get()
        {
            //Rate value = new Rate { placeId = "PL100000002", mark = 4, isLock = false, accountId = "100000383508693" };
            //DbConnection dbConnection = ((IObjectContextAdapter)vietSearchContext).ObjectContext.Connection;
            //DbTransaction trans = null;
            //try
            //{
            //    dbConnection.Open();
            //    trans = dbConnection.BeginTransaction();
            //    Place placeResult = new Place();
            //    var temp = (from place in vietSearchContext.places where place.placeId == value.placeId && place.isLock == false select new { place }).ToList();
            //    placeResult = temp
            //        .Select(p => new Place
            //        {
            //            placeId = p.place.placeId,
            //            rating = p.place.rating,
            //            numberRating = p.place.numberRating,

            //        }).ToList().FirstOrDefault();


            //    var rating = (from rate in vietSearchContext.rates where rate.placeId == value.placeId && rate.accountId == value.accountId select new { rate }).ToList();
            //    if (rating.Count == 0)
            //    {
            //        vietSearchContext.Database.ExecuteSqlCommandSmart("sp_InsertRating", new
            //        {
            //            AccountId = value.accountId,
            //            PlaceId = value.placeId,
            //            Mark = value.mark,
            //            IsLock = value.isLock
            //        });
            //        placeResult.rating = ((placeResult.rating * placeResult.numberRating) + value.mark) / (placeResult.numberRating + 1);
            //        placeResult.numberRating += 1;
            //    }
            //    else
            //    {
            //        vietSearchContext.Database.ExecuteSqlCommandSmart("sp_UpdateRating", new
            //        {
            //            AccountId = value.accountId,
            //            PlaceId = value.placeId,
            //            Mark = value.mark,
            //            IsLock = value.isLock
            //        });
            //        placeResult.rating = ((placeResult.rating * placeResult.numberRating) - rating[0].rate.mark + value.mark) / (placeResult.numberRating);

            //    }

            //    vietSearchContext.Database.ExecuteSqlCommandSmart("sp_UpdateRatingPlace", new
            //    {

            //        PlaceId = placeResult.placeId,
            //        Rating = "a",//placeResult.rating,
            //        NumberRating = placeResult.numberRating
            //    });
            //    trans.Commit();
            //    return "";
            //}
            //catch
            //{
            //    trans.Rollback();
            //    return "";
            //}
            //finally
            //{
            //    dbConnection.Dispose();
            //}
            return "";
            
        }
        public HttpResponseMessage Post(Rate value)
        {
            
            DbConnection dbConnection = ((IObjectContextAdapter)vietSearchContext).ObjectContext.Connection;
            DbTransaction trans = null;
            try
            {
                dbConnection.Open();
                trans = dbConnection.BeginTransaction();

                Place placeResult = new Place();
                var temp = (from place in vietSearchContext.places where place.placeId == value.placeId && place.isLock == false select new { place }).ToList();

                placeResult = temp
                    .Select(p => new Place
                    {
                        placeId = p.place.placeId,
                        rating = p.place.rating,
                        numberRating = p.place.numberRating,

                    }).ToList().FirstOrDefault();
                var rating = (from rate in vietSearchContext.rates where rate.placeId == value.placeId && rate.accountId == value.accountId select new { rate }).ToList();
                if (rating.Count == 0)
                {
                    vietSearchContext.Database.ExecuteSqlCommandSmart("sp_InsertRating", new
                    {
                        AccountId = value.accountId,
                        PlaceId = value.placeId,
                        Mark = value.mark,
                        IsLock = value.isLock
                    });
                    placeResult.rating = ((placeResult.rating * placeResult.numberRating) + value.mark) / (placeResult.numberRating + 1);
                    placeResult.numberRating += 1;
                }
                else
                {
                    vietSearchContext.Database.ExecuteSqlCommandSmart("sp_UpdateRating", new
                    {
                        AccountId = value.accountId,
                        PlaceId = value.placeId,
                        Mark = value.mark,
                        IsLock = value.isLock
                    });
                    placeResult.rating = ((placeResult.rating * placeResult.numberRating) - rating[0].rate.mark + value.mark) / (placeResult.numberRating);

                }
                vietSearchContext.Database.ExecuteSqlCommandSmart("sp_UpdateRatingPlace", new
                {

                    PlaceId = placeResult.placeId,
                    Rating = placeResult.rating,
                    NumberRating = placeResult.numberRating
                });
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                dbConnection.Dispose();
            }

            var response = Request.CreateResponse<Rate>(HttpStatusCode.Created, value);
            string uri = Url.Link("DefaultApi", new { accountId = value.accountId, placeId = value.placeId });
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}
