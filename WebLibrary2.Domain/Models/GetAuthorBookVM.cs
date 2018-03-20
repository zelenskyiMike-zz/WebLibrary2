using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Models
{
    public class GetAuthorBookVM
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public int AuthorID { get; set; }
    }
}
