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

        public DateTime DateIssued { get; set; }

        public DateTime PaidDate { get; set; }

        #endregion
    }
}
