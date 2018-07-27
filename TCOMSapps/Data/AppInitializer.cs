using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TCOMSapps.Features.OOSTitles.entities;
using TCOMSapps.Services.AppSettings;

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
          await usermanager.AddToRoleAsync(await usermanager.FindByNameAsync(UserName), "OOSTitles User");
        }

        // TODO: if no county exist, create Default county

        //create default county
        if (await context.Counties.CountAsync() == 0)
        {
          await context.Counties.AddRangeAsync(
              new County
              {
                Name = "NASSAU",
                Active = true,
                Theme = 1,
                DefaultLocation = 1,
                SharedData = false,
                OosTitleLetterTaxCollectorName = "Don Knotts",
                OosTitleLetterAddressLine1 = "1234 Some Road",
                OosTitleLetterAddressLine2 = "Suite 99",
                OosTitleLetterCity = "Greenbow",
                OosTitleLetterState = "AL",
                OosTitleLetterZip = "99999-9999",
                OosTitleEmailAddress = "ms@tc.com",
                OosTitleLetterWebsite = "www.tc.com",
                OosTitleLetterPhoneNumbers = "(999) 555-1212 - (800) 555-1234",
                OosTitleLetterMsPhoneNumber = "(555) 555-1213",
                OosTitlesCopyEmailAddress = "records@tc.com"
              }
          );
          context.SaveChanges();
        }

        //create default app settings
        if (await context.AppSettings.CountAsync() == 0)
        {
          await context.AppSettings.AddRangeAsync(
            new AppSettings
            {
              SmtpHost = "172.0.0.1",
              SmtpPort = 25
            }
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
                            new Tuple<string, int, int>("Suzy", 3, 1)
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

          // ## Add demo titles ########################################

          // get user to use for titles
          var employee = await usermanager.Users.FirstOrDefaultAsync(u => u.UserName == "mandy@nassautaxes.com");
          Random rnd = new Random();

          var titleItems = new List<Title>()
          {
              new Title()
              {
                CountyId = 1,
                CustFName = "John",
                CustLName = "Hanks",
                CustAddr1 = "1234 Dug Drive",
                CustCity = "Kirkland",
                CustState = "FL",
                CustZip = "32000",
                CustEmail = "",
                CustPhone = "555-3333",
                EntryDt = DateTime.Now,
                InitialLocation = rnd.Next(1,4),
                RecDt = DateTime.Now.AddDays(-rnd.Next(1,90)),
                TitleState = "GA",
                UserId = employee.Id.ToString(),
                VehMake = "Ford",
                VehModel = "2D",
                VehYr = rnd.Next(1980,2018),
                Vin = rnd.Next(10000000,999999999).ToString()
              },
            new Title()
            {
              CountyId = 1,
              CustFName = "Mary",
              CustLName = "Moore",
              CustAddr1 = "345 Lopez Drive",
              CustCity = "Carlsome",
              CustState = "FL",
              CustZip = "32300",
              CustEmail = "mmore@someplace.com",
              CustPhone = "555-3333",
              EntryDt = DateTime.Now,
              InitialLocation = rnd.Next(1,4),
              RecDt = DateTime.Now.AddDays(-rnd.Next(1,90)),
              TitleState = "CA",
              UserId = employee.Id.ToString(),
              VehMake = "Chevy",
              VehModel = "TK",
              VehYr = rnd.Next(1980,2018),
              Vin = rnd.Next(10000000,999999999).ToString()
            },
            new Title()
            {
              CountyId = 1,
              CustFName = "Frank",
              CustLName = "Andbeans",
              CustAddr1 = "14555 Jalit Drive",
              CustCity = "Lorang",
              CustState = "FL",
              CustZip = "32004",
              CustEmail = "",
              CustPhone = "555-3333",
              EntryDt = DateTime.Now,
              InitialLocation = rnd.Next(1,4),
              RecDt = DateTime.Now.AddDays(-rnd.Next(1,90)),
              TitleState = "AL",
              UserId = employee.Id.ToString(),
              VehMake = "Isuzu",
              VehModel = "4D",
              VehYr = rnd.Next(1980,2018),
              Vin = rnd.Next(10000000,999999999).ToString()
            },
            new Title()
            {
              CountyId = 1,
              CustFName = "Ben",
              CustLName = "Shapiro",
              CustAddr1 = "999 Shtick Road",
              CustCity = "Belvue",
              CustState = "OR",
              CustZip = "92040",
              CustEmail = "",
              CustPhone = "555-3333",
              EntryDt = DateTime.Now,
              InitialLocation = rnd.Next(1,4),
              RecDt = DateTime.Now.AddDays(-rnd.Next(1,90)),
              TitleState = "OR",
              UserId = employee.Id.ToString(),
              VehMake = "HD",
              VehModel = "MT",
              VehYr = rnd.Next(1980,2018),
              Vin = rnd.Next(10000000,999999999).ToString()
            },
            new Title()
            {
              CountyId = 1,
              CustFName = "Bill",
              CustLName = "Gates",
              CustAddr1 = "95 Windows Circle",
              CustCity = "Redmond",
              CustState = "WA",
              CustZip = "89000",
              CustEmail = "",
              CustPhone = "555-3333",
              EntryDt = DateTime.Now,
              InitialLocation = rnd.Next(1,4),
              RecDt = DateTime.Now.AddDays(-rnd.Next(1,90)),
              TitleState = "WA",
              UserId = employee.Id.ToString(),
              VehMake = "Toyota",
              VehModel = "Prius",
              VehYr = rnd.Next(1980,2018),
              Vin = rnd.Next(10000000,999999999).ToString()
            },
            new Title()
            {
              CountyId = 1,
              CustFName = "Johny",
              CustLName = "Cash",
              CustAddr1 = "77652 Woods Drive",
              CustCity = "Daytona",
              CustState = "FL",
              CustZip = "23000",
              CustEmail = "",
              CustPhone = "555-3333",
              EntryDt = DateTime.Now,
              InitialLocation = rnd.Next(1,4),
              RecDt = DateTime.Now.AddDays(-rnd.Next(1,90)),
              TitleState = "NC",
              UserId = employee.Id.ToString(),
              VehMake = "Dodge",
              VehModel = "PU",
              VehYr = rnd.Next(1980,2018),
              Vin = rnd.Next(10000000,999999999).ToString()
            }
          };

          foreach (var title in titleItems)
          {
            var title2 = (await context.Titles.AddAsync(title));

            // add transfer for title
            var transfer = new Transfer()
            {
              TitleId = title2.Entity.Id,
              LocationId = title.InitialLocation,
              TransferRequestUserId = employee.Id.ToString(),
              TransferRequestedDt = title.RecDt,
              IsInRoute = true,
              InRouteDt = title.RecDt,
              InRouteByUserId = employee.Id.ToString(),
              IsReceived = true,
              ReceivedDt = title.RecDt,
              ReceivedByUserId = employee.Id.ToString()
            };
            context.Transfers.Add(transfer);

            // add interaction for title
            var interaction = new Interaction()
            {
              TitleId = title2.Entity.Id,
              InteractionDt = title.RecDt,
              InteractionUserId = title.UserId,
              InteractionType = "Phone Call",
              VehYr = title.VehYr,
              VehMake = title.VehMake,
              VehModel = title.VehModel,
              Vin = title.Vin,
              TitleState = title.TitleState,
              IsNew = title.NewVeh,
              CustFName = title.CustFName,
              CustLName = title.CustLName,
              CustAddr1 = title.CustAddr1,
              CustAddr2 = title.CustAddr2,
              CustCity = title.CustCity,
              CustState = title.CustState,
              CustZip = title.CustZip,
              CustPhone = title.CustPhone,
              CustPhone2 = title.CustPhone2,
              CustFName2 = title.CustFName2,
              CustLName2 = title.CustLName2,
              CustFName3 = title.CustFName3,
              CustLName3 = title.CustLName3,
              TitleRecievedFromType = Interaction.TitleReceivedFromTypes.Lienholder,
              ReceivedDt = title.RecDt,
              Notes = "Left Voicemail asking Customer to call office."
            };
            context.Interactions.Add(interaction);
          }
          await context.SaveChangesAsync();

          var titwithwarning = new Title()
          {
            CountyId = 1,
            CustFName = "Laney",
            CustLName = "Watkins",
            CustAddr1 = "555 Tine Drive",
            CustCity = "York",
            CustState = "FL",
            CustZip = "33400",
            CustEmail = "",
            CustPhone = "555-3333",
            EntryDt = DateTime.Now.AddDays(-50),
            InitialLocation = rnd.Next(1, 4),
            RecDt = DateTime.Now.AddDays(-50),
            TitleState = "TN",
            UserId = employee.Id.ToString(),
            VehMake = "Yugo",
            VehModel = "2D",
            VehYr = rnd.Next(1980, 2018),
            Vin = rnd.Next(10000000, 999999999).ToString()
          };

          var title3 = (await context.Titles.AddAsync(titwithwarning));

          // add transfer for title
          var transferwarning = new Transfer()
          {
            TitleId = title3.Entity.Id,
            LocationId = titwithwarning.InitialLocation,
            TransferRequestUserId = employee.Id.ToString(),
            TransferRequestedDt = titwithwarning.RecDt,
            IsInRoute = true,
            InRouteDt = titwithwarning.RecDt,
            InRouteByUserId = employee.Id.ToString(),
            IsReceived = true,
            ReceivedDt = titwithwarning.RecDt,
            ReceivedByUserId = employee.Id.ToString()
          };
          context.Transfers.Add(transferwarning);

          // add warning interaction for title
          var interactionwarning = new Interaction()
          {
            TitleId = title3.Entity.Id,
            InteractionDt = titwithwarning.RecDt,
            InteractionUserId = titwithwarning.UserId,
            InteractionType = "Initial Notification Letter",
            VehYr = titwithwarning.VehYr,
            VehMake = titwithwarning.VehMake,
            VehModel = titwithwarning.VehModel,
            Vin = titwithwarning.Vin,
            TitleState = titwithwarning.TitleState,
            IsNew = titwithwarning.NewVeh,
            CustFName = titwithwarning.CustFName,
            CustLName = titwithwarning.CustLName,
            CustAddr1 = titwithwarning.CustAddr1,
            CustAddr2 = titwithwarning.CustAddr2,
            CustCity = titwithwarning.CustCity,
            CustState = titwithwarning.CustState,
            CustZip = titwithwarning.CustZip,
            CustPhone = titwithwarning.CustPhone,
            CustPhone2 = titwithwarning.CustPhone2,
            CustFName2 = titwithwarning.CustFName2,
            CustLName2 = titwithwarning.CustLName2,
            CustFName3 = titwithwarning.CustFName3,
            CustLName3 = titwithwarning.CustLName3,
            TitleRecievedFromType = Interaction.TitleReceivedFromTypes.Lienholder,
            ReceivedDt = titwithwarning.RecDt,
          };
          context.Interactions.Add(interactionwarning);

          // add warning interaction for title
          var interactionwarning2 = new Interaction()
          {
            TitleId = title3.Entity.Id,
            InteractionDt = titwithwarning.RecDt.AddDays(31),
            InteractionUserId = titwithwarning.UserId,
            InteractionType = "Warning Letter",
            VehYr = titwithwarning.VehYr,
            VehMake = titwithwarning.VehMake,
            VehModel = titwithwarning.VehModel,
            Vin = titwithwarning.Vin,
            TitleState = titwithwarning.TitleState,
            IsNew = titwithwarning.NewVeh,
            CustFName = titwithwarning.CustFName,
            CustLName = titwithwarning.CustLName,
            CustAddr1 = titwithwarning.CustAddr1,
            CustAddr2 = titwithwarning.CustAddr2,
            CustCity = titwithwarning.CustCity,
            CustState = titwithwarning.CustState,
            CustZip = titwithwarning.CustZip,
            CustPhone = titwithwarning.CustPhone,
            CustPhone2 = titwithwarning.CustPhone2,
            CustFName2 = titwithwarning.CustFName2,
            CustLName2 = titwithwarning.CustLName2,
            CustFName3 = titwithwarning.CustFName3,
            CustLName3 = titwithwarning.CustLName3,
            TitleRecievedFromType = Interaction.TitleReceivedFromTypes.Lienholder,
            ReceivedDt = titwithwarning.RecDt,
          };
          context.Interactions.Add(interactionwarning2);
          await context.SaveChangesAsync();
        }
      }
    }
  }
}