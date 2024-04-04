using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Users.Queries.CheckPassword;
using TravelMore.Application.Users.Queries.GetUserByEmail;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Errors;

namespace TravelMore.Application.Users.Commands.Login;

public record LoginUserCommand(string UserName, string Password) : IRequest<Result<string>>;

public class LoginUserCommandHandler(ISender sender) : IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly ISender _sender = sender;

    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _sender.Send(new GetUserByEmailQuery(request.UserName), cancellationToken);
        if (userResult.Value is null)
        {
            return DomainErrors.User.NotFoundByEmail;
        }

        var user = userResult.Value;

        var checkPasswordResult = await _sender.Send(new CheckPasswordQuery(request.Password, user.Salt, user.PasswordHash));
        if (checkPasswordResult.IsFailure)
        {
            return Error.None;
        }


        return Error.None;
    }
}
