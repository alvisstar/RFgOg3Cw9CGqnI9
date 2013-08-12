using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearchWebService.Models.ModelObject;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            
            this.HasKey(t => t.cityId);
            
            this.ToTable("City");
            
            this.Property(t => t.cityId)
                .HasColumnName("CityId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            this.Property(t => t.cityName)
                .HasColumnName("CityName")
                .HasMaxLength(200);
            
            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}