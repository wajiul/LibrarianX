using LibrarianX.DTO;
using LibrarianX.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarianX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest();
                }
                var userExist = await _userRepository.UserExistAsync(userDto.Email);
                if(userExist)
                {
                    return Conflict(new { Message = "Username or Email already exist" });
                }
                await _userRepository.AddUserAsync(userDto);
                return StatusCode(201, new { Message = "New user created successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();
                return Ok(users);
            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(id);
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.UserId || userDto == null)
            {
                return BadRequest();
            }
            try
            {
                var status = await _userRepository.UpdateUserAsync(userDto);
                if(status == false)
                {
                    return BadRequest();
                }
                return Ok("Updated successfully");

            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });

            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var status = await _userRepository.DeleteUserAsync(id);
                if(status == false)
                {
                    return BadRequest();
                }
                return Ok("Deleted successfully");
            }

            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

    }
}
