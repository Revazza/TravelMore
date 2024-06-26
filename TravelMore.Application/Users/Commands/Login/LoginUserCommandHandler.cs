﻿using MediatR;
using System.Threading;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Services;
using TravelMore.Application.Users.Queries.CheckPassword;
using TravelMore.Application.Users.Queries.GetUserByEmail;
using TravelMore.Domain.Errors;

namespace TravelMore.Application.Users.Commands.Login;

public record LoginUserCommand(string Email, string Password) : IRequest<Result<string>>;

public class LoginUserCommandHandler(ISender sender, IJwtTokenGenerator tokenGenerator) : IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly ISender _sender = sender;
    private readonly IJwtTokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _sender.Send(new GetUserByEmailQuery(request.Email), cancellationToken);
        if (userResult.Value is null)
        {
            return DomainErrors.User.NotFoundByEmail;
        }

        var user = userResult.Value;

        var checkPasswordResult = await _sender.Send(new CheckPasswordQuery(request.Password, user.Salt, user.PasswordHash), cancellationToken);
        if (checkPasswordResult.IsFailure)
        {
            return checkPasswordResult.Error;
        }

        return _tokenGenerator.Generate(user);
    }

}
