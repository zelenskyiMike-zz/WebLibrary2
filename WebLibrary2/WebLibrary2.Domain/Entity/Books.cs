using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLibrary2.Domain.Entity
{
    public class Book
    {
        public int idBook { get; set; }
        public int idAuthor { get; set; }
        public int idGenre { get; set; }
        public string NameOfBook { get; set; }
        public int YearOfPublish { get; set; }
    }
}