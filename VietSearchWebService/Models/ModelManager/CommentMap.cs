﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VietSearchWebService.Models.ModelObject;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietSearchWebService.Models.ModelManager
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            this.HasKey(t => new { t.accountId, t.placeId,t.createDate });
          //  this.HasKey(t => t.accountId);
            //this.HasKey(t => t.ordinal  );

            this.ToTable("Comment");

            this.Property(t => t.placeId)
                .HasColumnName("PlaceId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(m => m.place).WithMany(m => m.listComment).HasForeignKey(p => p.placeId);

            this.Property(t => t.accountId)
                .HasColumnName("AccountId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(m => m.account).WithMany(m => m.listComment).HasForeignKey(p => p.accountId);

            this.Property(t => t.createDate)
                .HasColumnName("CreateDate")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); ;

            this.Property(t => t.commentContent)
                .HasColumnName("CommentContent");

            this.Property(t => t.isLock)
                .HasColumnName("IsLock");
        }
    }
}