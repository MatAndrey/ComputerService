using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public interface ISupportRepository
    {
        Task<SupportMessage> AddMessageAsync(SupportMessage message);
        IQueryable<SupportMessage> GetAllMessages();
    }
}
