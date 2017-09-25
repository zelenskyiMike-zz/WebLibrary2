using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract;

namespace WebLibrary2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository repository;
     

        public HomeController(IBookRepository bookRepository)
        {
            this.repository = bookRepository;
        }
      
        public ViewResult Index()
        {
            
            return View(repository.Books);
        }

     
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}