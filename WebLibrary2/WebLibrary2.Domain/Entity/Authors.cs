using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibrary2.Domain.Entity
{
    public class Authors
    {
        [Key]
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        
        ICollection<Books> Books { get; set; }
        public Authors()
        {
            Books = new List<Books>();
        }
        
    }
}
