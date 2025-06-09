using BloodDonorMVCWebApp.Models.Entities;
using System.Linq.Expressions;

namespace BloodDonorMVCWebApp.Repositories.Interfaces
{
    public interface IBloodDonorRepository: IRepository<BloodDonorEntity>
    {
        //Task<IEnumerable<BloodDonorEntity>> GetAllAsync();
        //Task<BloodDonorEntity> GetByIdAsync(int id);
        //Task<IEnumerable<BloodDonorEntity>> FindAllAsync(Expression<Func<BloodDonorEntity,bool>> predicate);
        //Task Add(BloodDonorEntity bloodDonor);
        //Task UpdateAsync(BloodDonorEntity bloodDonor);
        //void Delete(BloodDonorEntity bloodDonor);
    }
}
