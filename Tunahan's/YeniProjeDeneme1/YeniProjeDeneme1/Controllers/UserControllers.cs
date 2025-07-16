using Microsoft.AspNetCore.Mvc;
using YeniProjeDeneme1.Services;
using YeniProjeDeneme1.Dtos;
using Microsoft.AspNetCore.Authorization;
namespace YeniProjeDeneme1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserControllers:ControllerBase
    {
        public readonly UserService _userService;
        public UserControllers(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userCreateDto)
        {
            await _userService.CreateUser(userCreateDto);
            return Ok("User created successfully.");
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto userUpdateDto)
        {
            await _userService.UpdateUser(userUpdateDto);
            return Ok("User updated successfully.");
        }
    }
}
