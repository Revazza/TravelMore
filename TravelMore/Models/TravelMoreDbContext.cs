using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelMore.ViewModels;

namespace TravelMore.Models
{
    public class TravelMoreDbContext : IdentityDbContext<User>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        public TravelMoreDbContext(DbContextOptions<TravelMoreDbContext> options) : base(options)
        {
            
        }

    }
}
