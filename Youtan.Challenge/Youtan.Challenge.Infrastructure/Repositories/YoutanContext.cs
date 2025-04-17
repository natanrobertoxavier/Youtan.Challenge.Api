using Microsoft.EntityFrameworkCore;
using Youtan.Challenge.Domain.Entities;

namespace Youtan.Challenge.Infrastructure.Repositories;

public class YoutanContext(DbContextOptions<YoutanContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(YoutanContext).Assembly);
    }
}