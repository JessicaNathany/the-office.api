using MediatR;
using Microsoft.AspNetCore.Mvc;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.api.application.Seasons.Messaging.Responses;
using the_office.domain.Shared;

namespace the_office.api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("seasons")]
public class SeasonsController : ApiController
{
    public SeasonsController(ISender sender) 
        : base(sender)
    {
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(SeasonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<Result<SeasonResponse>> Register([FromBody] RegisterSeasonRequest request)
    {
        return await Sender.Send(request);
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SeasonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<Result<SeasonResponse>> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new GetSeasonByIdRequest(id);
        return await Sender.Send(request, cancellationToken);
    }
}