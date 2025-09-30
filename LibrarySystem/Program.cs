using LibrarySystem.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
           using var dbcontext = new LibrarySystemDbContext();

            #region Seeding Data 
            //dbcontext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT (Authors, RESEED, 0);");

            //dbcontext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT (Categories, RESEED, 0);");


            //bool isSeeded = LibrarySystemDbContextSeeding.SeedData(dbcontext);

            //if (isSeeded)
            //{
            //    Console.WriteLine("Database has been seeded successfully.");
            //}
            //else
            //{
            //    Console.WriteLine("Database seeding failed.");
            //} 
            #endregion
        }
    }
}
