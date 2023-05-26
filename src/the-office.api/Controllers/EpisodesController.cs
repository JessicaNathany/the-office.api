using MediatR;
using Microsoft.AspNetCore.Mvc;
using the_office.api.application.Episodes.Messaging.Requests;

namespace the_office.api.Controllers;

[ApiController]
[Route("episodes")]
public class EpisodesController : ApiController
{
    public EpisodesController(ISender sender) 
        : base(sender)
    {
    }

    [HttpGet]
    public ActionResult Get()
    {
        throw new NotImplementedException();
    }
        
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new GetEpisodeByIdRequest(id);
            
        var result = await Sender.Send(request, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);
            
        return Ok(result.Value);
    }
}