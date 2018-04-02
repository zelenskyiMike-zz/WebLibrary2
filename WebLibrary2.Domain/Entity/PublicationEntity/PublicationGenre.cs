using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Entity.PublicationEntity
{
    public class PublicationGenre
    {
        [Key]
        public int PublicationGenreID { get; set; }
        [Required]
        public string PublicationGenreName { get; set; }

        public virtual IEnumerable<Publication> Publicationes { get; set; }

        public PublicationGenre()
        {
            Publicationes = new List<Publication>();
        }
    }
}
