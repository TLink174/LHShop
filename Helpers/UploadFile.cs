using Microsoft.Extensions.Hosting;

namespace LHShop.Helpers
{
    public class UploadFile
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public UploadFile(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string UploadFileIMG(IFormFile file, string subFolder)
        {
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, subFolder);
                string sanitizedFileName = SanitizeFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, sanitizedFileName);
                try
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    return sanitizedFileName;
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine($"Error uploading file: {ex.Message}");
                    return null;
                }
            }
            return null;
        }

        private string SanitizeFileName(string fileName)
        {
            // Implement logic to sanitize file name
            return Path.GetFileName(fileName);
        }
    }
}
