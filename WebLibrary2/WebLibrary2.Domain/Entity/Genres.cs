using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Entity
{
    public class Genres
    {

        ICollection<Books> Books { get; set; }
        public Genres()
        {
            Books = new List<Books>();
        }
    }
}
