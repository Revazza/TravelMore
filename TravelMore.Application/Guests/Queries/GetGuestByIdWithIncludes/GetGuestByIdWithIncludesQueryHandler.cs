using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Guests;

namespace TravelMore.Application.Guests.Queries.GetGuestByIdWithIncludes;

public class GetGuestByIdWithIncludesQueryHandler(IGuestRepository guestRepository) : IRequestHandler<GetGuestByIdWithIncludesQuery, Result<Guest?>>
{
    private readonly IGuestRepository _guestRepository = guestRepository;

    public async Task<Result<Guest?>> Handle(GetGuestByIdWithIncludesQuery request, CancellationToken cancellationToken)
    {
        var includes = GetIncludes(request);

        var query = includes
        .Where(include => include.Key)
            .Aggregate(_guestRepository.AsQuery(), (guest, include) => guest.Include(include.Value));

        return await query.FirstOrDefaultAsync(guest => guest.Id == request.GuestId, cancellationToken);
    }

    private static Dictionary<bool, Expression<Func<Guest, object>>> GetIncludes(GetGuestByIdWithIncludesQuery request)
    => new()
    {
            { request.IncludeDiscounts, guest => guest.Discounts },
            { request.IncludeMembership, guest => guest.Membership },
            { request.IncludeBookings, guest => guest.Bookings },
            { request.IncludeBookingPayments, guest => guest.BookingPayments }
    };

}
