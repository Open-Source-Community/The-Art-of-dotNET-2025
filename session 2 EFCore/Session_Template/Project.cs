using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Template
{
    public class Project
    {
        public int ProjId { get; set; }

        public string ProjectName { get; set; }

        public string? ProjectDesc { get; set; }

        public List<EmpProj>? EmpProjs { get; set; } = new();

        public override string ToString()
        {
            return $"{this.ProjId}::{this.ProjectName}::{this.ProjectDesc}";
        }
    }
}
