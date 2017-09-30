using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibrary2.Domain.Entity
{
    
    public class Books
    {   [Key]
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public int GenreID { get; set; }
        public string BookName { get; set; }
        public int YearOfPublish { get; set; }

    }
}