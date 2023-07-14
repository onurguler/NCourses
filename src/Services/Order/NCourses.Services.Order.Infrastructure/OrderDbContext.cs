using Microsoft.EntityFrameworkCore;

using NCourses.Services.Order.Domain.OrderAggregate;

namespace NCourses.Services.Order.Infrastructure;

public class OrderDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "ordering";

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.OrderAggregate.Order> Orders => Set<Domain.OrderAggregate.Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.OrderAggregate.Order>().ToTable("Orders", DEFAULT_SCHEMA);
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);

        modelBuilder.Entity<OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Domain.OrderAggregate.Order>().OwnsOne<Address>(x => x.Address).WithOwner();

        base.OnModelCreating(modelBuilder);
    }
}