using BloodDonorMVCWebApp.Services.Interfaces;
using System.Drawing;

namespace BloodDonorMVCWebApp.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveFileAsync(IFormFile? file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var folderPath = Path.Combine(_env.WebRootPath, "profiles");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fullPath = Path.Combine(folderPath, fileName);
                
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Path.Combine("profiles", fileName); // Save the file name in the entity
            }
            return string.Empty; // Return an empty string if no file is provided
        }
    }
}
