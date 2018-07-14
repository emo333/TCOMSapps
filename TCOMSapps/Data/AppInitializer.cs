using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Data
{
  public class AppInitializer
  {
    private string _password;

    public string UserName { get; set; } = "a@a.com";

    // Seeds database with default data if no data exists per table
    public async void Initialize(IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var rolemanager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var usermanager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();


        //create database schema if none exists
        context.Database.EnsureCreated();

        //Create the default Admin account 
        if (usermanager.Users.FirstOrDefaultAsync(u => u.UserName == UserName).Result == null)
        {
          string username = UserName;
          _password = "1234Abcd!";
          await usermanager.CreateAsync(new ApplicationUser
          {
            UserName = username,
            Email = username,
            EmailConfirmed = true,
            FirstName = "SuperUser",
            CountyId = 1
          }, _password);
          await context.SaveChangesAsync();
        }

        var superuser = usermanager.Users.FirstOrDefaultAsync(u => u.UserName == UserName).Result;

        // Create Default Roles if they dont exist 
        if (!await rolemanager.RoleExistsAsync("TCOMS Apps Administrator"))
        {
          await rolemanager.CreateAsync(new ApplicationRole("TCOMS Apps Administrator"));
          (await rolemanager.FindByNameAsync("TCOMS Apps Administrator")).CountyId = 1;
          await usermanager.AddToRoleAsync(await usermanager.FindByNameAsync(UserName), "TCOMS Apps Administrator");
        }

        if (!await rolemanager.RoleExistsAsync("OOSTitles Admin"))
        {
          await rolemanager.CreateAsync(new ApplicationRole("OOSTitles Admin"));
          (await rolemanager.FindByNameAsync("OOSTitles Admin")).CountyId = 1;
          await rolemanager.CreateAsync(new ApplicationRole("OOSTitles User"));
          (await rolemanager.FindByNameAsync("OOSTitles User")).CountyId = 1;
          await usermanager.AddToRoleAsync(await usermanager.FindByNameAsync(UserName), "OOSTitles Admin");
        }

        // TODO: if no county exist, create Default county

        //create default county
        if (await context.Counties.CountAsync() == 0)
        {
          await context.Counties.AddRangeAsync(
              new County { Name = "NASSAU COUNTY", Active = true, Theme = 1, DefaultLocation = 1, SharedData = false }
          );
          context.SaveChanges();
        }

        //create locations
        if (await context.Locations.CountAsync() == 0)
        {
          await context.Locations.AddRangeAsync(
              new Location { Name = "MAIN OFFICE", CountyId = 1, Default = true, Color = "white" },
              new Location() { Name = "CALLAHAN", CountyId = 1, Default = false, Color = "white" },
              new Location() { Name = "HCH", CountyId = 1, Default = false, Color = "white" },
              new Location() { Name = "HILLIARD", CountyId = 1, Default = false, Color = "white" }
          );
          context.SaveChanges();
        }

        //create supervisor user
        const string supervisorname = "Erica";
        if (usermanager.Users.FirstOrDefaultAsync(u => u.UserName == supervisorname + "@nassautaxes.com").Result == null)
        {

          await usermanager.CreateAsync(new ApplicationUser
          {
            UserName = supervisorname + "@nassautaxes.com",
            Email = supervisorname + "@nassautaxes.com",
            EmailConfirmed = true,
            FirstName = supervisorname,
            CountyId = 1
          }, _password);
          await context.SaveChangesAsync();

          //create employee users with a supervisor
          var supervisor = usermanager.Users.FirstOrDefaultAsync(u => u.UserName == supervisorname + "@nassautaxes.com").Result;
          var employeeItems = new List<Tuple<string, int, int>>
                        {
                            new Tuple<string, int, int>("Mandy", 1, 3),
                            new Tuple<string, int, int>("Carey", 2, 1),
                            new Tuple<string, int, int>("Suzy", 3, 1),
                            new Tuple<string, int, int>("Felicia", 4, 2),
                            new Tuple<string, int, int>("Kelly", 5, 1),
                            new Tuple<string, int, int>("Denora", 6, 1),
                            new Tuple<string, int, int>("Julie", 7, 2),
                            new Tuple<string, int, int>("Valerie", 8, 1),
                            new Tuple<string, int, int>("Bill", 9, 1),
                            new Tuple<string, int, int>("Kim", 10, 1),
                            new Tuple<string, int, int>("Donna", 11, 3),
                            new Tuple<string, int, int>("Janet", 12, 1),
                            new Tuple<string, int, int>("Cecilia", 12, 1),
                            new Tuple<string, int, int>("Sherry", 22, 1),
                            new Tuple<string, int, int>("Serena", 23, 1),
                            new Tuple<string, int, int>("Tina", 25, 1),
                            new Tuple<string, int, int>("John", 16, 3),
                            new Tuple<string, int, int>("Dot", 17, 2),
                            new Tuple<string, int, int>("Lisa", 18, 1),
                            new Tuple<string, int, int>("Venus", 19, 1),
                            new Tuple<string, int, int>("Christine", 26, 4),
                            new Tuple<string, int, int>("Bonnie", 21, 1),
                            new Tuple<string, int, int>("Jackie", 15, 1),
                            new Tuple<string, int, int>("Jennifer", 14, 1)
                        };
          foreach (var employeeItem in employeeItems)
          {
            await usermanager.CreateAsync(new ApplicationUser
            {
              UserName = employeeItem.Item1 + "@nassautaxes.com",
              Email = employeeItem.Item1 + "@nassautaxes.com",
              EmailConfirmed = true,
              FirstName = employeeItem.Item1,
              SupervisorId = supervisor.Id,
              CountyId = 1,
              DefaultShiftId = employeeItem.Item3,
              DefaultStationId = employeeItem.Item2
            }, _password);
            await context.SaveChangesAsync();

            await usermanager.AddToRoleAsync(await usermanager.FindByNameAsync(employeeItem.Item1 + "@nassautaxes.com"),
                "OOSTitles User");
          }
        }
      }
    }
  }
}