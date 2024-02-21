using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class ApplicationState : BaseEntity<int>
    {

        public string Name { get; set; }

        public ICollection<Application> Applications { get; set; }

        public ApplicationState()
        {
            Applications = new HashSet<Application>();
        }
        public ApplicationState(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
