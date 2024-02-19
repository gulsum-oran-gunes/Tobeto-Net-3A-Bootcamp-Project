using Business.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses.Employees
{
    public class CreateEmployeeResponse :CreateUserResponse
    {
        public string Position { get; set; }
        
    }
}
