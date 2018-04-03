using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractAuthor;

namespace WebLibrary2.WebUI.Controllers.AuthorControllers
{
    public class AuthorController : Controller
    {
        IAuthorsRepository authorsRepository;
        public AuthorController(IAuthorsRepository authorRepository)
        {
            authorsRepository = authorRepository;
        }
        // GET: Author
        public ActionResult AuthorView()
        {
            var author = authorsRepository.GetAllAuthors().ToList();
            return View(author);
        }
    }
}