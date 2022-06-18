using Demo.Application.Common.Interfaces;
using Demo.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.Infrastructure.Persistence
{
    public class DemoDbContext : DbContext, IDemoDbContext
    {
        public DemoDbContext(
            DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
