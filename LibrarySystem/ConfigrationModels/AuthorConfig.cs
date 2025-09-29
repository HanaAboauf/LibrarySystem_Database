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
    internal class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            #region Main configration of Entity

            builder.Property(a=>a.FirstName)
                   .HasColumnType("varchar")
                   .HasMaxLength(20);


            builder.Property(a => a.LastName)
                   .HasColumnType("varchar")
                   .HasMaxLength(20);


            #endregion
        }
    }
}
