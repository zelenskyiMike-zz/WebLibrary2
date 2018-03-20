using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Concrete
{
    public class EFAuthorRepository : IAuthorsRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Author> Authors
        {
            get { return context.Authors; }
        }

        public void CreateAuthor(Author author)
        {
            context.Authors.Add(author);
            Save();
        }

        public void DeleteAuthor(Author author)
        {
            context.Authors.Remove(author);
            Save();
        }

        public Author GetAuthorByID(int? id)
        {
            return context.Authors.Find(id);
        }

        public GetM2MCRUDAuthorVM GetBookDetails(int? id)
        {
            Author author = context.Authors.Find(id);
            AuthorBook aBook = context.AuthorBooks.Find(author.AuthorID);

            ///////////////////////////////////////////Make through LINQ////////////////////////////////////////////////////////////////
            IEnumerable<Book> book = context.Books.SqlQuery("Select * from Books where BookID in (Select AuthorBooks.BookID from AuthorBooks where AuthorBooks.AuthorID = " + id + ")").ToList();

            GetM2MCRUDAuthorVM authorVM = new GetM2MCRUDAuthorVM()
            {
                AuthorID = author.AuthorID,
                AuthorName = author.AuthorName,
                Books = book
            };
            return authorVM;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        //public void makeJson(List<Author> author)
        //{
        //    using (StreamWriter file = File.CreateText(@"C:\Users\Anuitex-53\Documents\Visual Studio 2017\WebLibrary2-master\WebLibrary2\authors.json"))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        serializer.Serialize(file, author);
        //    }
        //}
    }
}
