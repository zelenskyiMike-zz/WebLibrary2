using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Models
{
    public class GetM2MCRUDMagazineVM
    {
        public int MagazineID { get; set; }
        public int MagazineGenreID { get; set; }
        public string MagazineGenreName { get; set; }
        public string MagazineName { get; set; }
        public DateTime DateOfMagazinePublish { get; set; }

        public IEnumerable<Author> Authors { get; set; }
    }
}
