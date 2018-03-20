using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Concrete
{
    public class EFAuthorBooksRepository : IAuthorBooksRepository
    {
        private EFDbContext context = new EFDbContext();

        public void InsertAuthorBook(BookViewModel authorBookVM)
        {
            AuthorBook authorBook = new AuthorBook()
            {
                BookID = authorBookVM.BookID,
                AuthorID = authorBookVM.AuthorID
            };
            context.AuthorBooks.Add(authorBook);
            context.SaveChanges();
        }
    }
}
