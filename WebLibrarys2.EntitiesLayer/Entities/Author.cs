using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace WebLibrary2.EntitiesLayer.Entities
{
    [Serializable]
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Необходимо ввести имя и фамилию автора")]
        public string AuthorName { get; set; }

        [XmlIgnore]
        //[IgnoreDataMember]
        public virtual IEnumerable<Book> Books { get; set; }

        [XmlIgnore]
        //[IgnoreDataMember]
        public virtual IEnumerable<Publication> Publications { get; set; }

        [XmlIgnore]
        //[IgnoreDataMember]
        public virtual IEnumerable<Magazine> Magazines { get; set; }

        [XmlIgnore]
        //[IgnoreDataMember]
        public virtual IEnumerable<Article> Articles { get; set; }

        [XmlIgnore]
        //[IgnoreDataMember]
        public IEnumerable<BookAuthor> BookAuthors { get; set; }

        [XmlIgnore]
        //[IgnoreDataMember]
        public List<int> BooksIDs { get; set; }

        public Author()
        {
            Books = new List<Book>();
            Publications = new List<Publication>();
            Magazines = new List<Magazine>();
            Articles = new List<Article>();
        }
    }
}
