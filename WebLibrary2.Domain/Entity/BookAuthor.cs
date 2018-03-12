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
        
        [Key,Column(Order = 0)]
        [ForeignKey("BookID")]
        public int BookID { get; set; }
        [Key,Column(Order = 1)]
        [ForeignKey("AuthorID")]
        public int AuthorID { get; set; }
    }
}
