using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractArticle;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Abstract.AbstractPublication;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.WebUI.Controllers.AuthorControllers
{
    public class AuthorActivityController : Controller
    {
        IAuthorsRepository authorRepository;
        IBookAuthorsRepository bookAuthorRepository;
        IArticeAuthorsRepository articeAuthorRepository;
        IMagazineAuthorsRepository magazineAuthorRepository;
        IPublicationAuthorsRepository publicationAuthorRepository;

        public AuthorActivityController(IBookAuthorsRepository bookAuthorsRepository,
                                        IAuthorsRepository authorsRepository, 
                                        IMagazineAuthorsRepository magazineAuthorsRepository, 
                                        IPublicationAuthorsRepository publicationAuthorsRepository,
                                        IArticeAuthorsRepository articeAuthorsRepository)

        {
            authorRepository = authorsRepository;
            bookAuthorRepository = bookAuthorsRepository;
            articeAuthorRepository = articeAuthorsRepository;
            magazineAuthorRepository = magazineAuthorsRepository;
            publicationAuthorRepository = publicationAuthorsRepository;

        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetBooksOFAuthor(int? id)
        {
            var authorVM = authorRepository.GetAuthorsBooksDetails(id);

            MultiSelectList books = new MultiSelectList(authorRepository.GetBooksNotExistInAuthor((int)id), "BookID", "BookName", authorVM.Books);
            ViewData["Books"] = books;
            return PartialView(authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetBooksOFAuthor(/*GetM2MCRUDAuthorVM author*/int id, int[] bookIDsForDelete, int[] booksIDsForInsert)
        {
            var authorToUpdate = authorRepository.GetAuthorByID(/*author.AuthorID*/id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    bookAuthorRepository.DeleteBookFromAuthor(authorToUpdate.AuthorID, bookIDsForDelete);
                    bookAuthorRepository.AddBookToAuthor(authorToUpdate.AuthorID, booksIDsForInsert);
                    authorRepository.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }
            var authorVM = authorRepository.GetAuthorsBooksDetails(id);

            MultiSelectList books = new MultiSelectList(authorRepository.GetBooksNotExistInAuthor((int)id), "BookID", "BookName", authorVM.Books);
            ViewData["Books"] = books;

            return PartialView(authorVM);
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetArticlesOfAuthor(int? id)
        {
            var authorVM = authorRepository.GetAuthorsArticlesDetails(id);

            MultiSelectList articles = new MultiSelectList(authorRepository.GetArticlesNotExistInAuthor((int)id), "ArticleID", "ArticleName", authorVM.Books);
            ViewData["Articles"] = articles;
            return PartialView(authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetArticlesOfAuthor(int id, int[] articlesIDsForDelete, int[] articlesIDsForInsert)
        {
            var authorToUpdate = authorRepository.GetAuthorByID(id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    articeAuthorRepository.DeleteArticleFromAuthor(authorToUpdate.AuthorID, articlesIDsForDelete);
                    articeAuthorRepository.AddArticleToAuthor(authorToUpdate.AuthorID, articlesIDsForInsert);
                    authorRepository.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }
            var authorVM = authorRepository.GetAuthorsArticlesDetails(id);

            MultiSelectList articles = new MultiSelectList(authorRepository.GetArticlesNotExistInAuthor((int)id), "ArticleID", "ArticleName", authorVM.Books);
            ViewData["Articles"] = articles;

            return PartialView(authorVM);
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetMagazinesOfAuthor(int? id)
        {
            var authorVM = authorRepository.GetAuthorsMagazinesDetails(id);

            MultiSelectList magazines = new MultiSelectList(authorRepository.GetMagazinesNotExistInAuthor((int)id), "MagazineID", "MagazineName", authorVM.Books);
            ViewData["Magazines"] = magazines;
            return PartialView(authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetMagazinesOfAuthor(int id, int[] magazinesIDsForDelete, int[] magazinesIDsForInsert)
        {
            var authorToUpdate = authorRepository.GetAuthorByID(id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    magazineAuthorRepository.DeleteMagazineFromAuthor(authorToUpdate.AuthorID, magazinesIDsForDelete);
                    magazineAuthorRepository.AddMagazineToAuthor(authorToUpdate.AuthorID, magazinesIDsForInsert);
                    authorRepository.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }

            var authorVM = authorRepository.GetAuthorsMagazinesDetails(id);

            MultiSelectList magazines = new MultiSelectList(authorRepository.GetMagazinesNotExistInAuthor((int)id), "MagazineID", "MagazineName", authorVM.Books);
            ViewData["Magazines"] = magazines;

            return PartialView(authorVM);
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetPublicationsOfAuthor(int? id)
        {
            var authorVM = authorRepository.GetAuthorsPublicationsDetails(id);

            MultiSelectList publications = new MultiSelectList(authorRepository.GetPublicationsNotExistInAuthor((int)id), "PublicationID", "PublicationName", authorVM.Books);
            ViewData["Publications"] = publications;
            return PartialView(authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetPublicationsOfAuthor(int id, int[] publicationsIDsForDelete, int[] publicationsIDsForInsert)
        {
            var authorToUpdate = authorRepository.GetAuthorByID(id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    publicationAuthorRepository.DeletePublicationFromAuthor(authorToUpdate.AuthorID, publicationsIDsForDelete);
                    publicationAuthorRepository.AddPublicationToAuthor(authorToUpdate.AuthorID, publicationsIDsForInsert);
                    authorRepository.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }

            var authorVM = authorRepository.GetAuthorsPublicationsDetails(id);

            MultiSelectList publications = new MultiSelectList(authorRepository.GetPublicationsNotExistInAuthor((int)id), "PublicationID", "PublicationName", authorVM.Books);
            ViewData["Publications"] = publications;

            return PartialView(authorVM);
        }
    }
}