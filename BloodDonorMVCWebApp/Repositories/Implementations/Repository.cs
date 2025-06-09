using BloodDonorMVCWebApp.Data;
using BloodDonorMVCWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonorMVCWebApp.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public Repository(BloodDonorDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public void Add(T bloodDonor)
        {
            _dbSet.Add(bloodDonor);
        }

        public void Delete(T bloodDonor)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }

        public void Update(T bloodDonor)
        {
            _dbSet.Update(bloodDonor);
        }
    }
}
