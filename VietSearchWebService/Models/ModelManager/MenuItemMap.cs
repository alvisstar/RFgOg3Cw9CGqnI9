using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class MenuItemMap : EntityTypeConfiguration<MenuItem>
    {
        public MenuItemMap()
        {
            this.HasKey(t => t.menuItemId);

            this.ToTable("MenuItem");

            this.Property(t => t.menuItemId)
                .HasColumnName("MenuItemId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.menuItemName)
                .HasColumnName("MenuItemName")
                .HasMaxLength(200);

            this.Property(t => t.menuItemTypeId)
                .HasColumnName("MenuItemTypeId");
            this.HasRequired(m => m.menuItemType).WithMany(m => m.listMenuItem).HasForeignKey(p => p.menuItemTypeId); 
        }
    }
}