using Business.Requests.Users;
using Business.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Requests.Instructors
{
    public class CreateInstructorRequest :CreateUserRequest
    {
        public string CompanyName { get; set; }
    }
}
