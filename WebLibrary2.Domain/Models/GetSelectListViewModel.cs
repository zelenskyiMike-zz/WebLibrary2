using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Models
{
    public class GetSelectListViewModel
    {
        public int BookID { get; set; }
        public int GenreID { get; set; }

        [Required(ErrorMessage = "Неободимо добавить название книги")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Необходимо ввести год издания")]
        [Range(868, 2018, ErrorMessage = "Книга не могла быть издана раньше 868 и поже 2018 года")]
        public int YearOfPublish { get; set; }

        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }

    }
}
