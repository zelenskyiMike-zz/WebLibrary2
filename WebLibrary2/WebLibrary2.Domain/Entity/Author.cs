using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibrary2.Domain.Entity
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        
        ICollection<Book> Books { get; set; }
        public Author()
        {
            Books = new List<Book>();
        }
        
    }
}
