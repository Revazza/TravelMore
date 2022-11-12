using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TravelMore.Models;
using TravelMore.ViewModels;

namespace TravelMore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly TravelMoreDbContext _context;
        private readonly UserManager<User> _userManager;

        public ProfileController(
            TravelMoreDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile([FromQuery] string id)
        {

            var userExists = await _userManager.FindByIdAsync(id);

            if (userExists == null)
            {
                return BadRequest("User doesn't exist");
            }

            return Ok(userExists);
        }

        [HttpPost("add-hotel")]
        public async Task<IActionResult> AddHotel([FromBody] Hotel hotel)
        {
            var userExists = await _userManager.FindByIdAsync(hotel.OwnerId.ToString());
            if (userExists == null)
            {
                return BadRequest("Can't identify user");
            }

            try
            {
                hotel.Validate();
            }
            catch (ArgumentException e)
            {

                throw;
            }

            hotel.Id = Guid.NewGuid();

            userExists.OwnedHotelID = hotel.Id;
            _context.Hotels.Add(hotel);
            await _userManager.UpdateAsync(userExists);
            await _context.SaveChangesAsync();
            return Created($"hotel/{hotel.Id}", hotel);
        }

        [HttpDelete("delete-hotel/{id}")]
        public async Task<IActionResult> DeleteHotel(string id)
        {
            var userExists = await _userManager.FindByIdAsync(id);

            if (userExists != null)
            {
                var hotelToRemove = _context.Hotels.SingleOrDefault(hotel =>
                    hotel.Id.ToString() == userExists.OwnedHotelID.ToString()
                );
                if (hotelToRemove != null)
                {
                    _context.Hotels.Remove(hotelToRemove);
                    userExists.OwnedHotelID = null;
                    await _context.SaveChangesAsync();
                    await _userManager.UpdateAsync(userExists);
                    return Ok($"Hotel deleted");
                }
            }

            return BadRequest($"Hotel with id: {id} doesn't exist");
        }
        [HttpPost("change-information")]
        public async Task<IActionResult> ChangeInformation(ChangeInformationVM parameters)
        {
            var userExists = await _userManager.FindByIdAsync(parameters.UserId.ToString());
            if (userExists == null)
            {
                return BadRequest($"User with {parameters.UserId} doesn't exist");
            }
            if(parameters.UpdateProperty == "email")
            {
                userExists.Email = parameters.NewValue;
            }
            else if(parameters.UpdateProperty == "userName")
            {
                userExists.UserName = parameters.NewValue;
            }
            else if(parameters.UpdateProperty == "password")
            {
                await _userManager.RemovePasswordAsync(userExists);
                await _userManager.AddPasswordAsync(userExists,parameters.NewValue);
            }
            else
            {
                return BadRequest("UpdateProperty is not valid");
            }

            await _userManager.UpdateAsync(userExists);

            return Ok("Updated Successfuly");
        }
    }
}
