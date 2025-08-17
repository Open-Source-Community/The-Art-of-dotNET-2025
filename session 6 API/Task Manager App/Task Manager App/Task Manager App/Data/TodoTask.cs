using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task_Manager.Web.Data
{
    public class TodoTask
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-generates Id
        public int Id { get; set; }
        public string Title { get; set; }
        public short Score { get; set; }
        public string Body { get; set; }
        public bool IsDone { get; set; }
        public int CreatorId { get; set; }
        required public User Creator { get; set; }
    }
}
