using Domain.Common;
using Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Meeting> Meetings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
