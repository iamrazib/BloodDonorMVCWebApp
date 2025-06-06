﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonorMVCWebApp.Models
{
    public class BloodDonorEntity
    {
        [Key]
        public int Id { get; set; }
        public required string FullName { get; set; }
        
        [Phone]
        [Length(10,15)]
        public required string ContactNumber { get; set; }
        public required DateTime DateOfBirth { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
        public required BloodGroupEnum BloodGroup { get; set; }

        [Range(50,150)]
        [Display(Name ="Weight (Kg)")]
        public float weight { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public string? Address { get; set; }
        public bool IsAvailableForDonation { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string? ProfilePicture { get; set; }
        public Collection<Donation> Donations { get; set; } = new Collection<Donation>();
    }

    public enum BloodGroupEnum
    {
        APositive,
        ANegative,
        BPositive,
        BNegative,
        ABPositive,
        ABNegative,
        OPositive,
        ONegative
    }

    public class Donation
    {
        [Key]
        public int Id { get; set; }
        public required DateTime DonationDate { get; set; }
        public string? Location { get; set; }

        [ForeignKey("BloodDonor")]
        public required int BloodDonorId { get; set; }
    }
}