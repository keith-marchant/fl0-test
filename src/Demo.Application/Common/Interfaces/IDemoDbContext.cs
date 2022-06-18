using Demo.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Application.Common.Interfaces
{
    public interface IDemoDbContext
    {
        DbSet<Job> Jobs { get; set; }
        DbSet<RoomType> RoomTypes { get; set; }

        // DbContext interfaces
        DatabaseFacade Database { get; }
        EntityEntry Entry([NotNull] object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
