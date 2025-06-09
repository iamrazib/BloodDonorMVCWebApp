using BloodDonorMVCWebApp.Models.Entities;
using BloodDonorMVCWebApp.Repositories.Interfaces;

namespace BloodDonorMVCWebApp.Repositories.Implementations
{
    public class DonationRepository : IDonationRepository
    {
        public void Add(Donation bloodDonor)
        {
            throw new NotImplementedException();
        }

        public void Delete(Donation bloodDonor)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Donation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Donation> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Donation bloodDonor)
        {
            throw new NotImplementedException();
        }
    }
}
