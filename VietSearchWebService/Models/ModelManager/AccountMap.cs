using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using VietSearchWebService.Models.ModelObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            
            this.HasKey(t => t.accountId);
            
            this.ToTable("Account");

            this.Property(t => t.accountId)
                .HasColumnName("AccountId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            this.Property(t => t.accountName)
                .HasColumnName("AccountName")
                .HasMaxLength(200);
            
            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}