
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Models.Core;
using TaskManager.Repositories.Core.Interfaces;

namespace TaskManager.Repositories
{
    public class UserRepository : IGenericRepository<User>
    {
        private readonly ProjectManagerContext _context;

        public UserRepository(ProjectManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> Get() => await _context.Users.ToListAsync();

        public async Task<User> GetById(int id) => await _context.Users.FindAsync(id);

        public async Task Add(User user) => await _context.Users.AddAsync(user);

        public void Update(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(User user) => _context.Users.Remove(user);

        public IEnumerable<User> Search(Func<User, bool> filter) => _context.Users.Where(filter).ToList();

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
