using BloodDonorMVCWebApp.Data;
using BloodDonorMVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonorMVCWebApp.Controllers
{
    //[Route("BloodDonor/[controller]")]
    public class BloodDonorController : Controller
    {
        private readonly BloodDonorDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BloodDonorController(BloodDonorDbContext context, IWebHostEnvironment env) {
            _context = context;
            _env = env;
        }

        public IActionResult Index(string v_bloodGroup, string v_address, string v_contact)
        {
            IQueryable<BloodDonorEntity> query = _context.BloodDonors;

            if (!string.IsNullOrEmpty(v_bloodGroup))
            {
                query = query.Where(d => d.BloodGroup.ToString() == v_bloodGroup);
            }
            if (!string.IsNullOrEmpty(v_address))
            {
                query = query.Where(d => d.Address != null && d.Address.Contains(v_address));
            }
            if (!string.IsNullOrEmpty(v_contact))
            {
                query = query.Where(d => d.ContactNumber == v_contact);
            }

            //var donors = _context.BloodDonors.ToList();

            //var donors = query.ToList();

            var donors = query.Select(d => new BloodDonorListViewModel
            {
                Id = d.Id,
                FullName = d.FullName,
                ContactNumber = d.ContactNumber,
                Age = DateTime.Now.Year - d.DateOfBirth.Year,
                Email = d.Email,
                BloodGroup = d.BloodGroup.ToString(),
                LastDonationDate = d.LastDonationDate.HasValue ? $"{(DateTime.Today - d.LastDonationDate.Value).Days} days ago" : "Never",
                Address = d.Address,
                CreatedAt = d.CreatedAt.ToString("dd/MM/yyyy"),
                UpdatedAt = d.UpdatedAt.HasValue ? d.UpdatedAt.ToString() : "",
                ProfilePicture = d.ProfilePicture,
                IsEligible = (d.weight > 45 && d.weight < 200) && (d.LastDonationDate == null || (DateTime.Now - d.LastDonationDate.Value).TotalDays >= 90)

            }).ToList();

            return View(donors);
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
            };

            if(donor.ProfilePicture != null && donor.ProfilePicture.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(donor.ProfilePicture.FileName)}";
                var folderPath = Path.Combine(_env.WebRootPath, "profiles");

                if(!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fullPath = Path.Combine(folderPath, fileName);
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await donor.ProfilePicture.CopyToAsync(stream);
                }                
                donorEntity.ProfilePicture = Path.Combine("profiles", fileName); // Save the file name in the entity
            }

            _context.BloodDonors.Add(donorEntity);
            _context.SaveChanges();
            return RedirectToAction("Index");            
        }

    }
}
