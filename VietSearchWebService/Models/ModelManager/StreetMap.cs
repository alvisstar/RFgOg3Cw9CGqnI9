using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class StreetMap : EntityTypeConfiguration<Street>
    {
        public StreetMap()
        {
            
            this.HasKey(t => t.streetId);
           
            this.ToTable("Street");

            this.Property(t => t.streetId)
                .HasColumnName("StreetId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            this.Property(t => t.streetName)
                .HasColumnName("StreetName")
                .HasMaxLength(200);
            
            this.Property(t => t.districtId)
                .HasColumnName("DistrictId");
            this.HasRequired(m => m.district).WithMany(m=>m.listStreet).HasForeignKey(p=>p.districtId);

            this.Property(t => t.cityId)
               .HasColumnName("CityId");
            this.HasRequired(m => m.city).WithMany(m => m.listStreet).HasForeignKey(p => p.cityId);   
                
            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}