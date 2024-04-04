using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;

namespace TravelMore.Application.Users.Queries.DoesUserExistByEmail;

public class DoesUserExistByEmailQueryHandler(IUserRepository userRepository) : IRequestHandler<DoesUserExistByEmailQuery, Result<bool>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<bool>> Handle(DoesUserExistByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.DoesUserExistByEmail(request.Email);
    }
}
