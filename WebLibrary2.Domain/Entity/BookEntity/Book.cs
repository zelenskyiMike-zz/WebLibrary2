using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WebLibrary2.Domain.Entity.BookEntity
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public int GenreID { get; set; }
 
        [Required(ErrorMessage ="Неободимо добавить название книги")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Необходимо ввести год издания")]
        [Range(868, 2018, ErrorMessage = "Книга не могла быть издана раньше 868 и поже 2018 года")] //not necessary
        public int YearOfPublish { get; set; }

        [XmlIgnore]
        [IgnoreDataMember]
        public BookGenre Genres { get; set; }

        [XmlIgnore]
        [IgnoreDataMember]
        public virtual IEnumerable<Author> Authors { get; set; }

        [XmlIgnore]
        [IgnoreDataMember]
        public List<int> AuthorsIDs { get; set; }

        public Book()
        {
            Authors = new List<Author>();
        }
    }
}