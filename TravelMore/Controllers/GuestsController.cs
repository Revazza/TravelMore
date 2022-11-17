using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Globalization;
using TravelMore.Models;
using TravelMore.ViewModels;

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
        public async Task<IEnumerable<Guest>> GetAllGuests(string id)
        {
            return _context.Guests.Where(guest => guest.HotelOwnerId.ToString() == id).ToList();
        }


        private string DateTimeConverter(DateTime dateTime)
        {
            return dateTime.ToString("d", CultureInfo.GetCultureInfo("de-DE"));
        }

        [HttpPost("add-guest")]
        public async Task<IActionResult> AddGuest([FromBody] GuestVM guest)
        {
            if( string.IsNullOrEmpty(guest.HostFrom.ToString()) ||
                string.IsNullOrEmpty(guest.HostTo.ToString()))
            {
                return BadRequest("Please choose dates");
            }

            var guestExists = await _userManager.FindByIdAsync(guest.GuestId.ToString());

            if (guestExists == null)
            {
                return BadRequest("Can't identify user (guest)");
            }

            var hotel = _context.Hotels.FirstOrDefault(hotel => hotel.Id == guest.HotelId);

            if (hotel == null)
            {
                return BadRequest("Hotel couldn't be found");
            }

            if (hotel.OwnerId.ToString() == guestExists.Id)
            {
                return BadRequest("Can't book your own hotel");
            }

            var hotelOwer = await _userManager.FindByIdAsync(hotel.OwnerId.ToString());

            var bookingExists = _context.Bookings.FirstOrDefault(booking =>
                booking.GuestId == guest.GuestId &&
                booking.HostFrom == guest.HostFrom &&
                booking.HostTo == guest.HostTo
                );

            if (bookingExists != null)
            {
                var hostFrom = DateTimeConverter(bookingExists.HostFrom);
                var hostTo = DateTimeConverter(bookingExists.HostTo);
                return BadRequest($"Already Book from {hostFrom} to {hostTo}");
            }

            var newGuest = new Guest()
            {
                Id = Guid.NewGuid(),
                GuestId = guest.GuestId,
                HostFrom = guest.HostFrom,
                HostTo = guest.HostTo,
                FirstName = guestExists.FirstName!,
                LastName = guestExists.LastName!,
                HotelOwnerId = hotel.OwnerId,
                HotelId = hotel.Id,
            };

            var newBooking = new Booking()
            {
                Id = Guid.NewGuid(),
                BookedHotel = hotel,
                GuestId = guest.GuestId,
                HostFrom = guest.HostFrom,
                HostTo = guest.HostTo
            };




            Console.WriteLine();
            _context.Guests.Add(newGuest);
            _context.Bookings.Add(newBooking);


            _context.SaveChanges();

            return Ok("Hotel Booked Successfuly");
        }


        [HttpPost("update-guest")]
        public IActionResult UpdateGuest([FromBody] UpdateGuestVM guest)
        {
            var guestExists = _context.Guests.FirstOrDefault(g =>
                g.GuestId == guest.GuestId &&
                g.HotelId == guest.HotelId &&
                g.HostFrom == guest.HostFrom &&
                g.HostTo == guest.HostTo 
                );

            if (guestExists == null)
            {
                return BadRequest("Can't identify guest");
            }
            if(guestExists.Status == Status.Accepted)
            {
                return BadRequest($"Hotel is already booked from {DateTimeConverter(guest.HostFrom)} to {DateTimeConverter(guest.HostTo)}");
            }

            Console.WriteLine();
            

            guestExists.Status = guest.Status;

            var booking = _context.Bookings.Include(b=>b.BookedHotel).FirstOrDefault(booking =>
                booking.GuestId == guest.GuestId &&
                booking.BookedHotel!.Id == guest.HotelId &&
                booking.HostFrom == guest.HostFrom &&
                booking.HostTo == guest.HostTo
                );

            booking!.Status = guest.Status;

            _context.SaveChanges();

            return Ok("Guest Status Changed");
        }
    }

}
