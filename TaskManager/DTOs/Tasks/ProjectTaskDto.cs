namespace TaskManager.DTOs.Tasks
{
    public class ProjectTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; } = false;
        public int IdProject { get; set; }
    }
}
