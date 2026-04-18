using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public class SupportRepository(AppDbContext dbContext) : ISupportRepository
    {
        public async Task<SupportMessage> AddMessageAsync(SupportMessage message)
        {
            dbContext.SupportMessages.Add(message);
            await dbContext.SaveChangesAsync();
            return message;
        }

        public IQueryable<SupportMessage> GetAllMessages()
        {
            return dbContext.SupportMessages;
        }
    }
}
