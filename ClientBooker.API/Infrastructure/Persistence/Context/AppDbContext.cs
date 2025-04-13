using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
    }
}
