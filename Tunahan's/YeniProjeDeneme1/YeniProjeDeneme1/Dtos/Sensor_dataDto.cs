namespace YeniProjeDeneme1.Dtos
{
    public class SensorDataReadDto
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public int data { get; set; }
    }

    public class SensorDataCreateDto
    {
        public int SensorId { get; set; }
        public int data { get; set; }

    }

    public class SensorDataUpdateDto
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public int data { get; set; }
    }
}