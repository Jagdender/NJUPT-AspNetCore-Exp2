using Microsoft.EntityFrameworkCore;

namespace NJUPT_AspNetCore_Exp2.Models;

public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Party> Parties { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityConfiguration configuration = new();

        modelBuilder.ApplyConfiguration<User>(configuration);
        modelBuilder.ApplyConfiguration<Party>(configuration);
    }
}
