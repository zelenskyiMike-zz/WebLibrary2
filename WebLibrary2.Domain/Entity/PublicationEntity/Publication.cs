using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebLibrary2.Domain.Entity.PublicationEntity
{
    public class Publication
    {
        [Key]
        public int PublicationID { get; set; }
        public int PublicationGenreID { get; set; }
        [Required]
        public string PublicationName { get; set; }
        [Required]
        public DateTime DateOfPublicationPublish { get; set; }

        [XmlIgnore]
        [IgnoreDataMember]
        public PublicationGenre PublicationGenres { get; set; }

        [XmlIgnore]
        [IgnoreDataMember]
        public virtual IEnumerable<Author> Authors { get; set; }
        public Publication()
        {
            Authors = new List<Author>();
        }
    }
}
