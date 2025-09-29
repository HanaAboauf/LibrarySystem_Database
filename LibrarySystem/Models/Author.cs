using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    internal class Author:BaseEntity
    {
        #region Attributes
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        #endregion

        #region Relationships

        #region Book-Author relationship

        public ICollection<Book>? Books { get; set; } = new HashSet<Book>();

        #endregion

        #endregion
    }
}
