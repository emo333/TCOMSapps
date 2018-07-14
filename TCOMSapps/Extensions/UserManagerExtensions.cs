using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TCOMSapps.Data;

namespace TCOMSapps.Extensions
{
	public static class UserManagerExtensions
	{
		public static async Task<IdentityResult> SetFirstNameAsync(this UserManager<ApplicationUser> um, ApplicationUser user, string firstname)
		{
			user.FirstName = firstname;
			var x = await um.UpdateAsync(user);
			return x;
		}

		public static async Task<IdentityResult> SetLastNameAsync(this UserManager<ApplicationUser> um, ApplicationUser user, string lastname)
		{
			user.LastName = lastname;
			var x = await um.UpdateAsync(user);
			return x;
		}

		public static async Task<IdentityResult> SetCountyIdAsync(this UserManager<ApplicationUser> um, ApplicationUser user, int countyid)
		{
			user.CountyId = countyid;
			var x = await um.UpdateAsync(user);
			return x;
		}

		public static async Task<IdentityResult> SetSupervisorIdAsync(this UserManager<ApplicationUser> um, ApplicationUser user, Guid? supervisorid)
		{
			user.SupervisorId = supervisorid;
			var x = await um.UpdateAsync(user);
			return x;
		}

		public static async Task<IdentityResult> SetDefaultStationIdAsync(this UserManager<ApplicationUser> um, ApplicationUser user, int defaultstationid)
		{
			user.DefaultStationId = defaultstationid;
			var x = await um.UpdateAsync(user);
			return x;
		}

		public static async Task<IdentityResult> SetDefaultShiftIdAsync(this UserManager<ApplicationUser> um, ApplicationUser user, int defaultshiftid)
		{
			user.DefaultShiftId = defaultshiftid;
			var x = await um.UpdateAsync(user);
			return x;
		}

		public static async Task<string> GetFirstLastNameById(this UserManager<ApplicationUser> um, Guid? userid)
		{
			var user = await um.Users.FirstOrDefaultAsync(u => u.Id == userid);
			var userName = user.FirstName + " " + user.LastName;
			return userName;
		}

	}
}
