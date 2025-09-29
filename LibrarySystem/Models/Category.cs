using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    internal class Category: BaseEntity
    {

        #region Attributes

        public string Title { get; set; } = null!;

        public string Description { get; set; }
        #endregion


        #region Relationships

        #region Book-Category relationship

        public ICollection<Book>? Books { get; set; } = new HashSet<Book>();

        #endregion

        #endregion
    }
}
