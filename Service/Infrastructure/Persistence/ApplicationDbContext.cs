using DotNet.EventSourcing.Service.Application.Interfaces;
using DotNet.EventSourcing.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNet.EventSourcing.Service.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
