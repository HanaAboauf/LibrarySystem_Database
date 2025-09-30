using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;
using LibrarySystem.Models;

namespace LibrarySystem.Contexts
{
    internal static class LibrarySystemDbContextSeeding
    {

        public static bool SeedData(LibrarySystemDbContext dbcontext)
        {
            var Transaction = dbcontext.Database.BeginTransaction(); //explicit transaction to ensure all or nothing (cause I use 2 saveChanges() that sends 2 requests to db)
            try
            {
                dbcontext.Database.Migrate();// to ensure that all migrations has been applied

                var hasAuthors = dbcontext.Authors.Any();
                var hasCategories = dbcontext.Categories.Any();
                var hasBooks = dbcontext.Books.Any();
                var hasMembers = dbcontext.Members.Any();

                if (hasAuthors && hasCategories && hasBooks && hasMembers) return false;
       
                if (!hasAuthors)
                {
                    var authors = LoadDataFromFile<Author>("SeedData/Authors.json");
                    dbcontext.Authors.AddRange(authors);
                }
                if (!hasCategories)
                {
                    var categories = LoadDataFromFile<Category>("SeedData/Categories.json");
                    dbcontext.Categories.AddRange(categories);
                }
                dbcontext.SaveChanges();

                if (!hasBooks)
                {
                    var books = LoadDataFromFile<Book>("SeedData/Books.json");
                    dbcontext.Books.AddRange(books);
                }
                if (!hasMembers)
                {
                    var members = LoadDataFromFile<Member>("SeedData/Members.json");
                    dbcontext.Members.AddRange(members);

                }
                dbcontext.SaveChanges();

                Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Transaction.Rollback();
                return false;

            }
        }

        private static List<T> LoadDataFromFile<T>(string filePath) {

            if (!File.Exists(filePath)) throw new FileNotFoundException($"The file at path {filePath} was not found.");

            var fileContent = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<List<T>>(fileContent,options)?? new List<T>();
        }
    }
}
