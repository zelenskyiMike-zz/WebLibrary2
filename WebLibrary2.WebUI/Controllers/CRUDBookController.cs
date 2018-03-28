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
      
        public CRUDBookController(IBookRepository booksRepository, IAuthorsRepository authorsRepository, EFDbContext dataContext)
        {
            this.bookRepository = booksRepository;
            this.authorRepository = authorsRepository;
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
            bookRepository.InsertBook(bookVM);
            return RedirectToAction("BooksView", "Books");
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
            Book book = bookRepository.GetBookByID(id);

            SelectList genres = new SelectList(context.Genres, "GenreID", "GenreName", book.GenreID);
            ViewData["Genres"] = genres;

            GetSelectListViewModel getSelectListViewModel = new GetSelectListViewModel()
            {
                BookID = book.BookID,
                GenreID = book.GenreID,
                BookName = book.BookName,
                YearOfPublish = book.YearOfPublish
            };

            if (getSelectListViewModel == null)
            {
                return HttpNotFound();
            }

            return View(getSelectListViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(GetSelectListViewModel book)
        {
            var bookToUpdate = bookRepository.GetBookByID(book.BookID);

            if (TryUpdateModel(bookToUpdate))
            {
                try
                {
                    bookRepository.SaveBook();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }
            return RedirectToAction("BooksView", "Books");
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