namespace TaskManager.Services.Projects.Interfaces
{
    public interface IProjectService<T, TI, TU>
    {
        List<string> Errors { get; set; }
        IEnumerable<T> Get(int idUser);
        T GetById(int id, int idUser);
        Task<T> Add(TI entity);
        Task<T> Update(int id, TU entity);
        Task<T> Delete(int id, int idUser);
    }
}
