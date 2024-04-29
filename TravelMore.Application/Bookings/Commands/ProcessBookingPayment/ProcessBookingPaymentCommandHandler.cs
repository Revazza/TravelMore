using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Bookings.Commands.ProcessBookingPayment;

public record ProcessBookingPaymentCommand(DraftBooking Booking) : IRequest<Result<object>>;

public class ProcessBookingPaymentCommandHandler : IRequestHandler<ProcessBookingPaymentCommand, Result<object>>
{
    public async Task<Result<object>> Handle(ProcessBookingPaymentCommand request, CancellationToken cancellationToken)
    {
        
        return null;
    }
}
