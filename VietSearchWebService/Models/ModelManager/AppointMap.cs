using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class AppointMap : EntityTypeConfiguration<Appoint>
    {
        public AppointMap()
        {
            
            this.HasKey(t => t.placeId);
            this.HasKey(t => t.accountId);
            
            this.ToTable("Appoint");
            
            this.Property(t => t.placeId)
                .HasColumnName("PlaceId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(m => m.place).WithMany(m => m.listAppoint).HasForeignKey(p => p.placeId);

            this.Property(t => t.accountId)
                .HasColumnName("AccountId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(m => m.account).WithMany(m => m.listAppoint).HasForeignKey(p => p.accountId);

            this.Property(t => t.appointContent)
                .HasColumnName("AppointContent");
                          
            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}