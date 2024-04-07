using Microsoft.EntityFrameworkCore;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.Users;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Hosts;
using TravelMore.Domain.Users.PremiumGuests;
using TravelMore.Domain.Users.StandartGuests;

namespace TravelMore.Persistance.Contexts.TravelMore;

public partial class TravelMoreContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Guest> Guests { get; set; }
    public virtual DbSet<StandardGuest> StandardGuests { get; set; }
    public virtual DbSet<PremiumGuest> PremiumGuests { get; set; }
    public virtual DbSet<BookingPaymentDetails> PaymentsDetails { get; set; }
    public virtual DbSet<Host> Hosts { get; set; }
    public virtual DbSet<Hotel> Hotels { get; set; }
    public virtual DbSet<Booking> Bookings { get; set; }

}
