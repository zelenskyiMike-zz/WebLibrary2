using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity.BookEntity;

namespace WebLibrary2.Domain.Concrete
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
