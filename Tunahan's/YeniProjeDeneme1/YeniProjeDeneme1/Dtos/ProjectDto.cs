namespace YeniProjeDeneme1.Dtos
{
    public class ProjectDto
    {

    }
    public class ProjectReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProjectUserDtoa> ProjectUsers { get; set; } = new List<ProjectUserDtoa>();
        public List<ProjectProductDtoa> ProjectProducts { get; set; } = new List<ProjectProductDtoa>();



    }
    public class ProjectProductDtoa
    {
        public int ProductId { get; set; }
    }
    public class ProjectUserDtoa
    {
        public int UserId { get; set; }
    }
    public class ProjectCreateDto
    {
        public string Name { get; set; }
    }
    public class ProjectUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
