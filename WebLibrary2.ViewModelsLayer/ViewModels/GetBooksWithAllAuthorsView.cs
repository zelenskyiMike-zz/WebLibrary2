using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetBooksWithAllAuthorsView
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
        public IEnumerable<GetAuthorView> Authors { get; set; }
        public List<GetBookGenreView> Genres { get; set; }

    }
}
