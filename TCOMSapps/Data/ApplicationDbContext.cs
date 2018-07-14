using Microsoft.EntityFrameworkCore;
using TCOMSapps.Features.Account.entities;
using TCOMSapps.Features.OOSTitles.entities;
using TCOMSapps.Services.AppSettings;

namespace TCOMSapps.Data
{
  public class ApplicationDbContext : ApplicationBaseContext<ApplicationDbContext>, IApplicationDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {

      base.OnModelCreating(builder);
      builder.Entity<DepartmentSupervisor>().HasKey(table => new
      {
        table.DepartmentId,
        table.SupervisorId
      });

    }



    public DbSet<AppSettings> AppSettings { get; set; }

    public DbSet<County> Counties { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<DepartmentSupervisor> DepartmentSupervisors { get; set; }




    public DbSet<Interaction> Interactions { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Transfer> Transfers { get; set; }


  }
}
