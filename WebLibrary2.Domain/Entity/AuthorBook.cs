using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Entity
{
    public class AuthorBook
    {
        [Key]
        public int ID { get; set; }
        [Key,ForeignKey("BookID")]
        public Book BookID { get; set; }

        [Key,ForeignKey("AuthorID")]
        public Author AuthorID { get; set; }

        public virtual IEnumerable<Book> Books { get; set; }
        public virtual IEnumerable<Author> Authors { get; set; }
    }
}
