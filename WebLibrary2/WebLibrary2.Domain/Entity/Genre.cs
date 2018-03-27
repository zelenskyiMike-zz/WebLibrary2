using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Entity
{
    public class Genre
    {

        ICollection<Book> Books { get; set; }
        public Genre()
        {
            Books = new List<Book>();
        }
    }
}
