using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetAuthorLiteratureView
    {
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Неободимо добавить название книги")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длинна строки должна быть не менее 5 и не более 50 символов")]
        public string AuthorName { get; set; }
        public IEnumerable<GetBookView> Books { get; set; }
        public IEnumerable<GetArticleView> Articles { get; set; }
        public IEnumerable<GetPublicationView> Publications { get; set; }
        public IEnumerable<GetMagazineView> Magazines { get; set; }
    }
}
