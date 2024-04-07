using MediatR;
using TravelMore.Application.Common.Dtos;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Guests.StandardGuests.GetStandardGuestByEmail;

namespace TravelMore.Application.Guests.Commands.UpgradeToPremiumGuest;

public record UpgradeToPremiumGuestCommand(string Email) : IRequest<Result<PremiumGuestDto>>;

public class UpgradeToPremiumGuestCommandHandler(ISender sender) : IRequestHandler<UpgradeToPremiumGuestCommand, Result<PremiumGuestDto>>
{
    private readonly ISender _sender = sender;


    public async Task<Result<PremiumGuestDto>> Handle(UpgradeToPremiumGuestCommand request, CancellationToken cancellationToken)
    {
        var guestResult = await _sender.Send(new GetStandardGuestByEmailQuery(request.Email), cancellationToken);
        if (guestResult.IsFailure)
        {
            return guestResult.Error;
        }

        var guest = guestResult.Value!;
        var premium = guest.CreatePremiumVersion();

        return null;
    }
}
