using Microsoft.EntityFrameworkCore;

namespace TravelMore.Persistance.Contexts.TravelMore;

public partial class TravelMoreContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Apply all the configurations defined in Configurations folder
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TravelMoreContext).Assembly);
    }
}
