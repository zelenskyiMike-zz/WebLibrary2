using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class BookAuthorRepository : IBookAuthorsRepository
    {
        DbContext context;
        public BookAuthorRepository(DbContext dbContext)
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
