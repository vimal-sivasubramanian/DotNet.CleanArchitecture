using DotNet.CleanArchitecture.Service.Application.Interfaces;
using DotNet.CleanArchitecture.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNet.CleanArchitecture.Service.Infrastructure.Persistence
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
