using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers
{
    [ApiController]
    [Route("episode")]
    public class EpisodesController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }
    }
}
