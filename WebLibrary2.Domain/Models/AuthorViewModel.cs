using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Models
{
    public class AuthorViewModel
    {
        public string AuthorName { get; set; }
        public List<int> BooksIDs { get; set; }
    }
}
