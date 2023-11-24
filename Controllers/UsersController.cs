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
        [Route("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(userId);
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            try
            {
                if (userId != userDto.UserId || userDto == null)
                {
                    return BadRequest();
                }
                var userExist = await _userRepository.UserExistAsync(userDto.Email);
                if (!userExist)
                {
                    return BadRequest();
                }
                await _userRepository.UpdateUserAsync(userDto);
                return Ok("Updated successfully");

            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });

            }
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userRepository.DeleteUserAsync(userId);
                return Ok("Deleted successfully");
            }
            catch(InvalidOperationException)
            {
                return NotFound($"User with specified Id not found");
            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

    }
}
