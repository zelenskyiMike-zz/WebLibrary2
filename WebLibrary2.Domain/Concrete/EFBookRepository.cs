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

        public void DeleteBook(int? bookID)
        {
            var book = GetBookByID(bookID);
            context.Books.Remove(book);
        }

        public Book GetBookByID(int? bookID)
        {
            return context.Books.Find(bookID);
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

        public GetM2MCRUDBookVM GetMultileInfo(int id)
        {         
            //var getM2MViewModel = from book in context.Books
            //           join authorBook in context.AuthorBooks on book.BookID equals authorBook.BookID
            //           join author in context.Authors on authorBook.AuthorID equals author.AuthorID
            //           select new GetM2MViewModel { AuthorName = author.AuthorName, BookName = book.BookName };

            //GetM2MViewModel result = new GetM2MViewModel();
            //foreach(var item in getM2MViewModel)
            //{
            //    result.AuthorName = item.AuthorName;
            //    result.BookName = item.BookName;
            //}

            return null;
        }

        public void InsertBook(AddABookViewModel bookVM)
        {
            Book book = new Book()
            {
                BookName = bookVM.BookName,
                GenreID = bookVM.GenreID,
                YearOfPublish = bookVM.YearOfPublish,
            };
            context.Books.Add(book);
        }


        public void SaveBook()
        {
            context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
        }


    }
}
