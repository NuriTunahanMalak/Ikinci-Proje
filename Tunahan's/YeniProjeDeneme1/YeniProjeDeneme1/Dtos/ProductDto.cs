namespace YeniProjeDeneme1.Dtos
{
    public class ProductDto
    {
    }
    public class ProductDtoRead
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SensorDto_p> Sensors { get; set; } = new List<SensorDto_p>();
        public List<ProjectProductDtoam> ProjectProducts { get; set; } = new List<ProjectProductDtoam>();
    }
    public class SensorDto_p()
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ProjectProductDtoam
    {
        public int ProductId { get; set; }
    }
    public class ProductCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
