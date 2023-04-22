using the_office.api.application.Request;
using the_office.api.application.Response;
using the_office.insfrastructure.Data.Repository.Interface;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.api.application.Handlers.Season
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
