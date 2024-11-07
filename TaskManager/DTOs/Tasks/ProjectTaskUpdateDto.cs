using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs.Tasks
{
    public class ProjectTaskUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must be maximum 100 characters.")]
        public string Name { get; set; }

        public bool Completed { get; set; } = false;


        [Required(ErrorMessage = "Project is required.")]
        public int IdProject { get; set; }
    }
}
