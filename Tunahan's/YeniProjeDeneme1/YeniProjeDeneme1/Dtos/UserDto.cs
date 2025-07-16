namespace YeniProjeDeneme1.Dtos
{
    public class UserDto
    {

    }
    public class UserReadDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public List<ProjectUserDtom> ProjectUsers { get; set; } = new List<ProjectUserDtom>();
    }
    public class UserCreateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
    public class ProjectUserDtom {
        public int ProjectId { get; set; }
    }
    public class Login()
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}