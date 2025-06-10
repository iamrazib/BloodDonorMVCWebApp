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
            var donorEntity= _mapper.Map<BloodDonorEntity>(donor);
            donorEntity.ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture);

            await _bloodDonorService.AddAsync(donorEntity);

            return RedirectToAction("Index");            
        }


        public async Task<IActionResult> Details(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if(donor == null)
            {
                return NotFound();
            }

            var donorViewModel = _mapper.Map<BloodDonorListViewModel>(donor);
            return View(donorViewModel);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id); 
            if (donor == null)
            {
                return NotFound();
            }
            var donorViewModel = _mapper.Map<BloodDonorEditViewModel>(donor);            
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

            var existingDonor = await _bloodDonorService.GetByIdAsync(donor.Id);
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
