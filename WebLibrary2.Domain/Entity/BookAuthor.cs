using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Entity
{
    public class BookAuthor
    {
        [Key,Column(Order = 1)]     
        public int BookID { get; set; }
        [Key, Column(Order = 2)]
        public int AuthorID { get; set; }

        public Book Books { get; set; }
        public Author Authors { get; set; }
    }
}
