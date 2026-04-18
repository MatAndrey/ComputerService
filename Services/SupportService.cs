using ComputerService.Data.Repositories;
using ComputerService.Models;
using ComputerService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services
{
    public class SupportService(ISupportRepository supportRepository) : ISupportService
    {
        public async Task AddMessageAsync(SupportMessageViewModel viewModel)
        {
            SupportMessage message = new()
            {
                Email = viewModel.Email,
                Message = viewModel.Message,
                Name = viewModel.Name,
                Date = DateTime.Now
            };
            await supportRepository.AddMessageAsync(message);
        }

        public async Task<IEnumerable<SupportMessageViewModel>> GetAllMessagesAsync()
        {
            return await supportRepository.GetAllMessages()
                .OrderByDescending(m => m.Date)
                .Select(m => new SupportMessageViewModel
            {
                Email = m.Email,
                Message = m.Message,
                Name = m.Name,
                Id = m.Id,
                Date = m.Date
            }).ToListAsync();
        }
    }
}
