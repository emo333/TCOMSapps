#region USINGS
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rotativa.AspNetCore;
using TCOMSapps.Data;
using TCOMSapps.Data.Repositories;
using TCOMSapps.Data.Repositories.Interfaces;
using TCOMSapps.Services;

#endregion

//added for Resharper to stop being annoying with feature folders
#region RESHARPER HACKS
[assembly: AspMvcViewLocationFormat(@"~\Features\{1}\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Shared\{0}.cshtml")]

[assembly: AspMvcViewLocationFormat(@"~\Features\OOSTitles\{1}\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\OOSTitles\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\OOSTitles\Dashboard\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\OOSTitles\Shared\{0}.cshtml")]

[assembly: AspMvcViewLocationFormat(@"~\Features\Admin\{1}\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Admin\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Admin\Dashboard\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Admin\Shared\{0}.cshtml")]

[assembly: AspMvcViewLocationFormat(@"~\Features\Checklists\{1}\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Checklists\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Checklists\Shared\{0}.cshtml")]


[assembly: AspMvcViewLocationFormat(@"~\Features\EmployeeSchedule\{1}\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\EmployeeSchedule\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\EmployeeSchedule\Shared\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\EmployeeSchedule\Dashboard\{1}\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\EmployeeSchedule\Dashboard\{0}.cshtml")]

[assembly: AspMvcViewLocationFormat(@"~\Features\Timesheets\{1}\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Timesheets\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Timesheets\Dashboard\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Timesheets\Timesheets\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Timesheets\Shared\{0}.cshtml")]

[assembly: AspMvcAreaViewLocationFormat(@"~\Areas\{2}\{1}\{0}.cshtml")]
[assembly: AspMvcAreaViewLocationFormat(@"~\Areas\{2}\{0}.cshtml")]
[assembly: AspMvcAreaViewLocationFormat(@"~\Areas\{2}\Shared\{0}.cshtml")]
#endregion

namespace TCOMSapps
{
  public class Startup
  {
    public Startup(IConfiguration configuration) { Configuration = configuration; }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      // Add Identity services
      services.AddIdentity<ApplicationUser, ApplicationRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();
      //services.AddAuthorization();


      // Add db repos services
      services.AddTransient<IChecklistsRepository, ChecklistsRepository>();


      // Add application services.
      services.AddTransient<IEmailSender, EmailSender>();


      // Add db context services
      //services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

      services.AddMvc()
          .AddFeatureFolders();

      // Add Database Initializer service
      services.AddScoped<AppInitializer>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppInitializer appinitializer)
    {
      if (env.IsDevelopment())
      {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
        appinitializer.Initialize(app);
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseMvcWithDefaultRoute();

      RotativaConfiguration.Setup(env);
    }
  }
}
