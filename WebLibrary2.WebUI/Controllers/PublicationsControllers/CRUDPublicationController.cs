using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractPublication;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Concrete.ConcretePublication;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.WebUI.Controllers.PublicationsControllers
{
    public class CRUDPublicationController : Controller
    {
        EFDbContext context;
        EFPublicationRepository publicationRepository;
        EFPublicationAuthorsRepository publicationAuthorsRepository;


        public CRUDPublicationController(EFDbContext contextParam, EFPublicationRepository publicationsRepository, EFPublicationAuthorsRepository publicationsAuthorsRepository)
        {
            context = contextParam;
            publicationRepository = publicationsRepository;
            publicationAuthorsRepository = publicationsAuthorsRepository;
        }

        [HttpGet]
        public ActionResult CreatePublication()
        {
            SelectList genres = new SelectList(context.PublicationGenres, "PublicationGenreID", "PublicationGenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["PublicationGenres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePublication(PublicationViewModel publicationVM)
        {
            if (ModelState.IsValid)
            {
                publicationRepository.InsertPublication(publicationVM);
                return RedirectToAction("Index", "Home");
            }
            SelectList genres = new SelectList(context.PublicationGenres, "PublicationGenreID", "PublicationGenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["PublicationGenres"] = genres;
            ViewData["Authors"] = authors;
            return View(publicationVM);
        }

        [HttpGet]
        public ActionResult PublicationDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var publicationVM = publicationRepository.GetPublicationDetails(id);
            return View(publicationVM);
        }

        [HttpGet]
        public ActionResult EditPublication(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var publication = publicationRepository.GetPublicationDetails(id);

            if (publication == null)
            {
                HttpNotFound();
            }

            SelectList genres = new SelectList(context.PublicationGenres, "PublicationGenreID", "PublicationGenreName", publication.PublicationGenreID);
            ViewData["PublicationGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(publicationRepository.GetAuthorsNotExistInPublication((int)id), "AuthorID", "AuthorName", publication.Authors);
            ViewData["Authors"] = authors;

            return View(publication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPublication(GetM2MCRUDPublicationVM publicationVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            if (publicationVM == null)
            {
                HttpNotFound();
            }

            var publicationToUpdate = publicationRepository.GetPublicationByID(publicationVM.PublicationID);

            if (TryUpdateModel(publicationToUpdate))
            {
                try
                {
                    publicationAuthorsRepository.DeleteAuthorFromPublication(publicationToUpdate.PublicationID, authorIDsForDelete);
                    publicationAuthorsRepository.AddAuthorToPublication(publicationToUpdate.PublicationID, authorIDsForInsert);
                    publicationRepository.Save();
                }
                catch (DataException dex)
                {
                    ModelState.AddModelError("", "Unable to save");
                    Console.WriteLine(dex);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DeletePublication(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GetM2MCRUDPublicationVM publicationVM = publicationRepository.GetPublicationDetails(id);
            if (publicationVM == null)
            {
                HttpNotFound();
            }
            return View(publicationVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePublication(int id)
        {
            publicationRepository.DeletePublication(id);
            return RedirectToAction("Index", "Home");
        }
    }
}