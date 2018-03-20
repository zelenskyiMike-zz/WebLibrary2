using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Models
{
    public class BookViewModel
    {
        public string BookName { get; set; }
        public int GenreID { get; set; }
        public int AuthorID { get; set; }//Author author
        public int YearOfPublish { get; set; }
    }
}
