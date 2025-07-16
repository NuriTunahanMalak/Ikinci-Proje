using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Dtos;

namespace YeniProjeDeneme1.Services
{
    public class AuthService
    {
        public readonly AppDbContex _context;
        public AuthService(AppDbContex context)
        {
            _context = context;
        }
        public bool IsAdmin(Login login)
        {
            var users = _context.User.ToList();
            if (users.Any(p=> p.UserName==login.UserName&& p.Password==login.Password))
            {
                var role = _context.User.FirstOrDefault(p=> p.UserName == login.UserName && p.Password == login.Password);
                if (role != null && role.Role == "Admin")
                {
                    return true;
                }
                else if(role !=null && role.Role=="User")
                {
                    return false;
                }
            }
            throw new Exception("User not found or invalid credentials.");
        }
    }
}
