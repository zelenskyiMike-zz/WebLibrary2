using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.WebUI.Controllers.AuthorControllers
{
    public class AuthorActivityController : Controller
    {
        //EFDbContext context;
        IAuthorsRepository authorRepository;
        IBookAuthorsRepository bookAuthorRepository;
        public AuthorActivityController(IBookAuthorsRepository bookAuthorsRepository, IAuthorsRepository authorsRepository)
        {
            bookAuthorRepository= bookAuthorsRepository;
            authorRepository = authorsRepository;
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetBooksOFAuthor(int? id)
        {
            var authorVM = authorRepository.GetAuthorDetails(id);

            MultiSelectList books = new MultiSelectList(authorRepository.GetBooksNotExistInAuthor((int)id), "BookID", "BookName", authorVM.Books);
            ViewData["Books"] = books;
            return PartialView(authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        public PartialViewResult GetBooksOFAuthor(GetM2MCRUDAuthorVM author, int[] bookIDsForDelete, int[] booksIDsForInsert)
        {
            var authorToUpdate = authorRepository.GetAuthorByID(author.AuthorID);

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
            return PartialView();
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetArticlesOfAuthor(int? id)
        {
            var authorVM = authorRepository.GetAuthorDetails(id);

            MultiSelectList articles = new MultiSelectList(authorRepository.GetArticlesNotExistInAuthor((int)id), "ArticleID", "ArticleName", authorVM.Books);
            ViewData["Articles"] = articles;
            return PartialView(authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        public PartialViewResult GetArticlesOfAuthor(GetM2MCRUDAuthorVM author, int[] articlesIDsForDelete, int[] articlesIDsForInsert)
        {
            var authorToUpdate = authorRepository.GetAuthorByID(author.AuthorID);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    bookAuthorRepository.DeleteBookFromAuthor(authorToUpdate.AuthorID, articlesIDsForDelete);
                    bookAuthorRepository.AddBookToAuthor(authorToUpdate.AuthorID, articlesIDsForInsert);
                    authorRepository.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }
            return PartialView();
        }
    }
}