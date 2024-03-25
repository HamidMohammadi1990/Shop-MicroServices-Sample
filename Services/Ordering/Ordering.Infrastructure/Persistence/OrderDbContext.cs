using Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;

namespace Ordering.Infrastructure.Persistence;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "Hamid";
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "Hamid";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}