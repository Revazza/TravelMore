﻿using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Services.PasswordHasher;

namespace TravelMore.Application.Users.Queries.CheckPassword;

public record CheckPasswordQuery(string Password, string Salt, string HashedPassword) : IRequest<Result<bool>>;

public class CheckPasswordQueryHandler(IPasswordHasher passwordHasher) : IRequestHandler<CheckPasswordQuery, Result<bool>>
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Result<bool>> Handle(CheckPasswordQuery request, CancellationToken cancellationToken)
    {
        var hashPasswordResponse = _passwordHasher.Hash(request.Password, request.Salt);
        var isCorrect = hashPasswordResponse.HashedPassword == request.HashedPassword;
        return await Task.FromResult(isCorrect);
    }
}
