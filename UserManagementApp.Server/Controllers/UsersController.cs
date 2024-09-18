using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Server.Models;
using UserManagementApp.Server.Views;

namespace UserManagementApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userModels = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                RegistrationDate = u.RegistrationDate,
                LastLoginDate = u.LastLoginDate,
                IsBlocked = u.IsBlocked
            });
            return Ok(userModels);
        }

        [HttpPost("block")]
        public async Task<IActionResult> BlockUsers([FromBody] List<string> userIds)
        {
            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsBlocked = true;
                    await _userManager.UpdateAsync(user);
                }
            }
            return Ok();
        }

        [HttpPost("unblock")]
        public async Task<IActionResult> UnblockUsers([FromBody] List<string> userIds)
        {
            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsBlocked = false;
                    await _userManager.UpdateAsync(user);
                }
            }
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUsers([FromBody] List<string> userIds)
        {
            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            return Ok();
        }
    }
}
