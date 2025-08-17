using System.ComponentModel.DataAnnotations;

namespace Task_Manager.ApiService.DTOs
{
    public class UpdateTodoTaskDto
    {
        [Required]
        public int Id { get; set; }
        public string? Title { get; set; }
        public short? Score { get; set; }
        public string? Body { get; set; }
        public bool? IsDone { get; set; } 
    }
}
