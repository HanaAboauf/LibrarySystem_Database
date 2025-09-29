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
    internal class MemberLoanConfig : IEntityTypeConfiguration<MemberLoan>
    {
        public void Configure(EntityTypeBuilder<MemberLoan> builder)
        {
            #region Main configration of Entity
            builder.HasKey(builder => new { builder.MemberId, builder.LoanId, builder.BookId });

            builder.Property(ml => ml.DueDate)
                   .HasColumnType("datetime");

            builder.Property(ml => ml.ReturnDate)
                   .HasColumnType("datetime");

            #endregion

            #region Relationships configration
            builder.HasOne(ml => ml.Member)
                   .WithMany(m => m.MemberLoans)
                   .HasForeignKey(ml => ml.MemberId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ml => ml.Loan)
                   .WithMany(l => l.MemberLoans)
                   .HasForeignKey(ml => ml.LoanId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ml => ml.Book)
                   .WithMany(b => b.MemberLoans)
                   .HasForeignKey(ml => ml.BookId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }

    }

}
