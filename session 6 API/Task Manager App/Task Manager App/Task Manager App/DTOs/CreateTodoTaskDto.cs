using System.ComponentModel.DataAnnotations;

namespace Task_Manager.ApiService.DTOs
{
    public class CreateTodoTaskDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public short Score { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public bool IsDone { get; set; } 
    }
}
