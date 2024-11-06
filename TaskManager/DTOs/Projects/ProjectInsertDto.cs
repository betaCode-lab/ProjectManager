using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs.Projects
{
    public class ProjectInsertDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(20, ErrorMessage = "Name must be maximum 20 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(100, ErrorMessage = "Description must be maximum 100 characters.")]
        public string Description { get; set; }

        public bool Active { get; set; } = true;

        [Required(ErrorMessage = "User is required.")]
        public int IdUser { get; set; }
    }
}
