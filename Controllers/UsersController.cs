using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IUsersRepository repository)
        {
            _repository = repository;
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
        public async Task<IActionResult> Create(User user)
        {
            await _repository.Create(user);

            return Ok(new
            {
                message = "User created successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
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
