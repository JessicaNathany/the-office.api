using the_office.api.application.Season.Requests;
using the_office.api.application.Season.Responses;
using the_office.domain.Repositories;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.api.application.Season.Handlers
{
    public class SeasonHandler : CommandHandler<SeasonRequest, List<SeasonResponse>>
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonHandler(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        // to implement interface repository
        public override Task<CommandResponse<List<SeasonResponse>>> Handle(SeasonRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
