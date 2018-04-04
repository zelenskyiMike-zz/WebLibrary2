using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.ArticleEntity;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Entity.PublicationEntity;

namespace WebLibrary2.Domain.Models
{
    public class GetLiteratureViewModel
    {
        public IEnumerable<Article> Articles;
        public IEnumerable<Book> Books;
        public IEnumerable<Publication> Publications;
        public IEnumerable<Magazine> Magazines;
    }
}
