using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelMore.Models;

namespace TravelMore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly TravelMoreDbContext _context;


        public BookingController(
            UserManager<User> userManager,
            TravelMoreDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("bookings/{id}")]
        public IEnumerable<Booking> GetAllUserBookings(string id)
        {
            var bookings = _context.Bookings
                .Where(booking => 
                    booking.GuestId.ToString() == id
                 )
                 .Include(b => b.BookedHotel).ToList();
            return bookings;
        }

        






    }
}
