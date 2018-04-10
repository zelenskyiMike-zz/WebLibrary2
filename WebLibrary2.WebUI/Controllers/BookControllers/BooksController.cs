using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractBook;

namespace WebLibrary2.WebUI.Controllers.BookControllers
{
    public class BooksController : Controller
    {
        IBookRepository booksRepository;
        public BooksController(IBookRepository bookRepository)
        {
            booksRepository = bookRepository;
        }

        public PartialViewResult BooksView()
        {
            var book = booksRepository.GetAllBooksWithGenres();
                   
            return PartialView(book);
        }
    }
}
