using BloodDonorMVCWebApp.Repositories.Interfaces;

namespace BloodDonorMVCWebApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BloodDonorDbContext _context;
        public IBloodDonorRepository BloodDonorRepository { get; }

        public IDonationRepository DonationRepository { get; }

        public UnitOfWork(BloodDonorDbContext context, IBloodDonorRepository bloodDonorRepository, IDonationRepository donationRepository)
        {
            _context = context;
            BloodDonorRepository = bloodDonorRepository;
            DonationRepository = donationRepository;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            //int i = await _context.SaveChangesAsync();

            return await _context.SaveChangesAsync();
        }
    }
}
