using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    internal class Book : BaseEntity
    {

        #region Attributes
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public int PublicationYear { get; set; }

        public int AvailableCopies { get; set; }

        public int TotalCopies { get; set; }
        #endregion

        #region Relationships

        #region Book-Author relationship

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        #endregion

        #region Book-Category relationship
        
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
        #endregion

        #region MemberLoan-Book relationship
        public ICollection<MemberLoan> MemberLoans { get; set; } = new HashSet<MemberLoan>();

        #endregion

        #endregion
    }
}
