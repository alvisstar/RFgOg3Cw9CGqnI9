using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearchWebService.Models.ModelObject;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            
            this.HasKey(t => t.districtId);
            
            this.ToTable("District");

            this.Property(t => t.districtId)
                .HasColumnName("DistrictId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            this.Property(t => t.districtName)
                .HasColumnName("DistrictName")
                .HasMaxLength(200);
            
            this.Property(t => t.cityId)
                .HasColumnName("CityId");
            this.HasRequired(m => m.city).WithMany(m=>m.listDistrict).HasForeignKey(p=>p.cityId);   
                
            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}