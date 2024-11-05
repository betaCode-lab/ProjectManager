using TaskManager.DTOs.Users;

namespace TaskManager.Services.Users.Interfaces
{
    public interface IAuthService
    {
        List<string> Errors { get; set; }

        string Login(Login login);

        Task<bool> Add(UserInsertDto userDto);
    }
}
