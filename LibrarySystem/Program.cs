using LibrarySystem.Contexts;
using LibrarySystem.Models;
using LibrarySystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

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

            //dbcontext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT (Books, RESEED, 0);");

            //dbcontext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT (Members, RESEED, 0);");


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


            #region 9. Data Manipulation 

            int opChoice;
            bool flag;

            do
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1 - Retrieve books with price > entered value");
                Console.WriteLine("2 - Retrieve all authors and their books");
                Console.WriteLine("3 - Borrow a book");
                Console.WriteLine("4 - Return a book");
                Console.WriteLine("5 - Retrieve all members with active loans");
                Console.WriteLine("6 - Search books (title, author, category)");
                Console.WriteLine("7 - Exit");

                flag = int.TryParse(Console.ReadLine(), out opChoice);

            } while (!flag || opChoice < 0 || opChoice > 6);

            switch (opChoice)
            {
                case 1:
                    int price;
                    flag = int.TryParse(Console.ReadLine(), out price);
                    do
                    {
                        Console.WriteLine("Please Enter a valid price greater than 0");
                        flag = int.TryParse(Console.ReadLine(), out price);
                    } while (!flag || price <= 0);

                    #region  Retrieve the book title, its category title , and the author’s full name for all books whose price is greater than X.

                    var booksWithDetails = dbcontext.Books
                        .Where(b => b.Price > price)
                        .Select(b => new
                        {
                            BookTitle = b.Title,
                            CategoryTitle = b.Category.Title,
                            AuthorFullName = (b.Author != null ? b.Author.FirstName : " ") + " " + (b.Author != null ? b.Author.LastName : " ")
                        })
                        .ToList();

                    foreach (var book in booksWithDetails)
                    {
                        Console.WriteLine($"Book Title: {book.BookTitle}, Category: {book.CategoryTitle}, Author: {book.AuthorFullName}");
                    }
                    #endregion
                    break;
                case 2:
                    #region Retrieve All Authors And His/Her Books if Exists.

                    var AuthorWithBooks = dbcontext.Authors
                        .Include(a => a.Books)
                        .Select(a => new
                        {
                            AuthorFullName = (a.FirstName ?? " ") + " " + (a.LastName ?? " "),
                            BookTitles = a.Books != null && a.Books.Any()
                                         ? a.Books.Select(b => b.Title).ToList()
                                         : new List<string>()
                        })
                        .ToList();
                    foreach (var author in AuthorWithBooks)
                    {
                        Console.WriteLine($"Author name: {author.AuthorFullName}");
                        Console.WriteLine("Author books:");
                        foreach (var title in author.BookTitles)
                        {
                            Console.WriteLine($"{title} ");
                        }
                        Console.WriteLine();
                    }
                    #endregion
                    break;
                case 3:
                    #region Member with id x Want To Borrow The Book With Id y And He Will Return it After z Days 

                    int memberId, bookId, borrowingDays;
                    do
                    {
                        Console.WriteLine("Please Enter a valid member id");

                        flag = int.TryParse(Console.ReadLine(), out memberId);

                    } while (!flag);
                    do
                    {
                        Console.WriteLine("Please Enter a valid book id");
                        flag = int.TryParse(Console.ReadLine(), out bookId);
                    } while (!flag);

                    do
                    {
                        Console.WriteLine("Please Enter a valid number of days to borrow the book");
                        flag = int.TryParse(Console.ReadLine(), out borrowingDays);
                    } while (!flag);

                    var isBorrowed = Services.BorrowBook(memberId, bookId, borrowingDays, dbcontext);

                    Console.WriteLine(isBorrowed ? "The book has been borrowed successfully." : "Failed to borrow the book.");

                    #endregion
                    break;
                case 4:
                    #region Retrieve all members who currently have active loans (i.e., loans that have not yet been returned)

                    var activeLoanMembers = dbcontext.Members
                        .Where(m => m.MemberLoans.Any(ml => ml.Loan != null && ml.Loan.LoanStatus == LoanStatus.Borrowed))
                        .ToList();

                    Console.WriteLine("Members with active loans:");

                    foreach (var member in activeLoanMembers)

                        Console.WriteLine($"Member ID: {member.Id}, Name: {member.Name}");

                    #endregion
                    break;
                case 5:
                    #region Retrieve all members who currently have active loans (i.e., loans that have not yet been returned)

                    var MembersWithActiveLoan = dbcontext.Members
                        .Where(m => m.MemberLoans.Any(ml => ml.Loan != null && ml.Loan.LoanStatus == LoanStatus.Borrowed))
                        .ToList();

                    Console.WriteLine("Members with active loans:");

                    foreach (var member in MembersWithActiveLoan)

                        Console.WriteLine($"Member ID: {member.Id}, Name: {member.Name}");


                    #endregion
                    break;
                case 6:
                    #region search books by title, author, or category.


                    int option;

                    do
                    {
                        Console.WriteLine("Search Book By: 1- Title , 2- Author , 3- Category ");
                        flag = int.TryParse(Console.ReadLine(), out option);
                    } while (!flag || option < 1 || option > 3);


                    switch (option)
                    {
                        case 1:
                            string title;
                            do
                            {
                                Console.WriteLine("Please Enter a valid book title");
                                title = Console.ReadLine()!;
                            } while (string.IsNullOrWhiteSpace(title));
                            var booksByTitle = dbcontext.Books
                                .Where(b => b.Title == title)
                                .ToList();
                            if (booksByTitle.Any())
                            {
                                Console.WriteLine("Books found:");
                                foreach (var book in booksByTitle)
                                {
                                    Console.WriteLine($"Book ID: {book.Id}," +
                                                      $" Title: {book.Title}," +
                                                      $"Author ID: {book.AuthorId}" +
                                                      $" Price: {book.Price}," +
                                                      $" Available Copies: {book.AvailableCopies}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No books found with the given title.");
                            }
                            break;
                        case 2:
                            string authorFName, authorLName;
                            DateTime DOB;
                            bool result;
                            do
                            {
                                Console.WriteLine("Please Enter a valid author first name");
                                authorFName = Console.ReadLine()!;
                                Console.WriteLine("Please Enter a valid author last name");
                                authorLName = Console.ReadLine()!;
                                Console.WriteLine("Please Enter a valid author's DOB");
                                result = DateTime.TryParse(Console.ReadLine(), out DOB);
                            } while (string.IsNullOrWhiteSpace(authorFName) || string.IsNullOrWhiteSpace(authorLName) || !result);

                            var author = dbcontext.Authors
                                .FirstOrDefault(a => a.FirstName == authorFName && a.LastName == authorLName && a.DateOfBirth == DOB);

                            if (author is null)
                            {
                                Console.WriteLine("there is no author with this information");
                                break;
                            }

                            var booksByAuthor = dbcontext.Books.FirstOrDefault(b => b.Author == author);
                            if (booksByAuthor is null)
                            {
                                Console.WriteLine("No books found for the given author name.");
                                break;
                            }
                            Console.WriteLine($"Book ID: {booksByAuthor.Id}," +
                                              $" Title: {booksByAuthor.Title}," +
                                              $"Author ID: {booksByAuthor.AuthorId}" +
                                              $" Price: {booksByAuthor.Price}," +
                                              $" Available Copies: {booksByAuthor.AvailableCopies}");

                            break;
                        case 3:
                            string categoryTitle, description;
                            do
                            {
                                Console.WriteLine("Please Enter a valid category title");
                                categoryTitle = Console.ReadLine()!;
                                Console.WriteLine("Please Enter a valid category description");
                                description = Console.ReadLine()!;
                            } while (string.IsNullOrWhiteSpace(categoryTitle) || string.IsNullOrWhiteSpace(categoryTitle));
                            var category = dbcontext.Categories
                                .FirstOrDefault(c => c.Title == categoryTitle && c.Description == description);

                            if (category is null)
                            {
                                Console.WriteLine("there is no category with specified information");
                                break;
                            }
                            var booksByCategory = dbcontext.Books.FirstOrDefault(b => b.Category == category);
                            if (booksByCategory is null)
                            {
                                Console.WriteLine("No books found in the given category.");
                                break;
                            }
                            Console.WriteLine($"Book ID: {booksByCategory.Id}," +
                                              $" Title: {booksByCategory.Title}," +
                                              $"Author ID: {booksByCategory.AuthorId}, " +
                                              $"Category ID: {booksByCategory.CategoryId}, " +
                                              $" Price: {booksByCategory.Price}," +
                                              $" Available Copies: {booksByCategory.AvailableCopies}");

                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;

                    }

                    #endregion
                    break;
                default:
                    break;








            }
            #endregion


            #region MyRegion


            #region  Retrieve the book title, its category title , and the author’s full name for all books whose price is greater than 300.

            //var booksWithDetails = dbcontext.Books
            //    .Where(b => b.Price > 300)
            //    .Select(b => new
            //    {
            //        BookTitle = b.Title,
            //        CategoryTitle = b.Category.Title,
            //        AuthorFullName = (b.Author !=null ? b.Author.FirstName:" ") + " " + (b.Author != null ? b.Author.LastName : " ")
            //    })
            //    .ToList();

            //foreach (var book in booksWithDetails)
            //{
            //    Console.WriteLine($"Book Title: {book.BookTitle}, Category: {book.CategoryTitle}, Author: {book.AuthorFullName}");
            //}
            #endregion

            #region Retrieve All Authors And His/Her Books if Exists.

            //var AuthorWithBooks =dbcontext.Authors
            //    .Include(a => a.Books)
            //    .Select(a => new
            //    {
            //        AuthorFullName = (a.FirstName ?? " ") + " " + (a.LastName ?? " "),
            //        BookTitles = a.Books != null && a.Books.Any() 
            //                     ? a.Books.Select(b => b.Title).ToList() 
            //                     : new List<string> ()
            //    })
            //    .ToList();
            //foreach (var author in AuthorWithBooks)
            //{
            //    Console.WriteLine($"Author name: {author.AuthorFullName}");
            //    Console.WriteLine("Author books:");
            //    foreach (var title in author.BookTitles)
            //    {
            //        Console.WriteLine($"{title} ");
            //    }
            //    Console.WriteLine();
            //}
            #endregion

            #region Member with id x Want To Borrow The Book With Id y And He Will Return it After z Days 

            //int memberId, bookId, borrowingDays;
            //do
            //{
            //    Console.WriteLine("Please Enter a valid member id");

            //    flag = int.TryParse(Console.ReadLine(), out  memberId);

            //} while (!flag);
            //do
            //{
            //    Console.WriteLine("Please Enter a valid book id");
            //    flag = int.TryParse(Console.ReadLine(), out  bookId);
            //} while (!flag);

            //do
            //{
            //    Console.WriteLine("Please Enter a valid number of days to borrow the book");
            //    flag = int.TryParse(Console.ReadLine(), out  borrowingDays);
            //} while (!flag);

            //var isBorrowed = Services.BorrowBook(memberId, bookId, borrowingDays, dbcontext);

            //Console.WriteLine(isBorrowed ? "The book has been borrowed successfully." : "Failed to borrow the book.");

            #endregion

            #region After n Days Member with id x Returned The Book

            //bool flag;
            //int memberId, bookId, delayedDays;
            //do
            //{
            //    Console.WriteLine("Please Enter a valid member id");

            //    flag = int.TryParse(Console.ReadLine(), out memberId);

            //} while (!flag);
            //do
            //{
            //    Console.WriteLine("Please Enter a valid book id");
            //    flag = int.TryParse(Console.ReadLine(), out bookId);
            //} while (!flag);
            //do
            //{
            //    Console.WriteLine("Please Enter a valid number of delayed days in returning the book");
            //    flag = int.TryParse(Console.ReadLine(), out delayedDays);
            //} while (!flag);

            //var isReturned = Services.ReturnBook(memberId, bookId, delayedDays, dbcontext);
            //Console.WriteLine(isReturned ? "The book has been returned successfully." : "Failed to return the book.");


            #endregion

            #region Retrieve all members who currently have active loans (i.e., loans that have not yet been returned)

            //var activeLoanMembers = dbcontext.Members
            //    .Where(m => m.MemberLoans.Any(ml => ml.Loan != null && ml.Loan.LoanStatus == LoanStatus.Borrowed))
            //    .ToList();

            //Console.WriteLine("Members with active loans:");

            //foreach (var member in activeLoanMembers)

            //    Console.WriteLine($"Member ID: {member.Id}, Name: {member.Name}");


            #endregion

            #region search books by title, author, or category.


            //int choice;

            //bool flag;
            //do
            //{
            //    Console.WriteLine("Search Book By: 1- Title , 2- Author , 3- Category ");
            //    flag = int.TryParse(Console.ReadLine(), out choice);
            //} while (!flag || choice < 1 || choice > 3);


            //switch (choice)
            //{
            //    case 1:
            //        string title;
            //        do
            //        {
            //            Console.WriteLine("Please Enter a valid book title");
            //            title = Console.ReadLine()!;
            //        } while (string.IsNullOrWhiteSpace(title));
            //        var booksByTitle = dbcontext.Books
            //            .Where(b => b.Title == title)
            //            .ToList();
            //        if (booksByTitle.Any())
            //        {
            //            Console.WriteLine("Books found:");
            //            foreach (var book in booksByTitle)
            //            {
            //                Console.WriteLine($"Book ID: {book.Id}," +
            //                                  $" Title: {book.Title}," +
            //                                  $"Author ID: {book.AuthorId}" +
            //                                  $" Price: {book.Price}," +
            //                                  $" Available Copies: {book.AvailableCopies}");
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("No books found with the given title.");
            //        }
            //        break;
            //    case 2:
            //        string authorFName , authorLName;
            //        DateTime DOB;
            //        bool result;
            //        do
            //        {
            //            Console.WriteLine("Please Enter a valid author first name");
            //            authorFName = Console.ReadLine()!;
            //            Console.WriteLine("Please Enter a valid author last name");
            //            authorLName = Console.ReadLine()!;
            //            Console.WriteLine("Please Enter a valid author's DOB");
            //             result = DateTime.TryParse(Console.ReadLine(),out DOB);
            //        } while (string.IsNullOrWhiteSpace(authorFName) || string.IsNullOrWhiteSpace(authorLName) || !result);

            //        var author = dbcontext.Authors
            //            .FirstOrDefault(a => a.FirstName == authorFName && a.LastName == authorLName && a.DateOfBirth == DOB);

            //        if (author is null){
            //            Console.WriteLine("there is no author with this information");
            //            break;
            //        }

            //        var booksByAuthor = dbcontext.Books.FirstOrDefault(b=>b.Author==author);
            //        if (booksByAuthor is null)
            //        {
            //            Console.WriteLine("No books found for the given author name.");
            //            break;
            //        }
            //        Console.WriteLine($"Book ID: {booksByAuthor.Id}," +
            //                          $" Title: {booksByAuthor.Title}," +
            //                          $"Author ID: {booksByAuthor.AuthorId}" +
            //                          $" Price: {booksByAuthor.Price}," +
            //                          $" Available Copies: {booksByAuthor.AvailableCopies}");

            //        break;
            //    case 3:
            //        string categoryTitle , description;
            //        do
            //        {
            //            Console.WriteLine("Please Enter a valid category title");
            //            categoryTitle = Console.ReadLine()!;
            //            Console.WriteLine("Please Enter a valid category description");
            //            description = Console.ReadLine()!;
            //        } while (string.IsNullOrWhiteSpace(categoryTitle) || string.IsNullOrWhiteSpace(categoryTitle));
            //        var category = dbcontext.Categories
            //            .FirstOrDefault(c => c.Title == categoryTitle && c.Description == description);

            //        if (category is null)
            //        {
            //            Console.WriteLine("there is no category with specified information");
            //            break;
            //        }
            //        var booksByCategory = dbcontext.Books.FirstOrDefault(b => b.Category == category);
            //        if (booksByCategory is null)
            //        {
            //            Console.WriteLine("No books found in the given category.");
            //            break;
            //        }
            //        Console.WriteLine($"Book ID: {booksByCategory.Id}," +
            //                          $" Title: {booksByCategory.Title}," +
            //                          $"Author ID: {booksByCategory.AuthorId}, " +
            //                          $"Category ID: {booksByCategory.CategoryId}, " +
            //                          $" Price: {booksByCategory.Price}," +
            //                          $" Available Copies: {booksByCategory.AvailableCopies}");

            //        break;
            //    default:
            //        Console.WriteLine("Invalid choice.");
            //        break;

            //}
            #endregion
            #endregion

        }
    }
}
