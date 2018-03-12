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

        public int GetAddedID()
        {
            throw new NotImplementedException();
        }

        public Book GetBookByID(int? bookID)
        {
            return context.Books.Find(bookID);
        }

        public void InsertBook(AddABookViewModel bookVM)
        {
            Book book = new Book()
            {
                BookID = bookVM.BookID,
                BookName = bookVM.BookName,
                GenreID = bookVM.GenreID,
                YearOfPublish = bookVM.YearOfPublish,
                AuthorID = bookVM.AuthorID
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
