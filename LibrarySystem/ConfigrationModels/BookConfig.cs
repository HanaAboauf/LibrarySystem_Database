using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.ConfigrationModels
{
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> builder)
        {
            #region Main configration of Entity

            builder.Property(b => b.Title)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(b => b.Price)
                   .HasColumnType("decimal")
                   .HasPrecision(6,2);

            builder.ToTable(
                t=>{
                    t.HasCheckConstraint("PublicationYearCheckConstraint", "PublicationYear between 1950 and YEAR(GETDATE())");
                    t.HasCheckConstraint("AvailableCopiesCheckConstraint", "AvailableCopies<= TotalCopies");
                });





            #endregion
        }
    }
}
