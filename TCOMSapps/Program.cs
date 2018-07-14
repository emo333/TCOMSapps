using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TCOMSapps.Data;

namespace TCOMSapps
{
	public class Program
	{
		public static void Main(string[] args)
		{
			

			//var host = BuildWebHost(args);
			//using (var scope = host.Services.CreateScope())
			//{
			//	var services = scope.ServiceProvider;
			//	var context = services.GetRequiredService<ApplicationDbContext>();
			//	var rolemanager = services.GetRequiredService<RoleManager<ApplicationRole>>();
			//	var usermanager = services.GetRequiredService<UserManager<ApplicationUser>>();

			//	var appInitializer = new AppInitializer(rolemanager,usermanager,context);
			//	appInitializer.Initialize();
			//}


			//host.Run();









			
			 BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
				WebHost.CreateDefaultBuilder(args)
						.UseStartup<Startup>()
						.Build();
	}
}
