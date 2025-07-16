using FluentValidation;
using Microsoft.EntityFrameworkCore;
using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Dtos;
using YeniProjeDeneme1.Entities;

namespace YeniProjeDeneme1.Services
{
    public class UserService
    {
        public readonly AppDbContex _context;
        public readonly IValidator<UserCreateDto> _userCreateValidator;
        public readonly IValidator<UserUpdateDto> _userUpdateValidator;
        public UserService(IValidator<UserCreateDto> userCreateValidator,
                           IValidator<UserUpdateDto> userUpdateValidator,
                           AppDbContex context)
        {
            _userCreateValidator = userCreateValidator;
            _userUpdateValidator = userUpdateValidator;
            _context = context;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.User
                .Include(u => u.ProjectUsers)
                .ToListAsync();
            return users;
        }
        public async Task CreateUser(UserCreateDto userCreateDto)
        {
            var result = _userCreateValidator.Validate(userCreateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            if (_context.User.Any(u => u.Id == userCreateDto.Id))
            {
                throw new Exception("User already exists with the same username.");
            }
            if (_context.User.Any(u => u.UserName == userCreateDto.UserName))
            {
                throw new Exception("User already exists with the same username.");
            }
            var user = new User
            {
                UserName = userCreateDto.UserName,
                Password = userCreateDto.Password,
                Role = userCreateDto.Role,
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUser(UserUpdateDto userUpdateDto)
        {
            var result = _userUpdateValidator.Validate(userUpdateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userUpdateDto.Id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.UserName == userUpdateDto.UserName);
            if (existingUser!=null)
            {
                throw new Exception("UserName exist");
            }
            user.UserName = userUpdateDto.UserName;
            user.Password = userUpdateDto.Password;
            user.Role = userUpdateDto.Role;
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
