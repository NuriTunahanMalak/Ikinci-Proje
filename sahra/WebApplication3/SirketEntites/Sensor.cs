using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirketEntites
{
    public class Sensor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string SensorType { get; set; } = string.Empty;

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<SensorData> SensorDatas { get; set; } = new List<SensorData>();
    }
}
