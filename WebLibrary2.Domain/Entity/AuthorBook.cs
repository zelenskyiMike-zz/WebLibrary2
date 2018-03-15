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
                
        public int BookID { get; set; }
        public int AuthorID { get; set; }

        public Book Books { get; set; }
        public Author Authors { get; set; }
    }
}
