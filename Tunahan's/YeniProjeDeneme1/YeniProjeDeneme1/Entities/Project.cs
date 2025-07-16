namespace YeniProjeDeneme1.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<ProjectProduct> ProjectProducts { get; set; }
    }
}
