using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCOMSapps.Data;

namespace TCOMSapps.Features.Account.entities
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string AppRoleId { get; set; }

        public DateTime CreatedDt { get; set; } = DateTime.Now;

        public ApplicationUser CreatedByUser { get; set; }

        public DateTime? DeletedDt { get; set; }

        [CanBeNull]
        public ApplicationUser DeletedByUser { get; set; }


        public int CountyId { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }

    }
}
