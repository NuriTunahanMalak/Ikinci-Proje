namespace SirketBusiness.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public int DepartmentId { get; set; }
    }

    public class UserCreateDto
    {
        public string UserName { get; set; } = null!;
        public string DepartmentID { get; set; } = null;
        public string Password { get; set; } = null!;
    }

    public class UserUpdateDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string DepartmentId { get; set; }
    }
}
