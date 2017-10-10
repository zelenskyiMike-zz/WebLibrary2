using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity;
using System.Data.Entity;

namespace WebLibrary2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private EFDbContext context = new EFDbContext();
              
        public ViewResult Index()
        {
            var books = context.Books.Include(p => p.Authors);
            return View(books.ToList());
        }

     
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}