namespace SirketBusiness.DTOs
{
    public class SensorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
     
        public string SensorType { get; set; }   
    }

    public class SensorCreateDto
    {
        public string Name { get; set; }
   
        public string SensorType { get; set; }

        public string ProductId { get; set; }  
    }

    public class SensorDataDto
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class SensorDataCreateDto
    {
        public int SensorId { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
