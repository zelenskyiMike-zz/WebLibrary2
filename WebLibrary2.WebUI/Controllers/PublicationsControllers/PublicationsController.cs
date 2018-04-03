using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebLibrary2.WebUI.Controllers.PublicationControllers
{
    public class PublicationsController : Controller
    {
        // GET: Publication
        public ActionResult Index()
        {
            return View();
        }
    }
}