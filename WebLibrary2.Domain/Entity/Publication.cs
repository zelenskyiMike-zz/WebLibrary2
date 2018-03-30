using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebLibrary2.Domain.Entity
{
    public class Publication
    {
        [Key]
        public int PublicationID { get; set; }
        public int GenreID { get; set; }
        [Required]
        public string NameOfPublication { get; set; }
        [Required]
        public DateTime DateOfPublication { get; set; }

        [XmlIgnore]
        [IgnoreDataMember]
        public Genre Genres { get; set; }

        [XmlIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Author> Authors { get; set; }
    }
}
