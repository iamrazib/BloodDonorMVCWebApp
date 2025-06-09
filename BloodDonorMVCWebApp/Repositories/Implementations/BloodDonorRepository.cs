using BloodDonorMVCWebApp.Data;
using BloodDonorMVCWebApp.Models.Entities;
using BloodDonorMVCWebApp.Repositories.Interfaces;

namespace BloodDonorMVCWebApp.Repositories.Implementations
{
    public class BloodDonorRepository: Repository<BloodDonorEntity>, IBloodDonorRepository
    {
        //private readonly BloodDonorDbContext _context;

        public BloodDonorRepository(BloodDonorDbContext context) : base(context)
        {
            //_context = context;
        }

        //public BloodDonorRepository(BloodDonorDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task Add(BloodDonorEntity bloodDonor)
        //{
        //    await _context.BloodDonors.AddAsync(bloodDonor);
        //}

        //public void Delete(BloodDonorEntity bloodDonor)
        //{
        //    _context.BloodDonors.Remove(bloodDonor);
        //}

        //public Task<IEnumerable<BloodDonorEntity>> FindAllAsync(Expression<Func<BloodDonorEntity, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<IEnumerable<BloodDonorEntity>> GetAllAsync()
        //{
        //    return await _context.BloodDonors.ToListAsync();
        //}

        //public async Task<BloodDonorEntity> GetByIdAsync(int id)
        //{
        //    return await _context.BloodDonors.FindAsync(id);
        //}

        //public async Task UpdateAsync(BloodDonorEntity bloodDonor)
        //{
        //    _context.BloodDonors.Update(bloodDonor);
        //}
    }
}
