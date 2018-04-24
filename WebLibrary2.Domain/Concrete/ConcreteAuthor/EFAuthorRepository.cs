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
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Entity.ArticleEntity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Abstract.AbstractArticle;

namespace WebLibrary2.Domain.Concrete.ConcreteAuthor
{
    public class EFAuthorRepository : IAuthorsRepository
    {
        private EFDbContext context;
        public EFAuthorRepository(EFDbContext contextParam)
        {
            context = contextParam;
        }


        public IEnumerable<Author> Authors
        {
            get { return context.Authors; }
        }

        public void CreateAuthor(AuthorView authorVM)
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

        public IQueryable<Author> GetAllAuthors()
        {
            return context.Authors;
        }

        public Author GetAuthorByID(int? id)
        {
            return context.Authors.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }



        public List<Book> GetBooksNotExistInAuthor(int authorID)
        {
            var currAuthor = GetAuthorByID(authorID);
            List<Book> finalListOfBooks = new List<Book>();

            var initBookAuthorList = context.BookAuthors.Where(x => x.AuthorID == currAuthor.AuthorID).Select(x => x.Books).ToList();

            foreach (var item in context.Books.ToList())
            {
                if (!initBookAuthorList.Contains(item))
                {
                    finalListOfBooks.Add(item);
                }
            }
            return finalListOfBooks;
        }

        public List<Article> GetArticlesNotExistInAuthor(int authorID)
        {
            var currAuthor = GetAuthorByID(authorID);

            List<Article> finalListOfArticles = new List<Article>();

            var initArticleAuthorList = context.ArticleAuthors.Where(x => x.AuthorID == currAuthor.AuthorID).Select(x => x.Articles).ToList();

            foreach (var item in context.Articles.ToList())
            {
                if (!initArticleAuthorList.Contains(item))
                {
                    finalListOfArticles.Add(item);
                }
            }
            return finalListOfArticles;
        }

        public List<Magazine> GetMagazinesNotExistInAuthor(int authorID)
        {
            var currAuthor = GetAuthorByID(authorID);
            List<Magazine> finalListOfMagazines = new List<Magazine>();

            var initMagazineAuthorsList = context.MagazineAuthors.Where(x => x.AuthorID == currAuthor.AuthorID).Select(x => x.Magazines).ToList();

            foreach (var item in context.Magazines.ToList())
            {
                if (!initMagazineAuthorsList.Contains(item))
                {
                    finalListOfMagazines.Add(item);
                }
            }
            return finalListOfMagazines;
        }

        public List<Publication> GetPublicationsNotExistInAuthor(int authorID)
        {
            var currAuthor = GetAuthorByID(authorID);
            List<Publication> finalListOfPublication = new List<Publication>();

            var initPublicationsAuthorList = context.PublicationeAuthors.Where(x => x.AuthorID == currAuthor.AuthorID).Select(x => x.Publications).ToList();

            foreach (var item in context.Publications.ToList())
            {
                if (!initPublicationsAuthorList.Contains(item))
                {
                    finalListOfPublication.Add(item);
                }
            }
            return finalListOfPublication;
        }



        public GetAuthorLiteratureVM GetAuthorsDetails(int? id)
        {
            Author author = GetAuthorByID(id);
            var booksList = context.BookAuthors.Where(x => x.AuthorID == author.AuthorID).Select(x => x.Books).ToList();
            GetAuthorLiteratureVM authorVM = new GetAuthorLiteratureVM()
            {
                AuthorID = author.AuthorID,
                AuthorName = author.AuthorName,
                Books = booksList
            };
            return authorVM;
        }

        public GetAuthorLiteratureVM GetAuthorsBooksDetails(int? id)
        {
            var bookList = context.BookAuthors.Include(x => x.Books).Where(x => x.AuthorID == id).Select(x => x.Books).ToList();

            GetAuthorLiteratureVM authorVM = GetAuthorsDetails(id);
            authorVM.Books = bookList;
            return authorVM;
        }

        public GetAuthorLiteratureVM GetAuthorsArticlesDetails(int? id)
        {
            var articleList = context.ArticleAuthors.Include(x => x.Articles).Where(x => x.AuthorID == id).Select(x => x.Articles).ToList();

            GetAuthorLiteratureVM authorVM = GetAuthorsDetails(id);
            authorVM.Articles = articleList;
            return authorVM;
        }

        public GetAuthorLiteratureVM GetAuthorsMagazinesDetails(int? id)
        {
            var magazineList = context.MagazineAuthors.Include(x => x.Magazines).Where(x => x.AuthorID == id).Select(x => x.Magazines).ToList();

            GetAuthorLiteratureVM authorVM = GetAuthorsDetails(id);
            authorVM.Magazines = magazineList;
            return authorVM;
        }

        public GetAuthorLiteratureVM GetAuthorsPublicationsDetails(int? id)
        {
            var publicationsList = context.PublicationeAuthors.Include(x => x.Publications).Where(x => x.AuthorID == id).Select(x => x.Publications).ToList();

            GetAuthorLiteratureVM authorVM = GetAuthorsDetails(id);
            authorVM.Publications = publicationsList;
            return authorVM;
        }
    }
}
