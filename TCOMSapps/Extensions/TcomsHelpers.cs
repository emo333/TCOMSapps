using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCOMSapps.Data;

namespace TCOMSapps.Extensions
{
	public class TcomsHelpers
	{
		private readonly ApplicationDbContext _ctx;
		private readonly UserManager<ApplicationUser> _userManager;

		public TcomsHelpers(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
		{
			_ctx = ctx;
			_userManager = userManager;
		}

		public TcomsHelpers(ApplicationDbContext ctx)
		{
			_ctx = ctx;
		}


		public SelectList GetLocationSelectList()
		{
			var lsl = new SelectList(_ctx.Locations, "Id", "Name");
			foreach (var l in _ctx.Locations)
			{
				var s = new SelectListItem() { Value = l.Id.ToString(), Text = l.Name };
				lsl.Append(s);
			}
			return lsl;
		}


		public SelectList GetCountySelectList()
		{
			var csl = new SelectList(_ctx.Counties, "Id", "Name");
			foreach (var c in _ctx.Counties)
			{
				var s = new SelectListItem() { Value = c.Id.ToString(), Text = c.Name };
				csl.Append(s);
			}
			return csl;
		}


		public SelectList GetDepartmentSelectList()
		{
			var dsl = new SelectList(_ctx.Departments, "Id", "Name");
			foreach (var d in _ctx.Departments)
			{
				var s = new SelectListItem() { Value = d.Id.ToString(), Text = d.Name };
				dsl.Append(s);
			}
			return dsl;
		}

		public async Task<List<ApplicationUser>> GetEmployeesOfSupervisor(ApplicationUser supervisor)
		{
			return await _userManager.Users.Where(u => u.SupervisorId == supervisor.Id).ToListAsync();
		}

	}
}
