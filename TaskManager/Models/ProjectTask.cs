using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    [Table("Tasks")]
    public class ProjectTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_task")]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }

        [Column("id_project")]
        public int IdProject { get; set; }
    }
}
