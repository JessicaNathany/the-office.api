using MediatR;
using Microsoft.AspNetCore.Mvc;
using the_office.api.application.Episodes.Requests;

namespace the_office.api.Controllers
{
    [ApiController]
    [Route("episodes")]
    public class EpisodesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EpisodesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult Get()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var request = new GetEpisodeByIdRequest(id);
            
            var response = await _mediator.Send(request);
            
            return Ok(response.Result);
        }
    }
}