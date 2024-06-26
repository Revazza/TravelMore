﻿using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Services.Payments;
using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Bookings.Commands.ProcessBookingPayment;

public record ProcessBookingPaymentCommand(DraftBooking Booking, IPaymentMethodData PaymentData) : IRequest<Result<object>>;

public class ProcessBookingPaymentCommandHandler : IRequestHandler<ProcessBookingPaymentCommand, Result<object>>
{


    public async Task<Result<object>> Handle(ProcessBookingPaymentCommand request, CancellationToken cancellationToken)
    {
        
        return null;
    }
}
