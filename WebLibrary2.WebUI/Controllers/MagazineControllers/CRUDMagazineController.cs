using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Concrete.ConcreteMagazine;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.WebUI.Controllers.MagazineControllers
{
    public class CRUDMagazineController : Controller
    {
        EFDbContext context;
        EFMagazineRepository magazineRepository;
        EFMagazineAuthorRepository magazineAuthorsRepository;
        public CRUDMagazineController(EFDbContext contextParam, EFMagazineRepository magazinesRepository, EFMagazineAuthorRepository magazinesAuthorsRepository)
        {
            context = contextParam;
            magazineRepository = magazinesRepository;
            magazineAuthorsRepository = magazinesAuthorsRepository;
        }
        [HttpGet]
        public ActionResult CreateMagazine()
        {
            SelectList genres = new SelectList(context.MagazineGenres, "MagazineGenreID", "MagazineGenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["MagazineGenres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMagazine(MagazineViewModel magazineVM)
        {
            if (ModelState.IsValid)
            {
                magazineRepository.InsertMagazine(magazineVM);
                return RedirectToAction("Index", "Home");
            }
            SelectList genres = new SelectList(context.MagazineGenres, "MagazineGenreID", "MagazineGenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["MagazineGenres"] = genres;
            ViewData["Authors"] = authors;
            return View(magazineVM);
        }

        [HttpGet]
        public ActionResult MagazineDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var magazineVM = magazineRepository.GetMagazineDetails(id);
            return View(magazineVM);
        }

        [HttpGet]
        public ActionResult EditMagazine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var magazine = magazineRepository.GetMagazineDetails(id);

            if (magazine == null)
            {
                HttpNotFound();
            }

            SelectList genres = new SelectList(context.MagazineGenres, "MagazineGenreID", "MagazineGenreName", magazine.MagazineGenreID);
            ViewData["MagazineGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(magazineRepository.GetAuthorsNotExistInMagazine((int)id), "AuthorID", "AuthorName", magazine.Authors);
            ViewData["Authors"] = authors;

            return View(magazine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMagazine(GetM2MCRUDMagazineVM magazineVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            if (magazineVM == null)
            {
                HttpNotFound();
            }

            var magazineToUpdate = magazineRepository.GetMagazineByID(magazineVM.MagazineID);

            if (TryUpdateModel(magazineToUpdate))
            {
                try
                {
                    magazineAuthorsRepository.DeleteAuthorFromMagazine(magazineToUpdate.MagazineID, authorIDsForDelete);
                    magazineAuthorsRepository.AddAuthorToMagazine(magazineToUpdate.MagazineID, authorIDsForInsert);
                    magazineRepository.Save();
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
        public ActionResult DeleteMagazine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GetM2MCRUDMagazineVM magazineVM = magazineRepository.GetMagazineDetails(id);
            if (magazineVM == null)
            {
                HttpNotFound();
            }
            return View(magazineVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMagazine(int id)
        {
            magazineRepository.DeleteMagazine(id);
            return RedirectToAction("Index", "Home");
        }
    }
}