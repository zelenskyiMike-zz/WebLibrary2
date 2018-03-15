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
using System.IO;
using Newtonsoft.Json;

namespace WebLibrary2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IAuthorsRepository repositry;
        public HomeController(IAuthorsRepository authorRepo)
        {
            this.repositry = authorRepo;
        }

        private EFDbContext context = new EFDbContext();

        // Make through repository!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        
        public ActionResult Index()
        {
            var author = context.Authors;

            return View(author.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }

    }
}