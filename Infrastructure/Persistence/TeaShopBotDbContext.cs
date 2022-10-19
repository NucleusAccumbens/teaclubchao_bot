using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class TeaShopBotDbContext : DbContext, ITeaShopBotDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    
    public TeaShopBotDbContext(DbContextOptions<TeaShopBotDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TlgUser> Users => Set<TlgUser>();

    public DbSet<Contacts> Contacts => Set<Contacts>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Tea> Teas => Set<Tea>();

    public DbSet<Herb> Herbs => Set<Herb>();

    public DbSet<Honey> Honey => Set<Honey>();

    public DbSet<Order> Orders => Set<Order>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Tea>().ToTable("Teas");
        builder.Entity<Herb>().ToTable("Herbs");
        builder.Entity<Honey>().ToTable("Honey");

        builder.Entity<Product>()
            .HasMany(c => c.Orders)
            .WithMany(c => c.Products);

        builder.Entity<Order>()
            .HasMany(c => c.Products)
            .WithMany(c => c.Orders);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}

