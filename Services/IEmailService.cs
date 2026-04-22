namespace ComputerService.Services
{
    public interface IEmailService
    {
        Task SendEmailToAdminAsync(string subject, string body);
    }
}
