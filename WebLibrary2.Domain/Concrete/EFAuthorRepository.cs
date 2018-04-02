using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;
using System.Web;
using WebLibrary2.Domain.Models;
using WebLibrary2.Domain.Entity.BookEntity;

namespace WebLibrary2.Domain.Concrete
{
    public class EFAuthorRepository : IAuthorsRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Author> Authors
        {
            get { return context.Authors; }
        }

        public void CreateAuthor(AuthorViewModel authorVM)
        {
            Author author = new Author()
            {
                AuthorName = authorVM.AuthorName
            };
            context.Authors.Add(author);
            context.SaveChanges();

            foreach (var item in authorVM.BooksIDs)
            {
                BookAuthor bookAuthor = new BookAuthor()
                {
                    BookID = item,
                    AuthorID = author.AuthorID
                };
                context.BookAuthors.Add(bookAuthor);
                context.SaveChanges();
            }
        }

        public void DeleteAuthor(int? id)
        {
            Author author = GetAuthorByID(id);
            context.Authors.Remove(author);
            Save();
        }

        public Author GetAuthorByID(int? id)
        {
            return context.Authors.Find(id);
        }

        public GetM2MCRUDAuthorVM GetAuthorDetails(int? id)
        {
            Author author = GetAuthorByID(id);
          
            var bookList = context.BookAuthors.Include(x => x.Books).Where(x => x.AuthorID == id).Select(x => x.Books).ToList();
          
            GetM2MCRUDAuthorVM authorVM = new GetM2MCRUDAuthorVM()
            {
                AuthorID = author.AuthorID,
                AuthorName = author.AuthorName,
                Books = bookList
            };
            return authorVM;
        }

        public List<Book> GetBooksNotExistInAuthor(int authorID)
        {
            var currBook = GetAuthorByID(authorID);

            var initBookAuthorList = context.BookAuthors.Where(x => x.AuthorID == currBook.AuthorID).Select(x => x.Books).ToList();

            List<Book> finalListOfBooks = new List<Book>();

            foreach (var item in context.Books.ToList())
            {
                if (!initBookAuthorList.Contains(item))
                {
                    finalListOfBooks.Add(item);
                }
            }
            return finalListOfBooks;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
