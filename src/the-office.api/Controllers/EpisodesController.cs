using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers
{
    public class EpisodesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
