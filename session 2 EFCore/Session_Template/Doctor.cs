using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Template
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public string? Specialty { get; set; }  // nullable

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

}
