using Business.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses.Instructors
{
    public class CreateInstructorResponse :CreateUserResponse
    {
        public string CompanyName { get; set; }
     
        
    }
}
