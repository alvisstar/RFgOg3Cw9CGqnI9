using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearchWebService.Models.ModelManager;
using VietSearchWebService.Models.ModelObject;
using System.Web.Http;

namespace VietSearchWebService.Controllers
{
    public class PlaceController:ApiController
    {
        VietSearchContext vietSearchContext = new VietSearchContext();
        public PlaceController()
        {
            vietSearchContext = new VietSearchContext();
        }

        public Place Get(string placeId)
        {
            Place placeResult = new Place();
           
            var temp = (from place in vietSearchContext.places where place.placeId==placeId && place.isLock == false select new { place,place.street, place.district,place.city,place.placeType,place.listFavourite,place.listComment,place.listMenu }).ToList();
           // var temp = vietSearchContext.places.Where(r => r.placeId == placeId && r.isLock == false)
                //.Select(p => new Place { placeId = p.placeId,street = p.street, district = p.district,city= p.city,placeType= p.placeType,listFavourite = p.listFavourite,listComment = p.listComment,listMenu = p.listMenu }).ToList();
            //placeResult = (Place)temp;
            placeResult= temp
                .Select(p => new Place { placeId = p.place.placeId,placeName = p.place.placeName,longitude = p.place.longitude,latitude=p.place.latitude, 
                    street = p.street, district = p.district, city = p.city, placeType = p.placeType, listFavourite = p.listFavourite, listComment = p.listComment, listMenu = p.listMenu }).ToList().FirstOrDefault();
            return placeResult;


        }
    }
}