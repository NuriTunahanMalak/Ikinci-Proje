namespace YeniProjeDeneme1.Dtos
{
    public class ProjectProductDto
    {
    }
    public class ProjectProductReadDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ProductId { get; set; }
    }
    public class ProjectProductCreateDto
    {
        public int ProjectId { get; set; }
        public int ProductId { get; set; }
    }
    public class ProjectProductUpdateDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ProductId { get; set; }

    }

}
