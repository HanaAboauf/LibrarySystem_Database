using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.ConfigrationModels
{
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            #region Main configration of Entity


            builder.Property(c => c.Title)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(c=>c.Description)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            #endregion
        }
    }
}
