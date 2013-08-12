using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class MenuItemTypeMap : EntityTypeConfiguration<MenuItemType>
    {
        public MenuItemTypeMap()
        {
            this.HasKey(t => t.menuItemTypeId);

            this.ToTable("MenuItemType");

            this.Property(t => t.menuItemTypeId)
                .HasColumnName("MenuItemTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.menuItemTypeName)
                .HasColumnName("MenuItemTypeName")
                .HasMaxLength(200);

            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}