using Microsoft.AspNetCore.Mvc;
using SirketBusiness.Interfaces;
using SirketBusiness.DTOs;
using AutoMapper;
using SirketEntites;
using Microsoft.AspNetCore.Authorization;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        if (currentUserRole == "Admin")
        {
            var users = await _userService.GetAllUsersAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return Ok(userDtos);
        }
        else
        {
            var user = await _userService.GetAllUsersAsync();
            var currentUser = user.FirstOrDefault(u => u.Username == currentUserName);
            if (currentUser == null) return Unauthorized();
            var userDto = _mapper.Map<UserDto>(currentUser);
            return Ok(new List<UserDto> { userDto });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        if (currentUserRole != "Admin" && user.Username != currentUserName)
            return Forbid();
        var userDto = _mapper.Map<UserDto>(user);
        return Ok(userDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
    {
        var user = _mapper.Map<User>(userCreateDto);

        // Şifreyi hashle
        user.PasswordHash = ComputeSha256Hash(userCreateDto.Password);

        await _userService.AddUserAsync(user);

        var userDto = _mapper.Map<UserDto>(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDto);
    }

    private string ComputeSha256Hash(string rawData)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
    {
        var user = _mapper.Map<User>(userUpdateDto);
        user.Id = id;
        await _userService.UpdateUserAsync(user);
        return NoContent();
    }

    [HttpGet("by-department/{departmentId}")]
    public async Task<IActionResult> GetUsersByDepartment(int departmentId)
    {
        var users = await _userService.GetUsersByDepartmentAsync(departmentId);
        var dto = _mapper.Map<List<UserDto>>(users);
        return Ok(dto);
    }

}    
