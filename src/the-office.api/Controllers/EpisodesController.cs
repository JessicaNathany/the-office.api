using MediatR;
using Microsoft.AspNetCore.Mvc;
using the_office.api.application.Common.Pagination;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Shared;

namespace the_office.api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("episodes")]
public class EpisodesController : ApiController
{
    public EpisodesController(ISender sender) 
        : base(sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(EpisodeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<Result<EpisodeResponse>> Register([FromBody] RegisterEpisodeRequest request)
    {
        return await Sender.Send(request);
    }
        
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EpisodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<Result<EpisodeResponse>> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new GetEpisodeByIdRequest(id);
        return await Sender.Send(request, cancellationToken);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<EpisodeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<Result<PagedResult<EpisodeResponse>>> Get([FromQuery] int? page, [FromQuery]int? pageSize, CancellationToken cancellationToken)
    {
        var request = new GetEpisodesRequest(page, pageSize);
        return await Sender.Send(request, cancellationToken);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(EpisodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<Result<EpisodeResponse>> Update([FromRoute] int id, [FromBody] UpdateEpisodeRequest request)
    {
        request.Id = id;
        return await Sender.Send(request);
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<Result> Remove([FromRoute] int id)
    {
        var request = new RemoveEpisodeRequest(id);
        return await Sender.Send(request);
    }
}