namespace SirketEntites
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        // Şifre hash olarak tutulacak, plain-text saklama
        public string PasswordHash { get; set; } = string.Empty;

        // Kullanıcı rolü: "Admin", "User" gibi
        public string Role { get; set; } = "User";

        // Kullanıcının bağlı olduğu departman
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // Kullanıcının projeleri (çoktan çoğa ilişki)
        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
