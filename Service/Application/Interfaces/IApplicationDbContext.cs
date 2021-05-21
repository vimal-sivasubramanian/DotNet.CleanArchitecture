using DotNet.EventSourcing.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Service.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Person> Persons { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
