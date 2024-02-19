using Business.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses.Applicants
{
    public class GetByIdApplicantResponse:GetByIdUserResponse
    {

        public string About { get; set; }
    }
}
