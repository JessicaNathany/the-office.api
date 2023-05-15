using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers
{
    [ApiController]
    [Route("character")]
    public class CharactersController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }
    }
}
