namespace ComputerService.Data.Repositories
{
    public interface IFileRepository
    {
        Task<string> SaveFileAsync(IFormFile file, string subDirectory);
        void DeleteFile(string filePath);
    }
}
