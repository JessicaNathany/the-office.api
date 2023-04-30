using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers
{
    [ApiController]
    [Route("character")]
    public class CharactersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
