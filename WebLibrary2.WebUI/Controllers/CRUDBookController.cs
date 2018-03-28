using System.Web.Mvc;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;
using System.Net;
using System.Data;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDBookController : Controller
    {
        private EFDbContext context;

        IBookRepository bookRepository;
        IAuthorsRepository authorRepository;
        IAuthorBooksRepository authorBookRepository;
      
        public CRUDBookController(IBookRepository booksRepository, IAuthorsRepository authorsRepository, IAuthorBooksRepository authorBooksRepository, EFDbContext dataContext)
        {
            this.bookRepository = booksRepository;
            this.authorRepository = authorsRepository;
            this.authorBookRepository = authorBooksRepository;
            this.context = dataContext;
        }



        [HttpGet]
        public ActionResult CreateBook()
        {
            SelectList genres = new SelectList(context.Genres, "GenreID", "GenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewBag.Genres = genres;
            ViewBag.Authors = authors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(BookViewModel bookVM)
        {
            /*Works, but not write into AuthorBook*/
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
            ViewBag.Genres = genres;
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(Book book)
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
            GetBookGenreCRUDBookVM book = bookRepository.GetBooksWithGenres(id);
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



















        [HttpGet]
        public ActionResult AddRelations(Book id)
        {
            //int bookID = (int)id;
            GetAuthorBookVM authorBookVM = new GetAuthorBookVM()
            {
                BookID = id.BookID
            };
            SelectList authors = new SelectList(context.Authors, "AuthorID", "AuthorName");
            ViewBag.Authors = authors;

            return View(authorBookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRelations(GetAuthorBookVM getAuthorBookVM)
        {
            //authorBookRepository.InsertAuthorBook(getAuthorBookVM);
            return RedirectToAction("BooksView", "Books");
        }



    }
}