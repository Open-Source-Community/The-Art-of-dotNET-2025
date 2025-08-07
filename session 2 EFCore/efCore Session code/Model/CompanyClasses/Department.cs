using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efCore.Model.CompanyClasses
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentId { get; set; }
        [Required]
        [MaxLength(40)]
        public string? Name { get; set; }
        [MaxLength(40)]
        public string? Description { get; set; }

        #region Navigation Property
        public virtual List<Employee> Employees { get; set; } = new();
        #endregion

        public override string ToString()
        {
            return $"{Name}::{Description}::{DepartmentId}"
            ;
        }

    }
}
