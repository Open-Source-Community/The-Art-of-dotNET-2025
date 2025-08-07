using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efCore.Model.CompanyClasses
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjId { get; set; }

        [MaxLength(40)]
        public string ProjectName { get; set; }
        [MaxLength(100)]
        public string? ProjectDesc { get; set; }

        #region Navigation Property
        public virtual List<Employee>? employees{ get; set; } = new List<Employee>();
        #endregion

        public override string ToString()
        {
            return $"{ProjId}::{ProjectName}::{ProjectDesc}";
        }
    }
}
