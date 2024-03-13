using Microsoft.EntityFrameworkCore;

namespace TravelMore.Persistance.Contexts.TravelMore;

public partial class TravelMoreContext(DbContextOptions options) : DbContext(options)
{
}
