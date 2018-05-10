using System.Web.Mvc;

namespace WebLibrary2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(string tabID)
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}