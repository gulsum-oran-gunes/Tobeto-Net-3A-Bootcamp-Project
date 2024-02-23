using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class BootcampImage:BaseEntity<Guid>
    {
        public int BootcampId { get; set; }
        public string ImagePath { get; set; }


        public virtual Bootcamp Bootcamp { get; set; }

        public BootcampImage()
        {

        }

        public BootcampImage(Guid id, int bootcampId, string imagePath) : this()
        {
            Id = id;
            BootcampId = bootcampId;
            ImagePath = imagePath;
        }
    }
}
