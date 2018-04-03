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
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Publication> Publications { get; set; }
        public IEnumerable<Magazine> Magazines { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}
