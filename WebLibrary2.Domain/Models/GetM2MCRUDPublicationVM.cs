using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Models
{
    public class GetM2MCRUDPublicationVM
    {
        public int PublicationID { get; set; }
        public int PublicationGenreID { get; set; }
        public string PublicationGenreName { get; set; }
        public string PublicationName { get; set; }
        public DateTime DateOfPublicationPublish { get; set; }

        public IEnumerable<Author> Authors { get; set; }
    }
}
