using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            this.HasKey(t => new {t.menuItemId, t.placeId });
          //  this.HasKey(t => t.menuItemId);

            this.ToTable("Menu");

            this.Property(t => t.placeId)
                .HasColumnName("PlaceId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(m => m.place).WithMany(m => m.listMenu).HasForeignKey(p => p.placeId);

            this.Property(t => t.menuItemId)
                .HasColumnName("MenuItemId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(m => m.menuItem).WithMany(m => m.listMenu).HasForeignKey(p => p.menuItemId);

            this.Property(t => t.price)
                .HasColumnName("Price");

           
            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}