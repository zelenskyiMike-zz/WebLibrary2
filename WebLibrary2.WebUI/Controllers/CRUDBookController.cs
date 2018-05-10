using System.Web.Mvc;
using System.Net;
using WebLibrary2.ViewModelsLayer.ViewModels;
using WebLibrary2.BusinessLogicLayer.Sevices;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDBookController : Controller
    {
        private readonly BookService bookService;
        private readonly AuthorService authorService;

        public CRUDBookController(BookService bookService, AuthorService authorService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateBook()
        {
            SelectList genres = new SelectList(bookService.GetAllGenres(), "GenreID", "GenreName");
            MultiSelectList authors = new MultiSelectList(authorService.GetAllAuthors(), "AuthorID", "AuthorName");
            ViewData["Genres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(GetBookView bookVM)
        {
            if (ModelState.IsValid)
            {
                bookService.CreateBook(bookVM);
                return RedirectToAction("BooksView", "Books");
            }

            SelectList genres = new SelectList(bookService.GetAllGenres(), "GenreID", "GenreName");
            MultiSelectList authors = new MultiSelectList(authorService.GetAllAuthors(), "AuthorID", "AuthorName");
            ViewData["Genres"] = genres;
            ViewData["Authors"] = authors;
            return View(bookVM);
        }

        [Authorize(Roles = "user,admin")]
        public ActionResult BookDetails(int id = 0)
        {
            var book = bookService.GetBookDetails(id);
            return View(book);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditBook(int id)
        {
            var book = bookService.GetBookDetails(id);

            SelectList genres = new SelectList(bookService.GetAllGenres(), "GenreID", "GenreName", book.GenreID);
            ViewData["Genres"] = genres;

            MultiSelectList authors = new MultiSelectList(bookService.GetAuthorsNotExistInBook(book), "AuthorID", "AuthorName", book.Authors);
            ViewData["Authors"] = authors;

            if (book == null)
            {
                return HttpNotFound();
            }

            return View("EditBook",book);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(GetBookView bookFromView, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            if (TryUpdateModel(bookFromView))
            {
                bookService.EditBook(bookFromView, authorIDsForDelete, authorIDsForInsert);

                return RedirectToAction("Index", "Home");
            }

            var book = bookService.GetBookDetails(bookFromView.BookID);
            SelectList genres = new SelectList(bookService.GetAllGenres(), "GenreID", "GenreName", book.GenreID);
            ViewData["Genres"] = genres;

            MultiSelectList authors = new MultiSelectList(bookService.GetAuthorsNotExistInBook(book), "AuthorID", "AuthorName", book.Authors);
            ViewData["Authors"] = authors;

            return View("EditBook", book);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteBook(int id)
        {
            GetAllBooksView book = bookService.GetBookDetails(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View("DeleteBook", book);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(GetAllBooksView bookFromView)
        {
            bookService.DeleteBook(bookFromView);
            return RedirectToAction("Index", "Home");
        }
    }
}