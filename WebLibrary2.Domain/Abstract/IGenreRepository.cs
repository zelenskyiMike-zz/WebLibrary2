using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.BookEntity;

namespace WebLibrary2.Domain.Abstract
{
    public interface IGenreRepository
    {
        IEnumerable<BookGenre> Genres { get; }
    }
}
