﻿using BloodDonorMVCWebApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BloodDonorMVCWebApp.Models.ViewModel
{
    public class BloodDonorCreateViewModel
    {
        [Required]
        public required string FullName { get; set; }

        [Phone]
        [Length(10, 15)]
        public required string ContactNumber { get; set; }
        public required DateTime DateOfBirth { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
        public required BloodGroupEnum BloodGroup { get; set; }

        [Range(50, 150)]
        [Display(Name = "Weight (Kg)")]
        public float weight { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public string? Address { get; set; }
        public bool IsAvailableForDonation { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}
