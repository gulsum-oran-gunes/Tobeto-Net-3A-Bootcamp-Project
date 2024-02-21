using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Bootcamp : BaseEntity<int>
    {
        public string Name { get; set; }
        public int InstructorId { get; set; }
        public int BootcampStateId {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Instructor? Instructor { get; set; }
        public virtual BootcampState? BootcampState { get; set; }
        public ICollection<Application> Applications { get; set; }
        public Bootcamp()
        {
            Applications = new HashSet<Application>();
        }
        
        public Bootcamp(int id, string name, int instructorId, int bootcampState, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Name = name;
            InstructorId = instructorId;
            BootcampStateId = bootcampState;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
