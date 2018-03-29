using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Concrete
{
    public class EFBookAuthorRepository : IBookAuthorsRepository
    {
        EFDbContext context;
        public EFBookAuthorRepository(EFDbContext dbContext)
        {
            context = dbContext;
        }
        public void DeleteBookFromAuthor(int authorID, int[] bookIDsForDelete)
        {
            if (bookIDsForDelete != null)
            {
                foreach (var bookID in bookIDsForDelete)
                {
                    var bookToRemove = context.BookAuthors.Find(bookID, authorID);
                    context.BookAuthors.Remove(bookToRemove);
                    context.SaveChanges();
                }
            }
        }
        public void DeleteAuthorFromBook(int bookID, int[] authorIDsForDelete)
        {
            if (authorIDsForDelete != null)
            {
                foreach (var authorID in authorIDsForDelete)
                {
                    var bookToRemove = context.BookAuthors.Find(bookID, authorID);
                    context.BookAuthors.Remove(bookToRemove);
                    context.SaveChanges();
                }
            }
        }

        public void AddAuthorToBook(int bookID, int[] authorIDsForInsert)
        {
            if (authorIDsForInsert != null)
            {
                foreach (var authorID in authorIDsForInsert)
                {
                    BookAuthor bookAuthor = new BookAuthor()
                    {
                        BookID = bookID,
                        AuthorID = authorID
                    };
                    context.BookAuthors.Add(bookAuthor);
                    context.SaveChanges();
                }
            }
        }

        public void AddBookToAuthor(int authorID, int[] bookIDsForInsert)
        {
            if (bookIDsForInsert != null)
            {
                foreach (var bookID in bookIDsForInsert)
                {
                    BookAuthor bookAuthor = new BookAuthor()
                    {
                        BookID = bookID,
                        AuthorID = authorID
                    };
                    context.BookAuthors.Add(bookAuthor);
                    context.SaveChanges();
                }
            }
        }
    }
}
