using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers
{
    public class QuotesController : Controller
    {
        // GET: QuotesController
        public ActionResult Index()
        {
            return View();
        }
    }
}
