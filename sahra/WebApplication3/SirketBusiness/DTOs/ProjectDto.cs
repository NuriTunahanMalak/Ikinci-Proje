namespace SirketBusiness.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<int> UserIds { get; set; } = new();
        public List<int> ProductIds { get; set; } = new();
    }

    public class ProjectCreateDto
    {
        public string Name { get; set; } = null!;
        public List<int>? UserIds { get; set; }
        public List<int>? ProductIds { get; set; }
    }

    public class ProjectUpdateDto
    {
        public string? Name { get; set; }
        public List<int>? UserIds { get; set; }
        public List<int>? ProductIds { get; set; }
    }
}
