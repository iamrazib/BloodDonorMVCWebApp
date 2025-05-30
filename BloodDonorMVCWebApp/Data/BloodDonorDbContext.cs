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
            
            /*
            modelBuilder.Entity<BloodDonorEntity>().HasData(new BloodDonorEntity
            {
                Id=1,
                BloodGroup = BloodGroupEnum.APositive,
                FullName = "John Doe",
                ContactNumber = "1234567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "johndoe@example.com",
                weight=60,
                Address = "123 Main St, NewYork, USA",
                LastDonationDate= DateTime.UtcNow.AddMonths(-3),
                IsAvailableForDonation = true,
                CreatedAt = DateTime.UtcNow,
                ProfilePicture ="profiles/john_doe_profile.jpg"
            },
            new BloodDonorEntity
            {
                Id = 2,
                BloodGroup = BloodGroupEnum.BNegative,
                FullName = "Jane Smith",
                ContactNumber = "0987654321",
                DateOfBirth = new DateTime(1992, 2, 2),
                Email = "janesmith@example.com",
                weight=55,
                Address = "456 Elm St, Chicago, UK",
                LastDonationDate= DateTime.UtcNow.AddMonths(-6),
                IsAvailableForDonation = true,
                CreatedAt = DateTime.UtcNow,
                ProfilePicture = "profiles/jane_smith_profile.jpg"
            });*/
        }
    }
}