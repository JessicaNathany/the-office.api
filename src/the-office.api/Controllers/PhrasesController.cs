using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers
{
    [ApiController]
    [Route("phrase")]
    public class PhrasesController : Controller
    {
        // GET: QuotesController
        public ActionResult Index()
        {
            return View();
        }
    }
}
