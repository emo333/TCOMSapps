using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TCOMSapps.Data
{

        public class ApplicationBaseContext<TContext> : IdentityDbContext<ApplicationUser, ApplicationRole, Guid> where TContext : DbContext
        {
            public ApplicationBaseContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {

            }
        }

}
