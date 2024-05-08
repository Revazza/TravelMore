using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelMore.Application.Bookings.Commands.ProcessBookingPayment;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;
using TravelMore.Application.Services;
using TravelMore.Application.Services.Payments;
using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler(
    IUserIdentityService userIdentityService,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork,
    IDraftBookingRepository draftBookingRepository,
    ISender sender) : IRequestHandler<CreateBookingCommand, Result<Booking>>
{
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IDraftBookingRepository _draftBookingRepository = draftBookingRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result<Booking>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var guestId = _userIdentityService.GetUserId();
        var draftBooking = await GetDraftBookingAsync(request.DraftBookingId);


        return null;
    }

    private async Task<DraftBooking> GetDraftBookingAsync(Guid draftBookingId)
    {
        var draftBooking = await _draftBookingRepository
            .AsQuery()
            .Include(draftBooking => draftBooking.AppliedDiscounts)
            .Include(draftBooking => draftBooking.Guest)
            .Include(draftBooking => draftBooking.Hotel)
            .FirstOrDefaultAsync(draftBooking => draftBooking.Id == draftBookingId)
            ?? throw new Exception("Draft couldn't be found");

        return draftBooking;
    }

    private async Task ProcessPaymentAsync(DraftBooking draftBooking, IPaymentMethodData paymentData)
    {
        var result = await _sender.Send(new ProcessBookingPaymentCommand(draftBooking, paymentData));

    }

}
