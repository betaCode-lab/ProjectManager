namespace TaskManager.Services.Core.Interfaces
{
    public interface ICommonService<T, TI, TU>
    {
        List<string> Errors { get; set; }
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI entity);
        Task<T> Update(int id, TU entity);
        Task<T> Delete(int id);
    }
}
