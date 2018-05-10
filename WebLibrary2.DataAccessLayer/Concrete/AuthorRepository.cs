using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class AuthorRepository : IAuthorsRepository
    {
        private DbContext context;
        public AuthorRepository(DbContext contextParam)
        {
            context = contextParam;
        }


        public IEnumerable<Author> Authors
        {
            get { return context.Authors; }
        }

        public void CreateAuthor(Author authorVM)
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



        public List<Book> GetBooksNotExistInAuthor(Author author)
        {
            //var currAuthor = GetAuthorByID(authorID);
            List<Book> finalListOfBooks = new List<Book>();

            var initBookAuthorList = context.BookAuthors.Where(x => x.AuthorID == author.AuthorID).Select(x => x.Books).ToList();

            foreach (var item in context.Books.ToList())
            {
                if (!initBookAuthorList.Contains(item))
                {
                    finalListOfBooks.Add(item);
                }
            }
            return finalListOfBooks;
        }

        public List<Article> GetArticlesNotExistInAuthor(Author author)
        {
            //var currAuthor = GetAuthorByID(authorID);

            List<Article> finalListOfArticles = new List<Article>();

            var initArticleAuthorList = context.ArticleAuthors.Where(x => x.AuthorID == author.AuthorID).Select(x => x.Articles).ToList();

            foreach (var item in context.Articles.ToList())
            {
                if (!initArticleAuthorList.Contains(item))
                {
                    finalListOfArticles.Add(item);
                }
            }
            return finalListOfArticles;
        }

        public List<Magazine> GetMagazinesNotExistInAuthor(Author author)
        {
            //var currAuthor = GetAuthorByID(authorID);
            List<Magazine> finalListOfMagazines = new List<Magazine>();

            var initMagazineAuthorsList = context.MagazineAuthors.Where(x => x.AuthorID == author.AuthorID).Select(x => x.Magazines).ToList();

            foreach (var item in context.Magazines.ToList())
            {
                if (!initMagazineAuthorsList.Contains(item))
                {
                    finalListOfMagazines.Add(item);
                }
            }
            return finalListOfMagazines;
        }

        public List<Publication> GetPublicationsNotExistInAuthor(Author author)
        {
            //var currAuthor = GetAuthorByID(authorID);
            List<Publication> finalListOfPublication = new List<Publication>();

            var initPublicationsAuthorList = context.PublicationeAuthors.Where(x => x.AuthorID == author.AuthorID).Select(x => x.Publications).ToList();

            foreach (var item in context.Publications.ToList())
            {
                if (!initPublicationsAuthorList.Contains(item))
                {
                    finalListOfPublication.Add(item);
                }
            }
            return finalListOfPublication;
        }



        public Author GetAuthorsDetails(Author author)
        {
            List<Book> booksList = context.BookAuthors.Where(x => x.AuthorID == author.AuthorID).Select(x => x.Books).ToList();
            List<Article> articlesList = context.ArticleAuthors.Where(x => x.AuthorID == author.AuthorID).Select(x => x.Articles).ToList();
            List<Magazine> magazinesList = context.MagazineAuthors.Where(x => x.AuthorID == author.AuthorID).Select(x => x.Magazines).ToList();
            List<Publication> publicationsList = context.PublicationeAuthors.Where(x => x.AuthorID == author.AuthorID).Select(x => x.Publications).ToList();

            Author authorVM = new Author()
            {
                AuthorID = author.AuthorID,
                AuthorName = author.AuthorName,
                Books = booksList,
                Articles = articlesList,
                Magazines = magazinesList,
                Publications = publicationsList
            };
            return authorVM;
        }

        public Author GetAuthorsBooksDetails(int? id)
        {
            var author = GetAuthorByID(id);
            var bookList = context.BookAuthors.Include(x => x.Books).Where(x => x.AuthorID == id).Select(x => x.Books).ToList();

            Author authorVM = GetAuthorsDetails(author);
            authorVM.Books = bookList;
            return authorVM;
        }

        public Author GetAuthorsArticlesDetails(int? id)
        {
            var author = GetAuthorByID(id);
            var articleList = context.ArticleAuthors.Include(x => x.Articles).Where(x => x.AuthorID == id).Select(x => x.Articles).ToList();

            Author authorVM = GetAuthorsDetails(author);
            authorVM.Articles = articleList;
            return authorVM;
        }

        public Author GetAuthorsMagazinesDetails(int? id)
        {
            var author = GetAuthorByID(id);
            var magazineList = context.MagazineAuthors.Include(x => x.Magazines).Where(x => x.AuthorID == id).Select(x => x.Magazines).ToList();

            Author authorVM = GetAuthorsDetails(author);
            authorVM.Magazines = magazineList;
            return authorVM;
        }

        public Author GetAuthorsPublicationsDetails(int? id)
        {
            var author = GetAuthorByID(id);
            var publicationsList = context.PublicationeAuthors.Include(x => x.Publications).Where(x => x.AuthorID == id).Select(x => x.Publications).ToList();

            Author authorVM = GetAuthorsDetails(author);
            authorVM.Publications = publicationsList;
            return authorVM;
        }
    }
}
