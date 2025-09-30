using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.ConfigrationModels
{
    internal class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {

            #region Main configration of Entity

            builder.Property(m=>m.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(m=>m.Email)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(m=>m.PhoneNumber)
                   .HasColumnType("varchar")
                   .HasMaxLength(11);

            builder.Property(m => m.Address)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(m => m.MembershipDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(m => m.MemberStatus)
                   .HasConversion<string>();



            builder.ToTable(
                t => {
                t.HasCheckConstraint("EmailCheckConstraint", "Email like '_%@_%._%'");
                t.HasCheckConstraint("PhoneNumberCheckConstraint", "PhoneNumber like '01%' ");
                });

            #endregion
        }
    }
}
