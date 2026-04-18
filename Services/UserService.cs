using ComputerService.Data.Repositories;
using ComputerService.Models;
using ComputerService.ViewModels;

namespace ComputerService.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task<User?> AuthenticateAsync(string login, string password)
        {
            var user = await repository.GetByLoginAsync(login);
            if (user is null) return null;
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if(!isPasswordValid) return null;
            return user;
        }

        public async Task<UserViewModel> CreateUserAsync(UserViewModel viewModel)
        {
            var exitingUser = await repository.GetByLoginAsync(viewModel.Login);
            if (exitingUser != null)
                throw new ArgumentException("User alredy exists");

            var user = new User
            {
                Email = viewModel.Email,
                Login = viewModel.Login,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(viewModel.Password)
            };
            await repository.CreateUserAsync(user);
            return viewModel;
        }

        public async Task DeleteUserAsync(string login)
        {
            await repository.DeleteUserAsync(login);
        }

        public async Task<UserViewModel?> GetUserByLoginAsync(string login)
        {
            var user = await repository.GetByLoginAsync(login);
            if (user == null) return null;
            return new UserViewModel
            {
                Email = user.Email,
                Login = user.Login,
                Privileges = user.UserPrivileges.Select(p => p.PrivilegeName).ToList()
            };
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            return (await repository.GetAllUsersAsync()).Select(user => new UserViewModel
            {
                Email = user.Email,
                Login = user.Login,
                Privileges = user.UserPrivileges.Select(p => p.PrivilegeName).ToList()
            });
        }

        public async Task<UserViewModel> UpdateUserInfoAsync(UserViewModel viewModel)
        {
            var user = await repository.GetByLoginAsync(viewModel.Login);
            if (user == null)
                throw new ArgumentException("User is not exists");

            if (!String.IsNullOrEmpty(viewModel.Login)) user.Login = viewModel.Login;
            if (!String.IsNullOrEmpty(viewModel.Email)) user.Email = viewModel.Email;
            if (!String.IsNullOrEmpty(viewModel.Password)) user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(viewModel.Password);

            await repository.UpdateUserAsync(user);
            return viewModel;
        }

        public async Task<UserViewModel> UpdateUserPrivileges(string login, List<string> privileges)
        {
            var user = await repository.GetByLoginAsync(login);
            if (user == null)
                throw new ArgumentException("User is not exists");

            var existingPrivileges = user.UserPrivileges.ToList();

            foreach (var priv in existingPrivileges)
            {
                if (!privileges.Contains(priv.PrivilegeName))
                    user.UserPrivileges.Remove(priv);
            }

            foreach (var name in privileges)
            {
                if (!existingPrivileges.Any(p => p.PrivilegeName == name))
                    user.UserPrivileges.Add(new UserPrivilege { PrivilegeName = name });
            }

            await repository.UpdateUserAsync(user);
            return new UserViewModel {
                Email = user.Email,
                Login = user.Login,
                Privileges = user.UserPrivileges.Select(p => p.PrivilegeName).ToList()
            };
        }
    }
}
