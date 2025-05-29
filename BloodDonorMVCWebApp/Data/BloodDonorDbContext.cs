using BloodDonorMVCWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonorMVCWebApp.Data
{
    public class BloodDonorDbContext : DbContext
    {
        public BloodDonorDbContext(DbContextOptions<BloodDonorDbContext> options) : base(options)
        {
        }
        public DbSet<BloodDonorEntity> BloodDonors { get; set; }
        public DbSet<Donation> Donations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configurations can be added here
        }
    }
}