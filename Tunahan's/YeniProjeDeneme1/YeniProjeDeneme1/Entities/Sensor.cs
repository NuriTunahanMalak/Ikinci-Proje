using YeniProjeDeneme1.Entities;
namespace YeniProjeDeneme1.Entities
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sensor_Data> Sensor_Datas { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
