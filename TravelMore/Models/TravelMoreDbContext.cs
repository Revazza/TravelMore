using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TravelMore.Models
{
    public class TravelMoreDbContext : IdentityDbContext<User>
    {
        public DbSet<Hotel> Hotels { get; set; }

        public TravelMoreDbContext(DbContextOptions<TravelMoreDbContext> options) : base(options)
        {

        }
    }
}
