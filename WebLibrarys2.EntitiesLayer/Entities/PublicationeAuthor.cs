using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.EntitiesLayer.Entities
{
    public class PublicationeAuthor
    {
        [Key, Column(Order = 1)]
        public int PublicationID { get; set; }
        [Key, Column(Order = 2)]
        public int AuthorID { get; set; }

        public Publication Publications { get; set; }
        public Author Authors { get; set; }
    }
}
