using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibrary2.Domain.Entity
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public int GenreID { get; set; }

        [Required(ErrorMessage ="Неободимо добавить название книги")]
        public string BookName { get; set; }

        [Required(ErrorMessage ="Необходимо ввести год издания")]
        [Range(868,2018,ErrorMessage ="Книга не могла быть издана раньше 868 и поже 2018 года")]
        public int YearOfPublish { get; set; }

        public virtual Genre Genres { get; set; }

        //public List<Author> Authors { get; set; }
        public virtual ICollection<Author> Authors { get; set; }


        public Book()
        {
            Authors = new List<Author>();
        }
        
    }
}