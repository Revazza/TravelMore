using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Users;

namespace TravelMore.Application.Users.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByEmailQuery, Result<User?>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<User?>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByEmailAsync(request.Email);
    }
}
