using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WebLibrary2.Domain.Entity
{
    public class Genre 
    {
        [Key]
        public int GenreID { get; set; }
        public string GenreName { get; set; }

        public ICollection<Book> Books { get; set; }

        public Genre()
        {
            Books = new List<Book>();
        }

    }
}
