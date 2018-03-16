using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;
using System.Data.Entity;
using System.Net;
using System.Data;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDBookController : Controller
    {
        EFDbContext context = new EFDbContext();

        IBookRepository bookRepository;
        public CRUDBookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
          
        }

        [HttpGet]
        public ActionResult CreateBook()
        {
            SelectList genres = new SelectList(context.Genres, "GenreID", "GenreName");
            SelectList authors = new SelectList(context.Authors, "AuthorID", "AuthorName");
            ViewBag.Genres = genres;
            ViewBag.Authors = authors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(AddABookViewModel book)
        {
            /*Works, but not write into AuthorBook*/
            bookRepository.InsertBook(book);
            bookRepository.SaveBook();


            return RedirectToAction("BooksView", "Books");
        }


        public ActionResult BookDetails(int id = 0)
        {
            Book book = context.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            AuthorBook aBook = context.AuthorBooks.Find(book.BookID);

            ///////////////////////////////////////////Make through LINQ////////////////////////////////////////////////////////////////
            IEnumerable<Author> authorList = context.Authors.SqlQuery("Select * from Authors where AuthorID in (Select AuthorBooks.AuthorID from AuthorBooks where AuthorBooks.BookID = " + id + ")").ToList();

            GetM2MCRUDBookVM bookVM = new GetM2MCRUDBookVM()
            {
                BookID = book.BookID,
                BookName = book.BookName,
                Authors = authorList
            };
                    
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
            SelectList genres = new SelectList(context.Genres,"GenreID","GenreName",book.GenreID);
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
    }
}