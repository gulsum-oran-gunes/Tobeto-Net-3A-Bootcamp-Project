using Core.Utilities.Results;
using Core.Utilities.Security.Dtos;
using Core.Utilities.Security.Entities;
using Core.Utilities.Security.JWT;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAuthService
    {
        Task<DataResult<AccessToken>> Login(UserForLoginDto userForLoginDto);
        Task<DataResult<AccessToken>> RegisterApplicant(ApplicantForRegisterDto applicantForRegisterDto);
        Task<DataResult<AccessToken>> RegisterEmployee(EmployeeForRegisterDto employeeForRegisterDto);
        Task<DataResult<AccessToken>> RegisterInstructor(InstructorForRegisterDto instructorForRegisterDto);
        Task<DataResult<AccessToken>> CreateAccessToken(User user);
    }
}
