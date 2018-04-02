using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Entity.BookEntity
{
    public class BookGenre 
    {
        [Key]
        public int GenreID { get; set; }
        public string GenreName { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public BookGenre()
        {
            Books = new List<Book>();
        }

    }
}
