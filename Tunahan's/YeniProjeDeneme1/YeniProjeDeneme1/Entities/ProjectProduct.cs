namespace YeniProjeDeneme1.Entities
{
    public class ProjectProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
