using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasIndex(a => a.Email).IsUnique()
                .IncludeProperties(a => a.Name);

                entity.Property(a => a.Id).IsRequired();
                entity.Property(a => a.Name).IsRequired();
                entity.Property(a => a.Phone).IsRequired();
                entity.Property(a => a.Email).IsRequired();

            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasIndex(a => a.ClientId).IsUnique()
                .IncludeProperties(a => a.DateTime);

                entity.Property(a => a.Id).IsRequired();
                entity.Property(a => a.ClientId).IsRequired();
                entity.Property(a => a.DateTime).IsRequired();
                entity.Property(a => a.Description);

            });
        }
    }
}