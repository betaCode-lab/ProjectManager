using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs.Users;
using TaskManager.Services.Users.Interfaces;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public ActionResult<string> Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = _authService.Login(login);

            if (token.Equals(""))
            {
                return BadRequest(_authService.Errors);
            }

            return Ok(token);
        } 

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserInsertDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _authService.Add(userDto))
            {
                return BadRequest(_authService.Errors);
            }

            return Ok();
        }
    }
}
