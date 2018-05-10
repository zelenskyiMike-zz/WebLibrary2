using System.Net;
using System.Web.Mvc;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDMagazineController : Controller
    {
        //EFDbContext context;
        //EFMagazineRepository magazineRepository;
        //EFMagazineAuthorRepository magazineAuthorsRepository;
        public CRUDMagazineController(/*EFDbContext contextParam, EFMagazineRepository magazinesRepository, EFMagazineAuthorRepository magazinesAuthorsRepository*/)
        {
            //context = contextParam;
            //magazineRepository = magazinesRepository;
            //magazineAuthorsRepository = magazinesAuthorsRepository;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateMagazine()
        {
            SelectList genres = new SelectList(context.MagazineGenres, "MagazineGenreID", "MagazineGenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["MagazineGenres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMagazine(GetMagazineView magazineVM)
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


        [Authorize(Roles = "user,admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMagazine(GetMagazineView magazineVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            if (magazineVM == null)
            {
                HttpNotFound();
            }

            var magazineToUpdate = magazineRepository.GetMagazineByID(magazineVM.MagazineID);

            if (TryUpdateModel(magazineToUpdate))
            {
                magazineAuthorsRepository.DeleteAuthorFromMagazine(magazineToUpdate.MagazineID, authorIDsForDelete);
                magazineAuthorsRepository.AddAuthorToMagazine(magazineToUpdate.MagazineID, authorIDsForInsert);
                magazineRepository.Save();
                return RedirectToAction("Index", "Home");
            }
            var magazine = magazineRepository.GetMagazineDetails(magazineVM.MagazineID);

            SelectList genres = new SelectList(context.MagazineGenres, "MagazineGenreID", "MagazineGenreName", magazine.MagazineGenreID);
            ViewData["MagazineGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(magazineRepository.GetAuthorsNotExistInMagazine((int)magazine.MagazineID), "AuthorID", "AuthorName", magazine.Authors);
            ViewData["Authors"] = authors;

            return View("EditMagazine", magazine);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteMagazine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GetMagazineView magazineVM = magazineRepository.GetMagazineDetails(id);
            if (magazineVM == null)
            {
                HttpNotFound();
            }
            return View(magazineVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMagazine(int id)
        {
            magazineRepository.DeleteMagazine(id);
            return RedirectToAction("Index", "Home");
        }
    }
}