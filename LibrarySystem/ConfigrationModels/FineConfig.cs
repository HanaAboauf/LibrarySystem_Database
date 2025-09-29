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
    internal class FineConfig : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
 
            #region Main configration of Entity

            builder.Property(f=>f.Amount)
                     .HasColumnType("decimal")
                     .HasPrecision(6,2);

            builder.Property(f => f.IssueDate)
                   .HasDefaultValueSql("GETDATE()");


            builder.Property(f=>f.FineStatus)
                   .HasConversion<string>();

            #endregion
        }
    }
}
