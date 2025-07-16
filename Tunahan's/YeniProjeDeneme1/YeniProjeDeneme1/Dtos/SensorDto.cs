namespace YeniProjeDeneme1.Dtos
{
    public class SensorDto
    {
    }
    public class SensorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SensorDataDto> SensorDatas { get; set; } = new List<SensorDataDto>();
        public int ProductId { get; set; }
    }
    public class SensorDataDto
    {
        public int SensorId { get; set; }
        public int Data { get; set; }
    }
    public class SensorCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }
    public class SensorUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }


}
