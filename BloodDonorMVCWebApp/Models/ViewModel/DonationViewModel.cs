namespace BloodDonorMVCWebApp.Models.ViewModel
{
    public class DonationViewModel
    {
        public int Id { get; set; }
        public DateTime DonationDate { get; set; }
        public string? DonationDateStr { get; set; }
        public string? Location { get; set; }
        public int BloodDonorId { get; set; }
        public string DonorName { get; set; } = string.Empty;
    }
}
