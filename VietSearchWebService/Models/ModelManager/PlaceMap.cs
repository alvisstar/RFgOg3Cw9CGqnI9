using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class PlaceMap : EntityTypeConfiguration<Place>
    {
        public PlaceMap()
        {
            
            this.HasKey(t => t.placeId);
            
            this.ToTable("Place");

            this.Property(t => t.placeId)
                .HasColumnName("PlaceId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            this.Property(t => t.placeName)
                .HasColumnName("PlaceName")
                .HasMaxLength(200);
            
            this.Property(t => t.placeTypeId)
                .HasColumnName("PlaceTypeId");
            this.HasRequired(m => m.placeType).WithMany(m=>m.listPlace).HasForeignKey(p=>p.placeTypeId);

            this.Property(t => t.homeNumber)
               .HasColumnName("HomeNumber");

            this.Property(t => t.streetId)
                .HasColumnName("StreetId");
            this.HasRequired(m => m.street).WithMany(m => m.listPlace).HasForeignKey(p => p.streetId);

            this.Property(t => t.districtId)
                .HasColumnName("DistrictId");
            this.HasRequired(m => m.district).WithMany(m => m.listPlace).HasForeignKey(p => p.districtId);

            this.Property(t => t.cityId)
               .HasColumnName("CityId");
            this.HasRequired(m => m.city).WithMany(m => m.listPlace).HasForeignKey(p => p.cityId);

            this.Property(t => t.placeContact)
               .HasColumnName("PlaceContact");

            this.Property(t => t.placeIntroduce)
               .HasColumnName("PlaceIntroduce");

            this.Property(t => t.longitude)
               .HasColumnName("Longitude");

            this.Property(t => t.latitude)
               .HasColumnName("Latitude");

            this.Property(t => t.phone)
               .HasColumnName("Phone");

            this.Property(t => t.website)
               .HasColumnName("Website");
            this.Property(t => t.rating)
               .HasColumnName("Rating");
            this.Property(t => t.numberRating)
               .HasColumnName("NumberRating");

            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}