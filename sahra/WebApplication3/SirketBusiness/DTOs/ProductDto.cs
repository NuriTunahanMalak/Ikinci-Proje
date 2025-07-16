namespace SirketBusiness.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class ProductCreateDto
    {
        public string Name { get; set; } = null!;
    }

    public class ProductUpdateDto
    {
        public string? Name { get; set; }
    }
}
