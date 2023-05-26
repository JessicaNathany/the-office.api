using MediatR;
using the_office.domain.Shared;

namespace the_office.api.application.Common.Commands;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}