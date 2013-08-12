using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class PlaceTypeMap:EntityTypeConfiguration<PlaceType>
    {
        public PlaceTypeMap()
        {
            
            this.HasKey(t => t.placeTypeId);
            
            this.ToTable("PlaceType");
            
            this.Property(t => t.placeTypeId)
                .HasColumnName("PlaceTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            this.Property(t => t.placeTypeName)
                .HasColumnName("PlaceTypeName")
                .HasMaxLength(200);
            
            this.Property(t => t.picture)
                .HasColumnName("Picture");
                
            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}