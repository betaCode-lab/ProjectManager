using AutoMapper;
using TaskManager.DTOs.Users;
using TaskManager.Models;
using TaskManager.Repositories.Core.Interfaces;
using TaskManager.Services.Tokens.Interface;
using TaskManager.Services.Users.Interfaces;

namespace TaskManager.Services.Users
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public List<string> Errors { get; set; }

        public AuthService(
            ITokenService tokenService, 
            IGenericRepository<User> userRepository,
            IMapper mapper
            )
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _mapper = mapper;

            Errors = new List<string>();
        }

        public string Login(Login login)
        {
            try
            {
                var user = _userRepository.Search(u => u.Email.ToLower() == login.Email.ToLower()).FirstOrDefault();

                if (user == null)
                {
                    Errors.Add("This user was not found.");
                    return "";
                }

                if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
                {
                    Errors.Add("Incorrect password.");
                    return "";
                }

                var userDto = _mapper.Map<UserDto>(user);

                var token = _tokenService.GenerateAccessToken(userDto);
                string tokenString = _tokenService.WriteToken(token);

                return tokenString;
            }
            catch
            {
                Errors.Add("An error was ocurred while login");
                return "";
            }
        }

        public async Task<bool> Add(UserInsertDto userInsertDto)
        {
            try
            {
                if (_userRepository.Search(u => u.Username == userInsertDto.Username).FirstOrDefault() != null)
                {
                    Errors.Add("This username is already in use.");
                    return false;
                }

                if (_userRepository.Search(u => u.Email.ToLower() == userInsertDto.Email.ToLower()).FirstOrDefault() != null)
                {
                    Errors.Add("This email is already in use.");
                    return false;
                }

                if (userInsertDto.Password != userInsertDto.ConfirmPassword)
                {
                    Errors.Add("Passwords do not match.");
                    return false;
                }

                var user = _mapper.Map<User>(userInsertDto);

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                await _userRepository.Add(user);
                await _userRepository.Save();

                return true;
            }
            catch
            {
                Errors.Add("An error was ocurred while register.");
                return false;
            }
        }
    }
}
