using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Models
{
    public class AuthorViewModel
    {
        [Required(ErrorMessage = "Неободимо добавить имя автора")]
        [StringLength(50, MinimumLength = 5 , ErrorMessage = "Длинна строки должна быть не менее 5 и не более 50 символов")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Выберите хотя бы одну книгу")]
        public List<int> BooksIDs { get; set; }
    }
}
