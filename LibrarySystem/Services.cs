using LibrarySystem.Contexts;
using LibrarySystem.Models;
using LibrarySystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal static class Services
    {
        public static bool BorrowBook(int memberId, int bookId, int borrowingDays, LibrarySystemDbContext dbContext)
        {
            try
            {
                var member=dbContext.Members.Find(memberId);

                if (member == null || member.MemberStatus==MemberStatus.Suspended) return false;

                var book = dbContext.Books.Find(bookId);

                if (book is null || book.AvailableCopies <= 0) return false;

                Loan loan = new Loan() {
                    LoanDate = DateTime.Now,
                };

                dbContext.Loans.Add(loan);

                MemberLoan memberLoan = new MemberLoan()
                {
                    MemberId = memberId,
                    BookId = bookId,
                    Loan=loan,
                    DueDate = DateTime.Now.AddDays(borrowingDays)
                };
                dbContext.MemberLoans.Add(memberLoan);

                book.AvailableCopies -= 1;

               return dbContext.SaveChanges() > 0;

              


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing the book: {ex.InnerException?.Message ?? ex.Message}");
                return false;
            }

            }

        public static bool ReturnBook(int memberId, int bookId, int delayedDays, LibrarySystemDbContext dbContext)
        {

            try
            {
                var MemberLoan =dbContext.MemberLoans.FirstOrDefault(m=>m.MemberId==memberId);

                if(MemberLoan is null || MemberLoan.BookId !=bookId || MemberLoan.ReturnDate is not null) return false;

                var book = dbContext.Books.Find(bookId);

                if (book is null) return false;

                book.AvailableCopies += 1;

                decimal fineAmount = delayedDays * (0.1M*book.Price);

                var member = dbContext.Members.Find(memberId);

                if (member is null) return false;

                MemberLoan.ReturnDate = DateTime.Now.AddDays(delayedDays);

                var loan = dbContext.Loans.Find(MemberLoan.LoanId);

                if (loan is null) return false;


                // returndate is on time
                if (MemberLoan.ReturnDate==MemberLoan.DueDate) loan.LoanStatus = LoanStatus.Returned;

                //returndate is over time
                else
                {
                    Fine fine = new Fine()
                    {
                        Amount = fineAmount,
                        IssueDate = DateTime.Now,
                        PaidDate = DateTime.Now,
                        Loan=loan,
                    };
                    dbContext.Fines.Add(fine);
                    loan.LoanStatus = LoanStatus.Overdue;
                    member.MemberStatus = MemberStatus.Suspended;


                }
                return dbContext.SaveChanges() > 0;




            }
            catch (Exception ex) {
                Console.WriteLine($"An error occurred while returning the book: {ex.InnerException?.Message ?? ex.Message}");
                return false;
            }

        }

        public static bool PayFine(int fineId, int memberId, LibrarySystemDbContext dbContext)
        {
            try
            {
                // 1. Find fine
                var fine = dbContext.Fines.Find(fineId);

                if (fine == null || fine.FineStatus == FineStatus.Paid) return false;


                // 2. Mark as paid
                fine.FineStatus = FineStatus.Paid;
                fine.PaidDate = DateTime.Now;

                // 3. Reactivate member (optional rule)
                var member = dbContext.Members.Find(memberId);

                if (member is null || member.MemberStatus == MemberStatus.Active) return false;

                member.MemberStatus = MemberStatus.Active;

                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error paying fine: {ex.InnerException?.Message ?? ex.Message}");
                return false;
            }
        }

    }
}
