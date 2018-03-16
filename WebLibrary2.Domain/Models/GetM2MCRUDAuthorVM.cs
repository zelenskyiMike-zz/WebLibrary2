using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Models
{
    public class GetM2MCRUDAuthorVM 
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}