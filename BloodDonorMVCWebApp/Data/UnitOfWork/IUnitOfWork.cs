using BloodDonorMVCWebApp.Repositories.Interfaces;

namespace BloodDonorMVCWebApp.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBloodDonorRepository BloodDonorRepository { get; }
        IDonationRepository DonationRepository { get; }
        Task<int> SaveAsync();
    }
}
