using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task_Manager.Web.Data
{
    public class User
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public List<TodoTask> Tasks { get; set; }
        public bool IsAdmin { get; set; }
    }
}
