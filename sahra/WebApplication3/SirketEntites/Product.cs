using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirketEntites
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Ürüne bağlı projeler (çoktan çoğa)
        public ICollection<ProjectProduct> ProjectProducts { get; set; } = new List<ProjectProduct>();

        // Ürüne bağlı sensörler
        public ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
    }
}
