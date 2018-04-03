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


namespace WebLibrary2.WebUI.Controllers.BookControllers
{
    public class BooksController : Controller
    {
        private EFDbContext context = new EFDbContext();

        public ViewResult BooksView()
        {
            var book = context.Books.Include(g => g.Genres);
                   
            return View(book);
        }
    }
}
