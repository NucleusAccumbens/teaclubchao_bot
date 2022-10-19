using Domain.Entities;

namespace Application.Common.Interfaces;

public interface ITeaShopBotDbContext
{
    DbSet<TlgUser> Users { get; }
    DbSet<Contacts> Contacts { get; }
    DbSet<Product> Products { get; }
    DbSet<Tea> Teas { get; }
    DbSet<Herb> Herbs { get; }
    DbSet<Honey> Honey { get; }
    DbSet<Order> Orders { get; }
    Task<int> SaveChangesAsync();

}
