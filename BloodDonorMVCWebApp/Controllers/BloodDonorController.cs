using AutoMapper;
using BloodDonorMVCWebApp.Models.Entities;
using BloodDonorMVCWebApp.Models.ViewModel;
using BloodDonorMVCWebApp.Services.Interfaces;
using BloodDonorMVCWebApp.Services.Model;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonorMVCWebApp.Controllers
{
    //[Route("BloodDonor/[controller]")]
    public class BloodDonorController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IBloodDonorService _bloodDonorService;
        private readonly IMapper _mapper;

        public BloodDonorController(IFileService fileService, IBloodDonorService bloodDonorService, IMapper mapper)
        {
            _fileService = fileService;
            _bloodDonorService = bloodDonorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string v_bloodGroup, string v_address, string v_contact, bool? eligibility)
        {
            var filter = new FilterDonorModel
            {
                bloodGroup = v_bloodGroup, address = v_address, contact = v_contact, isEligible = eligibility
            };
            var donors = await _bloodDonorService.GetFilteredBloodDonorAsync(filter);
            var donorViewModels = _mapper.Map<List<BloodDonorListViewModel>>(donors);

            return View(donorViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BloodDonorCreateViewModel donor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please correct the errors in the form.";
                return View(donor);
            }

            ViewBag.Message = "Donor Created Successfully";

            var donorEntity = new BloodDonorEntity
            {
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                DateOfBirth = donor.DateOfBirth,
                Email = donor.Email,
                BloodGroup = donor.BloodGroup,
                weight = donor.weight,
                LastDonationDate = donor.LastDonationDate,
                Address = donor.Address,
                IsAvailableForDonation = donor.IsAvailableForDonation,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null, // Set to null initially
                ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture)
            };

            //_context.BloodDonors.Add(donorEntity);
            //_context.SaveChanges();

            await _bloodDonorService.AddAsync(donorEntity);

            return RedirectToAction("Index");            
        }


        public async Task<IActionResult> Details(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);// _context.BloodDonors.FirstOrDefault(d => d.Id == id);
            if(donor == null)
            {
                return NotFound();
            }

            var donorViewModel = _mapper.Map<BloodDonorListViewModel>(donor);

            //var donorViewModel = new BloodDonorListViewModel
            //{
            //    Id = donor.Id,
            //    FullName = donor.FullName,
            //    ContactNumber = donor.ContactNumber,
            //    Age = DateTime.Now.Year - donor.DateOfBirth.Year,
            //    Email = donor.Email,
            //    BloodGroup = donor.BloodGroup.ToString(),
            //    LastDonationDate = donor.LastDonationDate.HasValue ? $"{(DateTime.Today - donor.LastDonationDate.Value).Days} days ago" : "Never",
            //    Address = donor.Address,
            //    CreatedAt = donor.CreatedAt.ToString("dd/MM/yyyy"),
            //    UpdatedAt = donor.UpdatedAt.HasValue ? donor.UpdatedAt.ToString() : "",
            //    ProfilePicture = donor.ProfilePicture,
            //    IsEligible = (donor.weight > 45 && donor.weight < 200) && (donor.LastDonationDate == null || (DateTime.Now - donor.LastDonationDate.Value).TotalDays >= 90)
            //};
            return View(donorViewModel);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id); //_context.BloodDonors.FirstOrDefault(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorViewModel = _mapper.Map<BloodDonorEditViewModel>(donor);
            //var donorViewModel = new BloodDonorEditViewModel
            //{
            //    Id = donor.Id,
            //    FullName = donor.FullName,
            //    ContactNumber = donor.ContactNumber,
            //    DateOfBirth=donor.DateOfBirth,
            //    Email = donor.Email,
            //    BloodGroup = donor.BloodGroup,
            //    LastDonationDate = donor.LastDonationDate,
            //    Address = donor.Address,
            //    weight = donor.weight,
            //    IsAvailableForDonation = donor.IsAvailableForDonation,
            //    ExistingProfilePicture = donor.ProfilePicture,                
            //};
            return View(donorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BloodDonorEditViewModel donor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please correct the errors in the form.";
                return View(donor);
            }                                  

            var updateDonorEntity = new BloodDonorEntity
            {
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                DateOfBirth = donor.DateOfBirth,
                Email = donor.Email,
                BloodGroup = donor.BloodGroup,
                weight = donor.weight,
                LastDonationDate = donor.LastDonationDate,
                Address = donor.Address,
                IsAvailableForDonation = donor.IsAvailableForDonation,
                UpdatedAt = DateTime.UtcNow,
                ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture)
            };

            var existingDonor = await _bloodDonorService.GetByIdAsync(donor.Id);// _context.BloodDonors.FirstOrDefaultAsync(d => d.Id == donor.Id);
            if (existingDonor == null)
            {
                throw new InvalidOperationException("Blood donor not found.");
            }

            existingDonor.FullName = updateDonorEntity.FullName;
            existingDonor.ContactNumber = updateDonorEntity.ContactNumber;
            existingDonor.DateOfBirth = updateDonorEntity.DateOfBirth;
            existingDonor.Email = updateDonorEntity.Email;
            existingDonor.BloodGroup = updateDonorEntity.BloodGroup;
            existingDonor.weight = updateDonorEntity.weight;
            existingDonor.LastDonationDate = updateDonorEntity.LastDonationDate;
            existingDonor.Address = updateDonorEntity.Address;
            existingDonor.IsAvailableForDonation = updateDonorEntity.IsAvailableForDonation;
            existingDonor.UpdatedAt = updateDonorEntity.UpdatedAt;
            existingDonor.ProfilePicture = updateDonorEntity.ProfilePicture ?? existingDonor.ProfilePicture; // Keep existing picture if not updated
                        
            await _bloodDonorService.UpdateAsync(existingDonor);
            ViewBag.Message = "Donor Updated Successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id) 
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorViewModel = _mapper.Map<BloodDonorListViewModel>(donor);
            //var donorViewModel = new BloodDonorListViewModel
            //{
            //    Id = donor.Id,
            //    FullName = donor.FullName,
            //    ContactNumber = donor.ContactNumber,
            //    Age = DateTime.Now.Year - donor.DateOfBirth.Year,
            //    Email = donor.Email,
            //    BloodGroup = donor.BloodGroup.ToString(),
            //    Address = donor.Address,
            //    LastDonationDate = donor.LastDonationDate.HasValue ? $"{(DateTime.Today - donor.LastDonationDate.Value).Days} days ago" : "Never",
            //    ProfilePicture = donor.ProfilePicture,
            //    IsEligible = (donor.weight > 45 && donor.weight < 200) && (!donor.LastDonationDate.HasValue || (DateTime.Now - donor.LastDonationDate.Value).TotalDays >= 90)
            //};
            return View(donorViewModel);
        }

        [ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmedAsync(int id)
        {
            await _bloodDonorService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
