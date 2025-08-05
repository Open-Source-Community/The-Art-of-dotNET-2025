using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Template
{
    public class Employee
    {

        public int EmpSSN { get; set; } 
        
        public string Name { get; set; }
        
        public decimal Salary { get; set; }

        public int? Age { get; set; }
        
        public string? Address { get; set; }
        
        public string Anything { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Dept { get; set; }

        public List<EmpProj>? EmpProjs { get; set; } = new();

        public override string ToString()
        {
            return $"{this.EmpSSN}::{this.Name}::{this.Salary}::{this.Dept.Name}::{this.DepartmentId}";
        }
    }
}
