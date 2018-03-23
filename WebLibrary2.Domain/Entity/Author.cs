using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibrary2.Domain.Entity
{
    [Serializable]
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Необходимо ввести имя и фамилию автора")]
        public string AuthorName { get; set; }

        //public virtual ICollection<Book> Books { get; set; }
        //public virtual IList<Book> Books { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }
       
    }
}
