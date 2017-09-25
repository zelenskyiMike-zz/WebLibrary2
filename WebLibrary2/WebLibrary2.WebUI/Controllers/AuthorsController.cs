using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract;


namespace WebLibrary2.WebUI.Controllers
{
    public class AuthorsController : Controller
    {
        private IAuthorsRepository repository;

        public AuthorsController(IAuthorsRepository authorRepository)
        {
            this.repository = authorRepository;
        }

        // GET: Authors
        public ViewResult Autors()
        {
            //ViewBag.Message = "Your application description page.";

            return View(repository.Authors);
        }
    }
}