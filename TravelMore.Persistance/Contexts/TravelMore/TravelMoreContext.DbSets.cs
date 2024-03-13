using Microsoft.EntityFrameworkCore;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Users;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Persistance.Contexts.TravelMore;

public partial class TravelMoreContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Guest> Guests { get; set; }
    public virtual DbSet<Host> Hosts { get; set; }
    public virtual DbSet<Hotel> Hotels { get; set; }
    public virtual DbSet<Booking> Bookings { get; set; }

}
