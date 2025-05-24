using JobTrackerApp.Application.Helpers;
using JobTrackerApp.Application.Interfaces;
using JobTrackerApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobTrackerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result.IsFail)
                return StatusCode((int)result.Status, result.ErrorMessage);

            var user = result.Data!;
            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            var users = result.Data!
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    Email = u.Email
                })
                .ToList();

            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                PasswordHash = PasswordHelper.HashPassword(dto.Password)
            };

            var result = await _userService.CreateAsync(user);
            return StatusCode((int)result.Status, new UserResponseDto
            {
                Id = result.Data!.Id,
                Name = result.Data.Name,
                Surname = result.Data.Surname,
                Email = result.Data.Email
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var users = await _userService.GetAllAsync();
            var user = users.Data!.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || !PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash))
                return Unauthorized("Email veya şifre hatalı");

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            });
        }
    }
}
