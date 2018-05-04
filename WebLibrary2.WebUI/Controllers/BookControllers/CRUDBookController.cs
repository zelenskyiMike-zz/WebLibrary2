using System.Web.Mvc;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Models;
using System.Net;
using System.Data;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Concrete.ConcreteBook;
using WebLibrary2.Domain.Concrete.ConcreteAuthor;

namespace WebLibrary2.WebUI.Controllers.BookControllers
{
    public class CRUDBookController : Controller
    {
        private EFDbContext context;

        EFBookRepository bookRepository;
        EFAuthorRepository authorRepository;
        EFBookAuthorRepository bookAuthorRepository;


        public CRUDBookController(EFBookRepository booksRepository, EFAuthorRepository authorsRepository, EFBookAuthorRepository bookAuthorsRepository, EFDbContext dataContext)
        {
            this.bookRepository = booksRepository;
            this.authorRepository = authorsRepository;
            this.bookAuthorRepository = bookAuthorsRepository;
            this.context = dataContext;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateBook()
        {
            SelectList genres = new SelectList(context.Genres, "GenreID", "GenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["Genres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "user,admin")]
        public ActionResult BookDetails(int id = 0)
        {
            var book = bookRepository.GetBooksDetails(id);
            return View(book);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(GetM2MCRUDBookVM bookFromView, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            var bookToUpdate = bookRepository.GetBookByID(bookFromView.BookID);

            if (TryUpdateModel(bookToUpdate))
            {
                bookAuthorRepository.DeleteAuthorFromBook(bookToUpdate.BookID, authorIDsForDelete);
                bookAuthorRepository.AddAuthorToBook(bookToUpdate.BookID, authorIDsForInsert);
                bookRepository.Save();
                return RedirectToAction("Index", "Home");
            }

            var book = bookRepository.GetBooksDetails(bookFromView.BookID);
            SelectList genres = new SelectList(context.Genres, "GenreID", "GenreName", book.GenreID);
            ViewData["Genres"] = genres;

            MultiSelectList authors = new MultiSelectList(bookRepository.GetAuthorsNotExistInBook((int)book.BookID), "AuthorID", "AuthorName", book.Authors);
            ViewData["Authors"] = authors;

            return View("EditBook", book);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(int id)
        {
            bookRepository.DeleteBook(id);
            bookRepository.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}