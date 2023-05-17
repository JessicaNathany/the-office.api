using the_office.api.application.Episodes.Requests;
using the_office.api.application.Episodes.Responses;
using the_office.domain.Interface;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.api.application.Episodes.Handlers
{
    public class EpisodeHandler : CommandHandler<EpisodeRequest, List<EpisodeResponse>>
    {
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodeHandler(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }
    
        public override Task<CommandResponse<List<EpisodeResponse>>> Handle(EpisodeRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
