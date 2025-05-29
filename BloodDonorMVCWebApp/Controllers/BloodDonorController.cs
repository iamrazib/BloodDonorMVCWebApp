using BloodDonorMVCWebApp.Data;
using BloodDonorMVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonorMVCWebApp.Controllers
{
    //[Route("BloodDonor/[controller]")]
    public class BloodDonorController : Controller
    {
        private readonly BloodDonorDbContext _context;
        
        public BloodDonorController(BloodDonorDbContext context) {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BloodDonorEntity donor)
        {
            ViewBag.Message = "Donor Created Successfully";
            if (ModelState.IsValid)
            {
                _context.BloodDonors.Add(donor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
