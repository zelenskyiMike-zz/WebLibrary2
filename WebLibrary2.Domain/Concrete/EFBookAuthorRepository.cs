using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract;

namespace WebLibrary2.Domain.Concrete
{
    public class EFBookAuthorRepository : IBookAuthorsRepository
    {
        EFDbContext context;
        public EFBookAuthorRepository(EFDbContext dbContext)
        {
            context = dbContext;
        }
        public void DeleteBookFromAuthor(int authorID, int[] bookIDs)
        {
            if (bookIDs != null)
            {
                foreach (var bookID in bookIDs)
                {
                    var bookToRemove = context.BookAuthors.Find(bookID, authorID);
                    context.BookAuthors.Remove(bookToRemove);
                    context.SaveChanges();
                }
            }
        }
        public void DeleteAuthorFromBook(int bookID, int[] authorIDs)
        {
            if (authorIDs != null)
            {
                foreach (var authorID in authorIDs)
                {
                    var bookToRemove = context.BookAuthors.Find(bookID, authorID);
                    context.BookAuthors.Remove(bookToRemove);
                    context.SaveChanges();
                }
            }
        }

    }
}
