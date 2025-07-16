using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirketEntites
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Departmana bağlı kullanıcılar
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
