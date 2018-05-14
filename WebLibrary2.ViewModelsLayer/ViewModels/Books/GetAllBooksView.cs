using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetAllBooksView
    {
        public int BookID { get; set; }

        [Required(ErrorMessage = "Неободимо добавить название книги")]
        public string BookName { get; set; }

        public int GenreID { get; set; }
        public string GenreName { get; set; }

        [Required(ErrorMessage = "Необходимо ввести год издания")]
        [Range(868, 2018, ErrorMessage = "Книга не могла быть издана раньше 868 и поже 2018 года")]
        public int YearOfPublish { get; set; }

        public IEnumerable<GetAuthorView> Authors { get; set; }
    }
}
