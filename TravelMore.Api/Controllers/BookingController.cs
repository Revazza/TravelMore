using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelMore.Application.Bookings.Commands.CreateBooking;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Bookings;

namespace TravelMore.Api.Controllers;
[Route("api/booking")]
[ApiController]
public class BookingController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<Result<Booking>> Create(CreateBookingCommand command)
    {
        var result = await _sender.Send(command);
        return result;
    }
}
