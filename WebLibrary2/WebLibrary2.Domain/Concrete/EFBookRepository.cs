using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Books> Books
        {
            get { return context.Books; }
        }

        public IQueryable<Books> TolstoiBooks
        {
            get
            {
                return context.Books;
            }

        }
    }
}
