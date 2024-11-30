using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Allow only authenticated users
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Allow any authenticated user to fetch their own details or admin to fetch anyone's details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var currentUserRole = User.FindFirstValue(ClaimTypes.Role);

            // Allow Admins to fetch any user; Regular users can only fetch their own details
            if (currentUserRole != "Admin" && currentUserId != id)
            {
                return Forbid("You are not authorized to view this user's details.");
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        // Allow any authenticated user to fetch their own username
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null) return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        // Allow only Admins to fetch all users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // Allow only Admins to delete users
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound(new { message = "User not found" });

            return Ok(new { message = "User deleted successfully" });
        }
    }
}
