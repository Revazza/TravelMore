using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Bookings.Queries.GetDraftBookingWithIncludes;

public class GetDraftBookingByIdWithIncludesQueryHandler(IDraftBookingRepository draftBookingRepository) : IRequestHandler<GetDraftBookingByIdWithIncludesQuery, Result<DraftBooking?>>
{
    private readonly IDraftBookingRepository _draftBookingRepository = draftBookingRepository;

    public async Task<Result<DraftBooking?>> Handle(GetDraftBookingByIdWithIncludesQuery request, CancellationToken cancellationToken)
    {
        var includes = GetIncludes(request);

        var query = includes
        .Where(include => include.Key)
            .Aggregate(_draftBookingRepository.AsQuery(), (draftBooking, include) => draftBooking.Include(include.Value));

        return await query.FirstOrDefaultAsync(draftBooking => draftBooking.Id == request.DraftBookingId, cancellationToken);
    }

    private Dictionary<bool, Expression<Func<DraftBooking, object>>> GetIncludes(GetDraftBookingByIdWithIncludesQuery request)
    => new()
    {
            { request.IncludeAppliedDiscounts, draftBooking => draftBooking.AppliedDiscounts },
            { request.IncludeGuest, draftBooking => draftBooking.Guest },
            { request.IncludeHotel, draftBooking => draftBooking.Hotel },
    };

}
