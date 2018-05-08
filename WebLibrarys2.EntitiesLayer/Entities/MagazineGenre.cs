using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.EntitiesLayer.Entities
{
    public class MagazineGenre
    {
        [Key]
        public int MagazineGenreID { get; set; }
        [Required]
        public string MagazineGenreName { get; set; }

        public IEnumerable<Magazine> Magazines { get; set; }

        public MagazineGenre()
        {
            Magazines = new List<Magazine>();
        }
    }
}
