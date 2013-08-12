using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class PlacePictureMap : EntityTypeConfiguration<PlacePicture>
    {
        public PlacePictureMap()
        {
            
            this.HasKey(t => t.placeId);
            this.HasKey(t => t.ordinal);
            
            this.ToTable("PlacePicture");
            
            this.Property(t => t.placeId)
                .HasColumnName("PlaceId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(m => m.place).WithMany(m => m.listPlacePicture).HasForeignKey(p => p.placeId);

            this.Property(t => t.ordinal)
                .HasColumnName("Ordinal")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.picture)
                .HasColumnName("Picture");

            this.Property(t => t.description)
               .HasColumnName("Description");
            
            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}