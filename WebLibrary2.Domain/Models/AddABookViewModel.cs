using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Models
{
    public class AddABookViewModel : IEnumerable
    {
        [Key]
        public int BookID { get; set; }
        public string BookName { get; set; }
        public int GenreID { get; set; }
        public string GenreName { get; set; }
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public int YearOfPublish { get; set; }

        public Book Books { get; set; }
        public Author Authors { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}