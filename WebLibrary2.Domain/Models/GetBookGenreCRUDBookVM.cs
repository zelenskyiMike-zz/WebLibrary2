using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Models
{
    public class GetBookGenreCRUDBookVM
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string GenreName { get; set; }
        public int YearOfPublish { get; set; }
    }
}
