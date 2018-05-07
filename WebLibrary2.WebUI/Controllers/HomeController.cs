using System.Web.Mvc;




namespace WebLibrary2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //public HomeController(EFDbContext context)
        //{
        //    this.context = context;

        //    var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
        //    serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");

        //}

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