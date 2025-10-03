using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    internal class MemberLoan
    {

        #region Attributes

        public DateTime DueDate{ get; set; }

        public DateTime? ReturnDate { get; set; }
        #endregion
        #region Relationship attributes
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public int LoanId { get; set; }
        public Loan? Loan { get; set; }

        public int BookId { get; set; }

        public Book? Book { get; set; } 
        #endregion
    }
}
