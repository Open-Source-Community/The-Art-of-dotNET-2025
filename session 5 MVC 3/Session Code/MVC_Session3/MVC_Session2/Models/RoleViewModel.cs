using System.ComponentModel.DataAnnotations;

namespace MVC_Session2.Models
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
