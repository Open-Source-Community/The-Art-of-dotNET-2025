using System.ComponentModel.DataAnnotations;

namespace MVC_Session2.Models
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string confirmPassword { get; set; }

        public string Address { get; set; }
    }
}
