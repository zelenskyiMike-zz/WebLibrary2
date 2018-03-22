using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;
using System.Web;
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

        public void InsertBook(Book book)
        {
            //Book book = new Book()
            //{
            //    BookName = bookVM.BookName,
            //    GenreID = bookVM.GenreID,
            //    YearOfPublish = bookVM.YearOfPublish
            //};

            //foreach (var item in Authors)
            //{
            //    //var author = context.Authors.Find(item);
            //    book.Authors.Add(item);
            //}

            context.Books.Add(book);
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

        public GetBookGenreCRUDBookVM GetBooksWithGenres(int? id)
        {
            Book book = GetBookByID(id);
            var genreName = (from g in context.Genres
                             where g.GenreID == book.GenreID
                             select g.GenreName).SingleOrDefault();

            GetBookGenreCRUDBookVM bookVM = new GetBookGenreCRUDBookVM()
            {
                BookID = book.BookID,
                BookName = book.BookName,
                YearOfPublish = book.YearOfPublish,
                GenreName = genreName
            };
            return bookVM;
        }

        public GetM2MCRUDBookVM GetBooksDetails(int? id)
        {
            Book book = context.Books.Find(id);
            var authorList = context.AuthorBooks.Include(x => x.Authors).Where(x => x.BookID == id).Select(x => x.Authors).ToList();

            GetM2MCRUDBookVM bookVM = new GetM2MCRUDBookVM()
            {
                BookID = book.BookID,
                BookName = book.BookName,
                Authors = authorList
            };
            return bookVM;
        }

        public Book GetLastBook()
        {
            Book book = context.Books.ToList().Last();
            return book;
        }
    }
}
