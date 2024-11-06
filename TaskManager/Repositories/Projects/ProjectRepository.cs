using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Models.Core;
using TaskManager.Repositories.Core.Interfaces;

namespace TaskManager.Repositories.Projects
{
    public class ProjectRepository : IGenericRepository<Project>
    {
        private readonly ProjectManagerContext _context;

        public ProjectRepository(ProjectManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> Get() => await _context.Projects.ToListAsync();

        public async Task<Project> GetById(int id) => await _context.Projects.FindAsync(id);

        public async Task Add(Project entity) => await _context.Projects.AddAsync(entity);

        public void Update(Project entity)
        {
            _context.Projects.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Project entity) => _context.Projects.Remove(entity);

        public IEnumerable<Project> Search(Func<Project, bool> filter) => _context.Projects.Where(filter);

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
