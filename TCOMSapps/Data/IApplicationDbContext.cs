using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TCOMSapps.Features.Account.entities;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Data
{
  public interface IApplicationDbContext : IDisposable
  {

    DbSet<County> Counties { get; set; }

    DbSet<Department> Departments { get; set; }
    DbSet<DepartmentSupervisor> DepartmentSupervisors { get; set; }


    DbSet<Interaction> Interactions { get; set; }
    DbSet<Location> Locations { get; set; }
    DbSet<Title> Titles { get; set; }
    DbSet<Transfer> Transfers { get; set; }


    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

  }
}
