using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class RecentlySearchMap : EntityTypeConfiguration<RecentlySearch>
    {
        public RecentlySearchMap()
        {
            this.HasKey(t => new { t.placeId, t.accountId });
            //this.HasKey(t => t.accountId);

            this.ToTable("RecentlySearch");

            this.Property(t => t.placeId)
                .HasColumnName("PlaceId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(m => m.place).WithMany(m => m.listRecentlySearch).HasForeignKey(p => p.placeId);

            this.Property(t => t.accountId)
                .HasColumnName("AccountId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(m => m.account).WithMany(m => m.listRecentlySearch).HasForeignKey(p => p.accountId);

           
        }
    }
}