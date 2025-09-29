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
    internal class LoanConfig : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {

            #region Main configration of Entity

            builder.Property(l=>l.LoanDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(l => l.LoanStatus)
                   .HasConversion<string>();

            #endregion
        }
    }
}
