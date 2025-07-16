namespace YeniProjeDeneme1.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sensor> Sensor { get; set; }
        public ICollection<ProjectProduct> ProjectProducts { get; set; }
    }
}
