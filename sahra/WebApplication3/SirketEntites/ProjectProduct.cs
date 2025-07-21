using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirketEntites
{
    public class ProjectProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

      
    }
}
