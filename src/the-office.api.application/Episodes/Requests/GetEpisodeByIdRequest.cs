using the_office.api.application.Episodes.Responses;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.api.application.Episodes.Requests;

public class GetEpisodeByIdRequest : CommandHandler<EpisodeResponse>
{
    public GetEpisodeByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; }
}