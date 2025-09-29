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

        public DateTime DateTime { get; set; }

        public LoanStatus LoanStatus { get; set; }
        #endregion
    }
}
