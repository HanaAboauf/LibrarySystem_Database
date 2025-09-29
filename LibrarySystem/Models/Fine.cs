using LibrarySystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    internal class Fine: BaseEntity
    {

        #region Attributes

        public decimal Amount { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime PaidDate { get; set; }

        public FineStatus FineStatus { get; set; }

        #endregion
    }
}
