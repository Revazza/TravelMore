using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TravelMore.Models;
using TravelMore.ViewModels;

namespace TravelMore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TravelMoreController : ControllerBase
    {
        private readonly TravelMoreDbContext _context;
        private readonly UserManager<User> _userManager;

        public TravelMoreController(TravelMoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("hotels")]
        public IEnumerable<Hotel> GetAllHotels([FromQuery] HotelVM parameters)
        {
            IQueryable<Hotel> hotels = _context.Hotels;

            if (parameters.Id != Guid.Empty)
            {
                hotels = hotels.Where(hotel => hotel.Id == parameters.Id);
            }

            // TODO:
            // Add Filter by MinPrice & MaxPrice 

            return hotels;
        }

    }
}
