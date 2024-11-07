using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Models.Core;
using TaskManager.Repositories.Core.Interfaces;

namespace TaskManager.Repositories.Tasks
{
    public class ProjectTaskRepository : IGenericRepository<ProjectTask>
    {
        private readonly ProjectManagerContext _context;

        public ProjectTaskRepository(ProjectManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectTask>> Get() => await _context.Tasks.ToListAsync();

        public async Task<ProjectTask> GetById(int id) => await _context.Tasks.FindAsync(id);

        public async Task Add(ProjectTask entity) => await _context.Tasks.AddAsync(entity);

        public void Update(ProjectTask entity)
        {
            _context.Tasks.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(ProjectTask entity) => _context.Tasks.Remove(entity);

        public IEnumerable<ProjectTask> Search(Func<ProjectTask, bool> filter) => _context.Tasks.Where(filter);

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                _context.Database.BeginTransaction().Rollback();
            }
        }
    }
}
