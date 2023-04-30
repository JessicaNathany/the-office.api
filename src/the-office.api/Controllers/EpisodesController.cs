using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers
{
    [ApiController]
    [Route("episode")]
    public class EpisodesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
