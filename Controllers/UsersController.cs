using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using prediktif.Interfaces;
using prediktif.Models;
using prediktif.Repositories;

namespace prediktif.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repository;
        private readonly IJwtService _jwtService;

        public UsersController(IUsersRepository repository, IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "Name and password are required."
                });
            }

            var user = await _repository.GetByNameAndPassword(request.Name, request.Password);

            if (user == null)
            {
                return Unauthorized(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid name or password."
                });
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Login successful.",
                Token = token,
                User = user
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest user)
        {
            await _repository.Create(user);

            return Ok(new
            {
                message = "User created successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequest user)
        {
            await _repository.Update(user);

            return Ok(new
            {
                message = "User updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);

            return Ok(new
            {
                message = "User deleted successfully."
            });
        }
    }
}
