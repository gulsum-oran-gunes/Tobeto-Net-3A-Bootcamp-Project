using Business.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses.Applicants
{
    public class CreateApplicantResponse:CreateUserResponse
    {
       
        public string About { get; set; }
        
    }
}
