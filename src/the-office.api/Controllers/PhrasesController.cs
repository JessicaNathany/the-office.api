using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers
{
    [ApiController]
    [Route("phrase")]
    public class PhrasesController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Index()
        {
            throw new NotImplementedException();    
        }
    }
}
