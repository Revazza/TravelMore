using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Globalization;
using TravelMore.Models;

namespace TravelMore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GuestsController : ControllerBase
    {
        private readonly TravelMoreDbContext _context;
        private readonly UserManager<User> _userManager;

        public GuestsController(
            TravelMoreDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllGuests(string id)
        {
            var userExists = await _userManager.FindByIdAsync(id);

            if(userExists == null)
            {
                return BadRequest("User can't be found");
            }


            return Ok(userExists.Guests);
        }

        [HttpPost("add-guest")]
        public async Task<IActionResult> AddGuest([FromBody] Guest guest)
        {
            try
            {
                guest.Validate();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            var hotel = _context.Hotels.ToList().Find(hotel=>hotel.Id == guest.HotelId);
            if(hotel == null)
            {
                return BadRequest("Hotel not found");
            }


            var hotelOwner = await _userManager.FindByIdAsync(hotel.OwnerId.ToString());

            if(hotelOwner == null)
            {
                return BadRequest("Can't identify hotel owner");
            }

            hotelOwner.Guests.Add(guest);
            var result = await _userManager.UpdateAsync(hotelOwner);

            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("");
        }
    }

}
