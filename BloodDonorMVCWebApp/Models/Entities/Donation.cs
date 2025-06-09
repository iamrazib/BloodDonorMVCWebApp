using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonorMVCWebApp.Models.Entities
{
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
