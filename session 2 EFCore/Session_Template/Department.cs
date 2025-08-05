using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Template
{
    public class Department
    {

        public int DepartmentId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        
        public virtual List<Employee> Employees { get; set; } = new();

        public override string ToString()
        {
            return $"{this.Name}::{this.Description}::{this.DepartmentId}"
            ;
        }

    }
}
