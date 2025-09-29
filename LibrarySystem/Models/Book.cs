using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    internal class Book: BaseEntity
    {

        #region Attributes
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public int PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; } 
        #endregion
    }
}
