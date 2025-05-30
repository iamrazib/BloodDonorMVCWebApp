using System.ComponentModel.DataAnnotations;

namespace BloodDonorMVCWebApp.Models
{
    public class BloodDonorListViewModel
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string ContactNumber { get; set; }
        public required int Age { get; set; }                
        public required string Email { get; set; }
        public required string BloodGroup { get; set; }
        public required string LastDonationDate { get; set; }
        public string? Address { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string? ProfilePicture { get; set; }
        public bool IsEligible { get; set; }
    }
}
