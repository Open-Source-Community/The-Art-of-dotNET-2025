using System.ComponentModel.DataAnnotations;

namespace Task_Manager.ApiService.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
