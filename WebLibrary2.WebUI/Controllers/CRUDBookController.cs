using System.Web.Mvc;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;
using System.Net;
using System.Data;
using System.Collections.Generic;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDBookController : Controller
    {
        private EFDbContext context;

        IBookRepository bookRepository;
        IAuthorsRepository authorRepository;
        IBookAuthorsRepository bookAuthorRepository;

        public CRUDBookController(IBookRepository booksRepository, IAuthorsRepository authorsRepository, IBookAuthorsRepository bookAuthorsRepository, EFDbContext dataContext)
        {
            this.bookRepository = booksRepository;
            this.authorRepository = authorsRepository;
            this.bookAuthorRepository = bookAuthorsRepository;
            this.context = dataContext;
        }

        [HttpGet]
        public ActionResult CreateBook()
        {
            SelectList genres = new SelectList(context.Genres, "GenreID", "GenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["Genres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(BookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                bookRepository.InsertBook(bookVM);
                return RedirectToAction("BooksView", "Books");
            }

            SelectList genres = new SelectList(context.Genres, "GenreID", "GenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["Genres"] = genres;
            ViewData["Authors"] = authors;
            return View(bookVM);
        }


        public ActionResult BookDetails(int id = 0)
        {
            var bookVM = bookRepository.GetBooksDetails(id);
            return View(bookVM);
        }

        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = bookRepository.GetBooksDetails(id);

            SelectList genres = new SelectList(context.Genres, "GenreID", "GenreName", book.GenreID);
            ViewData["Genres"] = genres;

            MultiSelectList authors = new MultiSelectList(bookRepository.GetAuthorsNotExistInBook((int)id), "AuthorID", "AuthorName", book.Authors);
            ViewData["Authors"] = authors;

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(GetM2MCRUDBookVM book, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            var bookToUpdate = bookRepository.GetBookByID(book.BookID);

            //if (TryUpdateModel(bookToUpdate))
            //{
            bookAuthorRepository.DeleteAuthorFromBook(bookToUpdate.BookID, authorIDsForDelete);
            bookAuthorRepository.AddAuthorToBook(bookToUpdate.BookID, authorIDsForInsert);
            try
            {
                bookRepository.UpdateBook(bookToUpdate);
                return RedirectToAction("BooksView", "Books");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save");
            }

            //}

            SelectList genres = new SelectList(context.Genres, "GenreID", "GenreName", book.GenreID);
            ViewData["Genres"] = genres;

            MultiSelectList authors = new MultiSelectList(bookRepository.GetAuthorsNotExistInBook(book.BookID), "AuthorID", "AuthorName", book.Authors);
            ViewData["Authors"] = authors;

            return View(book);
        }



        public ActionResult DeleteBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetM2MCRUDBookVM book = bookRepository.GetBooksDetails(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(int id)
        {
            bookRepository.DeleteBook(id);
            bookRepository.SaveBook();
            return RedirectToAction("BooksView", "Books");
        }
    }
}