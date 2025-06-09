using BloodDonorMVCWebApp.Data.UnitOfWork;
using BloodDonorMVCWebApp.Models.Entities;
using BloodDonorMVCWebApp.Models.ViewModel;
using BloodDonorMVCWebApp.Services.Interfaces;
using BloodDonorMVCWebApp.Services.Model;
using Microsoft.EntityFrameworkCore;

namespace BloodDonorMVCWebApp.Services.Implementations
{
    public class BloodDonorService : IBloodDonorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BloodDonorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(BloodDonorEntity bloodDonor)
        {
            _unitOfWork.BloodDonorRepository.Add(bloodDonor);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var donor = await _unitOfWork.BloodDonorRepository.GetByIdAsync(id);
            if(donor != null)
            {
                _unitOfWork.BloodDonorRepository.Delete(donor);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<BloodDonorEntity>> GetAllAsync()
        {
            return await _unitOfWork.BloodDonorRepository.GetAllAsync();
        }

        public async Task<BloodDonorEntity?> GetByIdAsync(int id)
        {
            return await _unitOfWork.BloodDonorRepository.GetByIdAsync(id);
        }

        public async Task<List<BloodDonorEntity>> GetFilteredBloodDonorAsync(FilterDonorModel filter)
        {
            var query = _unitOfWork.BloodDonorRepository.Query();

            if (!string.IsNullOrEmpty(filter.bloodGroup))
            {
                query = query.Where(d => d.BloodGroup.ToString() == filter.bloodGroup);
            }
            if (!string.IsNullOrEmpty(filter.address))
            {
                query = query.Where(d => d.Address != null && d.Address.Contains(filter.address));
            }
            if (!string.IsNullOrEmpty(filter.contact))
            {
                query = query.Where(d => d.ContactNumber == filter.contact);
            }

            //var donors = query.Select(d => new BloodDonorListViewModel
            //{
            //    Id = d.Id,
            //    FullName = d.FullName,
            //    ContactNumber = d.ContactNumber,
            //    Age = DateTime.Now.Year - d.DateOfBirth.Year,
            //    Email = d.Email,
            //    BloodGroup = d.BloodGroup.ToString(),
            //    LastDonationDate = d.LastDonationDate.HasValue ? $"{(DateTime.Today - d.LastDonationDate.Value).Days} days ago" : "Never",
            //    Address = d.Address,
            //    CreatedAt = d.CreatedAt.ToString("dd/MM/yyyy"),
            //    UpdatedAt = d.UpdatedAt.HasValue ? d.UpdatedAt.ToString() : "",
            //    ProfilePicture = d.ProfilePicture,
            //    IsEligible = (d.weight > 45 && d.weight < 200) && (d.LastDonationDate == null || (DateTime.Now - d.LastDonationDate.Value).TotalDays >= 90)
            //}).ToList();

            //if (filter.isEligible.HasValue)
            //{
            //    donors = donors.Where(x => x.IsEligible == filter.isEligible).ToList();
            //}

            return await query.ToListAsync();

            //return donors;
        }

        public async Task UpdateAsync(BloodDonorEntity bloodDonor)
        {
            _unitOfWork.BloodDonorRepository.Update(bloodDonor);
            await _unitOfWork.SaveAsync();
        }

        public static bool IsEligible(BloodDonorEntity bloodDonor)
        {
            if (bloodDonor.weight <= 45 || bloodDonor.weight >= 200)
                return false;
            if (bloodDonor.LastDonationDate.HasValue)
            {
                var daysSinceLastDonation = (DateTime.Now - bloodDonor.LastDonationDate.Value).TotalDays;
                return daysSinceLastDonation >= 90;
            }
            return true;
        }

    }
}
