namespace YeniProjeDeneme1.Entities
{
    public class Sensor_Data
    {
        public int Id { get; set; }
        public int data { get; set; }
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }
    }
}
