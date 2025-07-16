namespace YeniProjeDeneme1.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin, User, etc.
        public ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}

