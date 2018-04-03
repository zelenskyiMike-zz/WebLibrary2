using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity;
using System.Data.Entity;
using System.Net;
using System.Data;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Abstract.AbstractBook;

namespace WebLibrary2.WebUI.Controllers.BookControllers
{
    public class BooksController : Controller
    {
        IBookRepository booksRepository;
        public BooksController(IBookRepository bookRepository)
        {
            booksRepository = bookRepository;
        }

        public ViewResult BooksView()
        {
            var book = booksRepository.GetAllBooksWithGenres();
                   
            return View(book);
        }
    }
}
