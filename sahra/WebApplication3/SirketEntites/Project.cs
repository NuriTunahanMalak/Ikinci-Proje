using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirketEntites
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

     
        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();

   
        public ICollection<ProjectProduct> ProjectProducts { get; set; } = new List<ProjectProduct>();
    }
}
