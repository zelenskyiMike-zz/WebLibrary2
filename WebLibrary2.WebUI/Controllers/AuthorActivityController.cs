using System.Data;
using System.Web.Mvc;
using WebLibrary2.BusinessLogicLayer.Sevices;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.WebUI.Controllers
{
    public class AuthorActivityController : Controller
    {
        private AuthorService service;

        public AuthorActivityController(AuthorService service)
        {
            this.service = service;
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetBooksOFAuthor(int? id)
        {
            var authorVM = service.GetAuthorsBooksDetails(id);

            MultiSelectList books = new MultiSelectList(service.GetBooksNotExistInAuthor(authorVM), "BookID", "BookName", authorVM.Books);
            ViewData["Books"] = books;
            return PartialView("GetBooksOFAuthor",authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetBooksOFAuthor(GetAuthorLiteratureView authorVM, int[] bookIDsForDelete, int[] booksIDsForInsert)
        {
            var authorToUpdate = service.GetAuthorByID(authorVM.AuthorID);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    service.EditBooksOFAuthor(authorVM.AuthorID, bookIDsForDelete, booksIDsForInsert);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }
            var author = service.GetAuthorsBooksDetails(authorVM.AuthorID);

            MultiSelectList books = new MultiSelectList(service.GetBooksNotExistInAuthor(authorVM), "BookID", "BookName", authorVM.Books);
            ViewData["Books"] = books;

            return PartialView(authorVM);
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetArticlesOfAuthor(int? id)
        {
            var authorVM = service.GetAuthorsArticlesDetails(id);

            MultiSelectList articles = new MultiSelectList(service.GetArticlesNotExistInAuthor(authorVM), "ArticleID", "ArticleName", authorVM.Books);
            ViewData["Articles"] = articles;
            return PartialView(authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetArticlesOfAuthor(int id, int[] articlesIDsForDelete, int[] articlesIDsForInsert)
        {
            var authorToUpdate = service.GetAuthorByID(id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    service.EditArticlesOFAuthor(id, articlesIDsForDelete, articlesIDsForInsert);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }
            var authorVM = service.GetAuthorsArticlesDetails(id);

            MultiSelectList articles = new MultiSelectList(service.GetArticlesNotExistInAuthor(authorVM), "ArticleID", "ArticleName", authorVM.Books);
            ViewData["Articles"] = articles;

            return PartialView(authorVM);
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetMagazinesOfAuthor(int? id)
        {
            var authorVM = service.GetAuthorsMagazinesDetails(id);

            MultiSelectList magazines = new MultiSelectList(service.GetMagazinesNotExistInAuthor(authorVM), "MagazineID", "MagazineName", authorVM.Books);
            ViewData["Magazines"] = magazines;
            return PartialView(authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetMagazinesOfAuthor(int id, int[] magazinesIDsForDelete, int[] magazinesIDsForInsert)
        {
            var authorToUpdate = service.GetAuthorByID(id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    service.EditMagazinesOFAuthor(id, magazinesIDsForDelete, magazinesIDsForInsert);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }

            var authorVM = service.GetAuthorsMagazinesDetails(id);

            MultiSelectList magazines = new MultiSelectList(service.GetMagazinesNotExistInAuthor(authorVM), "MagazineID", "MagazineName", authorVM.Books);
            ViewData["Magazines"] = magazines;

            return PartialView(authorVM);
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult GetPublicationsOfAuthor(int? id)
        {
            var authorVM = service.GetAuthorsPublicationsDetails(id);

            MultiSelectList publications = new MultiSelectList(service.GetPublicationsNotExistInAuthor(authorVM), "PublicationID", "PublicationName", authorVM.Books);
            ViewData["Publications"] = publications;
            return PartialView(authorVM);
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetPublicationsOfAuthor(int id, int[] publicationsIDsForDelete, int[] publicationsIDsForInsert)
        {
            var authorToUpdate = service.GetAuthorByID(id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    service.EditPublicationsOFAuthor(id, publicationsIDsForDelete, publicationsIDsForInsert);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }

            var authorVM = service.GetAuthorsPublicationsDetails(id);

            MultiSelectList publications = new MultiSelectList(service.GetPublicationsNotExistInAuthor(authorVM), "PublicationID", "PublicationName", authorVM.Books);
            ViewData["Publications"] = publications;

            return PartialView(authorVM);
        }
    }
}