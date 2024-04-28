using MediatR;
using System.Reflection;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Services;
using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Bookings.Commands.CreateDraftBooking;

public class CreateDraftBookingCommandHandler(
    IUserIdentityService userIdentityService, 
    IUnitOfWork unitOfWork, 
    ISender sender) : IRequestHandler<CreateDraftBookingCommand, Result<DraftBooking>>
{
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result<DraftBooking>> Handle(CreateDraftBookingCommand request, CancellationToken cancellationToken)
    {
        var guestId = _userIdentityService.GetUserId();


        return null;
    }
}
