using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirketEntites
{
    public class SensorData
    {
        public int Id { get; set; }

    
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }

       
        public double Value { get; set; }

      
        public DateTime Timestamp { get; set; }
    }
}
