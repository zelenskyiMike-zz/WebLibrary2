using System.Collections.Generic;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Abstract.AbstractBook;

namespace WebLibrary2.Domain.Concrete.ConcreteBook
{
    public class EFGenreRepository : IGenreRepository
    {
        public EFDbContext context = new EFDbContext();

        public IEnumerable<BookGenre> Genres
        {
            get { return context.Genres; }
        }

        

    }
}
