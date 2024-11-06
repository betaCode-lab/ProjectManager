using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_project")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        
        [Column("id_user")]
        public int IdUser { get; set; }
    }
}
