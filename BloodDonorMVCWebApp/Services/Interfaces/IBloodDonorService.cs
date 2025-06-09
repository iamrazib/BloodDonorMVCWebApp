using BloodDonorMVCWebApp.Models.Entities;
using BloodDonorMVCWebApp.Services.Model;

namespace BloodDonorMVCWebApp.Services.Interfaces
{
    public interface IBloodDonorService
    {
        Task<IEnumerable<BloodDonorEntity>> GetAllAsync();
        Task<List<BloodDonorEntity>> GetFilteredBloodDonorAsync(FilterDonorModel filter);
        Task<BloodDonorEntity?> GetByIdAsync(int id);
        Task AddAsync(BloodDonorEntity bloodDonor);
        Task UpdateAsync(BloodDonorEntity bloodDonor);
        Task DeleteAsync(int id);
    }
}
