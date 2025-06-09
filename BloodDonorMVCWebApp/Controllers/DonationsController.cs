using BloodDonorMVCWebApp.Data;
using BloodDonorMVCWebApp.Models.Entities;
using BloodDonorMVCWebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BloodDonorMVCWebApp.Controllers
{
    public class DonationsController : Controller
    {
        private readonly BloodDonorDbContext _context;

        public DonationsController(BloodDonorDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {            
            var donations = await (from d in _context.Donations
                                   join b in _context.BloodDonors
                                   on d.BloodDonorId equals b.Id
                                   select new DonationViewModel
                                   {
                                       Id = d.Id,
                                       DonationDate = d.DonationDate,
                                       DonationDateStr = d.DonationDate.ToString("dd/MMM/yyyy"),
                                       Location = d.Location,
                                       BloodDonorId = d.BloodDonorId,
                                       DonorName = b.FullName
                                   }).ToListAsync();

            return View(donations);
        }
                
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                        
            var donation = await (from d in _context.Donations
                                  join b in _context.BloodDonors
                                  on d.BloodDonorId equals b.Id
                                  where d.Id == id
                                  select new DonationViewModel
                                  {
                                      Id = d.Id,
                                      DonationDate = d.DonationDate,
                                      DonationDateStr = d.DonationDate.ToString("dd/MMM/yyyy"),
                                      Location = d.Location,
                                      BloodDonorId = d.BloodDonorId,
                                      DonorName = b.FullName
                                  }).FirstOrDefaultAsync();

            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }
        
        public IActionResult Create()
        {
            ViewBag.donorList = new SelectList(_context.BloodDonors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DonationDate,Location,BloodDonorId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            ViewBag.donorList = new SelectList(_context.BloodDonors, "Id", "FullName", donation.BloodDonorId);

            return View(donation);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DonationDate,Location,BloodDonorId")] Donation donation)
        {
            if (id != donation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.donorList = new SelectList(_context.BloodDonors, "Id", "FullName", donation.BloodDonorId); // Add this line again
            return View(donation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            return _context.Donations.Any(e => e.Id == id);
        }
    }
}
