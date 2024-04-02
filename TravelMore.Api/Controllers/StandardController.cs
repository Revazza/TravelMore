using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelMore.Application.Common.Dtos;
using TravelMore.Application.Common.Results;
using TravelMore.Application.StandardGuests.Commands.CreateGuest;

namespace TravelMore.Api.Controllers;

[Route("api/standard-guest")]
[ApiController]
public class StandardController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;


    [HttpPost]
    public async Task<IActionResult> Create(CreateGuestCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

}
