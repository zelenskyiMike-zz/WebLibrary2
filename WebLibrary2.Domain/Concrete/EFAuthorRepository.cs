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

        public GetM2MCRUDAuthorVM GetBookDetails(int? id)
        {
            Author author = context.Authors.Find(id);
           // BookAuthor aBook = context.BookAuthors.Find(author.AuthorID);
          
            var bookList = context.BookAuthors.Include(x => x.Books).Where(x => x.AuthorID == id).Select(x => x.Books).ToList();
          
            GetM2MCRUDAuthorVM authorVM = new GetM2MCRUDAuthorVM()
            {
                AuthorID = author.AuthorID,
                AuthorName = author.AuthorName,
                Books = bookList
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
