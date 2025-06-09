namespace BloodDonorMVCWebApp.Utilities
{
    public class DateHelper
    {
        public static string GetLastDonationDateString(DateTime? LastDonationDate) => LastDonationDate.HasValue ? $"{(DateTime.Today - LastDonationDate.Value).Days} days ago" : "Never";
        public static int CalculateAge(DateTime DateOfBirth) => DateTime.Now.Year - DateOfBirth.Year;
    }
}
