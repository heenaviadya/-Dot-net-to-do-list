using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using To_do_list.Areas.Identity.Data;

namespace To_do_list.Models
{
    public class ToDoList
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [DisplayName("Task Name")]
        public String? TaskName { get; set; }

        [Required]
        public string? Description { get; set; }

       
        [DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; } 

        [Required]
        public DateTime? Deadline { get; set; } = new DateTime();

        [Required]
        public string? Status { get; set; }

        public string? Id { get; set; }
        public To_do_listUser? User { get; set; }
    }
}
