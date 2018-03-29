using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;


namespace WebLibrary2.Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Book> Books
        {
            get { return context.Books; }
        }

        public Book GetBookByID(int? bookID)
        {
            return context.Books.Find(bookID);
        }

        public List<Author> GetAuthorsNotExistInBook(int bookID)
        {
            var currBook = GetBookByID(bookID);
            BookViewModel bookVM = new BookViewModel()
            {
                BookName = currBook.BookName,
                GenreID = currBook.GenreID,
                YearOfPublish = currBook.YearOfPublish,
                AuthorsIDs = currBook.AuthorsIDs
            };

            return context.Authors.Where(a => !currBook.Authors.Select(aID => aID.AuthorID).Contains(a.AuthorID)).ToList(); //Passes all authors? but mustn't
        }

        public void InsertBook(BookViewModel bookVM)
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

        public void DeleteBook(int? bookID)
        {
            var book = GetBookByID(bookID);
            context.Books.Remove(book);
        }

        public void SaveBook()
        {
            context.SaveChanges();
        }

        public GetM2MCRUDBookVM GetBooksDetails(int? id)
        {
            Book book = GetBookByID(id);
            var genreName = (from g in context.Genres
                             where g.GenreID == book.GenreID
                             select g.GenreName).SingleOrDefault();

            var authorList = context.BookAuthors.Include(x => x.Authors).Where(x => x.BookID == id).Select(x => x.Authors).ToList();

            GetM2MCRUDBookVM bookVM = new GetM2MCRUDBookVM()
            {
                BookID = book.BookID,
                BookName = book.BookName,
                GenreID = book.GenreID,
                YearOfPublish = book.YearOfPublish,
                GenreName = genreName,
                Authors = authorList
            };
            return bookVM;
        }
    }
}
