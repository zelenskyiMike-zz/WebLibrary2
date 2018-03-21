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

        public void InsertAuthorBook(GetAuthorBookVM getAuthorBookVM)
        {
            AuthorBook authorBook = new AuthorBook()
            {
                BookID = getAuthorBookVM.BookID,
                AuthorID = getAuthorBookVM.AuthorID // Int32.Parse(authorBookVM.AuthorID.ToString())
            };
            context.AuthorBooks.Add(authorBook);
            context.SaveChanges();
        }
    }
}
