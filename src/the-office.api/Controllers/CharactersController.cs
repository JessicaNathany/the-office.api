using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers
{
    public class CharactersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
