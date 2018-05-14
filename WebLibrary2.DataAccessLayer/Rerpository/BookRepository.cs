using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class BookRepository : GenericRepository<Book>
    {
        private DbContext context;
        public BookRepository(DbContext contextParam): base(contextParam)
        {
            context = contextParam;
        }

        public List<Author> GetAuthorsNotExistInBook(Book book)
        {
            var initBookAuthorList = context.BookAuthors.Where(x => x.BookID == book.BookID).Select(x => x.Authors).ToList();

            List<Author> finalListOfAuthors = new List<Author>();

            foreach (var item in context.Authors.ToList())
            {
                if (!initBookAuthorList.Contains(item))
                {
                    finalListOfAuthors.Add(item);
                }
            }
            return finalListOfAuthors;
        }

        public override void Create(Book bookVM)
        {
            Book book = new Book()
            {
                BookName = bookVM.BookName,
                GenreID = bookVM.GenreID,
                YearOfPublish = bookVM.YearOfPublish
            };
            context.Books.Add(book);
            context.SaveChanges();

            foreach (var item in bookVM.AuthorsIDs)
            {
                BookAuthor bookAuthor = new BookAuthor()
                {
                    BookID = book.BookID,
                    AuthorID = item
                };
                context.BookAuthors.Add(bookAuthor);
                context.SaveChanges();
            }
        }

        public void UpdateBook(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
        }

        public Book GetBooksDetails(Book book)
        {
            BookGenre genre = context.Genres.Where(x => x.GenreID == book.GenreID).SingleOrDefault();

            var authorList = context.BookAuthors.Include(x => x.Authors).Where(x => x.BookID == book.BookID).Select(x => x.Authors).ToList();

            Book bookVM = new Book()
            {
                BookID = book.BookID,
                BookName = book.BookName,
                YearOfPublish = book.YearOfPublish,
                Genres = genre,
                Authors = authorList
            };
            return bookVM;
        }

        public IEnumerable<Book> GetAllBooksWithGenres()
        {
            return context.Books.Include(g => g.Genres);
        }
    }
}
