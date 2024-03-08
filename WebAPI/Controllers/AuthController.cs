using Business.Abstracts;
using Core.Utilities.Security.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register applicant")]
        public async Task<IActionResult> Register(ApplicantForRegisterDto applicantForRegisterDto)
        {
            var result = await _authService.RegisterApplicant(applicantForRegisterDto);
            return HandleDataResult(result);
        }
        [HttpPost("register employee")]
        public async Task<IActionResult> Register(EmployeeForRegisterDto employeeForRegisterDto)
        {
            var result = await _authService.RegisterEmployee(employeeForRegisterDto);
            return HandleDataResult(result);
        }
        [HttpPost("register instructor")]
        public async Task<IActionResult> Register(InstructorForRegisterDto instructorForRegisterDto)
        {
            var result = await _authService.RegisterInstructor(instructorForRegisterDto);
            return HandleDataResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var result = await _authService.Login(userForLoginDto);
            return HandleDataResult(result);
        }



    }
}
