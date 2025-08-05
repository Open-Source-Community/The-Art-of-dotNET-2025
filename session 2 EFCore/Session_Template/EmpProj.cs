using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Template
{
    public class EmpProj
    {
        public int EmpId { get; set; }
        
        public int ProjId { get; set; }

        public int Hours { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Project Project { get; set; }

    }
}
