using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Models
{
    public class ArticleViewModel
    {
        [Required(ErrorMessage = "Неободимо добавить название статьи")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длинна строки должна быть не менее 5 и не более 50 символов")]
        public string ArticleName { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать жанр")]
        public int ArticleGenreID { get; set; }

        [Required(ErrorMessage = "Выберите хотя бы одного автора")]
        public List<int> AuthorsIDs { get; set; }

        [Required(ErrorMessage = "Выберите дату")]
        public DateTime DateOfArticlePublish { get; set; }
    }
}
