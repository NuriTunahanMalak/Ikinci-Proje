namespace YeniProjeDeneme1.Dtos
{
    public class ProjectUserDto
    {
    }
    public class ProjectUserReadDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
    public class ProjectUserCreateDto
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
    public class ProjectUserUpdateDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
