using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace WebLibrary2.Domain.Entity
{
    [Serializable]
    public class Author : IEnumerable
    {
        [Key]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Необходимо ввести имя и фамилию автора")]
        public string AuthorName { get; set; }

        //public virtual ICollection<Book> Books { get; set; }
        //public virtual IList<Book> Books { get; set; }
        [XmlIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
