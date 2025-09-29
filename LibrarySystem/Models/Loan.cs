using LibrarySystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    internal class Loan: BaseEntity
    {
        #region Attributes

        public DateTime LoanDate { get; set; }

        public LoanStatus LoanStatus { get; set; }
        #endregion


        #region Relationships

        #region Loan-Fine relationship

        public Fine Fine { get; set; }= null!;

        #endregion

        #region MemberLoan-Loan relationship
        public ICollection<MemberLoan> MemberLoans { get; set; } = new HashSet<MemberLoan>();
        #endregion

        #endregion
    }
}
