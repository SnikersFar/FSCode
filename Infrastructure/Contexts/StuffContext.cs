using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class StuffContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected StuffContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(u => u.Orders).WithOne(o => o.Customer);
            modelBuilder.Entity<User>().HasMany(u => u.Orders).WithOne(o => o.Executor);

            modelBuilder.Entity<Message>().HasOne(u => u.Order).WithMany(o => o.Messages);
            modelBuilder.Entity<Message>().HasOne(u => u.Creator).WithMany(o => o.Messages);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}