using the_office.api.application.Request;
using the_office.api.application.Response;
using the_office.insfrastructure.Data.Repository.Interface;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.api.application.Handlers.Episodes
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
