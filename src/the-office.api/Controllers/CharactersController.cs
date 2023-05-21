using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;
using the_office.api.application.Characters.Requests;
using the_office.api.ModelExamples;
using the_office.domain.Response;

namespace the_office.api.Controllers
{
    [ApiController]
    [Route("character")]
    public class CharactersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CharactersController(IMediator meditor)
        {
            _mediator = meditor;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(ObjectResponse), StatusCodes.Status422UnprocessableEntity)]
        [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationResponseModelExamples))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundErrorModelExample))]
        [ProducesResponseType(typeof(ObjectResponse), StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CharacterModelExample))]
        public async Task<IActionResult> PostCharacterAsync([FromBody] CreateCharacterRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.IsValid)
                return StatusCode(response.GetStatusCode(), response.ErrorsTheOffice);

            return Ok(response);
        }

    }
}
