namespace TaskManager.Services.Tasks.Interfaces
{
    public interface IProjectTaskService<T, TI, TU>
    {
        List<string> Errors { get; set; }
        IEnumerable<T> Get(int idProject);
        T GetById(int id, int idProject);
        Task<T> Add(TI entity);
        Task<T> Update(int id, TU entity);
        Task<T> Delete(int id, int idProject);
    }
}
