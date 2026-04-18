namespace ComputerService.ViewModels
{
    public class UserViewModel
    {
        public string Login {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Privileges { get; set; }
    }
}
