namespace ComputerService.Data.Repositories
{
    public class FileRepository(IWebHostEnvironment env) : IFileRepository
    {
        private readonly string _webRootPath = env.WebRootPath;

        public async Task<string> SaveFileAsync(IFormFile file, string subDirectory)
        {
            var uploadDir = Path.Combine(_webRootPath, "uploads", subDirectory);
            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadDir, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"/uploads/{subDirectory}/{fileName}";
        }

        public void DeleteFile(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl)) return;
            var physicalPath = Path.Combine(_webRootPath, fileUrl.TrimStart('/'));
            if (File.Exists(physicalPath))
                File.Delete(physicalPath);
        }
    }
}
