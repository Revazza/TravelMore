using Microsoft.EntityFrameworkCore;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Memberships;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.Users;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Persistance.Contexts.TravelMore;

public partial class TravelMoreContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Guest> Guests { get; set; }
    public virtual DbSet<BookingPaymentDetails> PaymentsDetails { get; set; }
    public virtual DbSet<Host> Hosts { get; set; }
    public virtual DbSet<Hotel> Hotels { get; set; }
    public virtual DbSet<Booking> Bookings { get; set; }
    public virtual DbSet<Membership> Memberships { get; set; }
    public virtual DbSet<StandardMembership> StandardMemberships { get; set; }
    public virtual DbSet<PremiumMembership> PremiumMemberships { get; set; }


}
