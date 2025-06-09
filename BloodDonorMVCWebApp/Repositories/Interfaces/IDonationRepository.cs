using BloodDonorMVCWebApp.Models.Entities;
using System.Linq.Expressions;

namespace BloodDonorMVCWebApp.Repositories.Interfaces
{
    public interface IDonationRepository
    {
        Task<IEnumerable<Donation>> GetAllAsync();
        Task<Donation> GetByIdAsync(int id);
        void Add(Donation bloodDonor);
        void Update(Donation bloodDonor);
        void Delete(Donation bloodDonor);
    }
}
