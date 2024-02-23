using Business.Abstracts;
using Business.Requests.Employees;

using Business.Responses.Employees;

using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _employeeService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _employeeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IDataResult<CreateEmployeeResponse>> AddAsync(CreateEmployeeRequest request)
        {
            return await _employeeService.AddAsync(request);
        }

        [HttpDelete]
        public async Task<Core.Utilities.Results.IResult> DeleteAsync(DeleteEmployeeRequest request)
        {
            return await _employeeService.DeleteAsync(request);
        }

        [HttpPut]
        public async Task<IDataResult<UpdateEmployeeResponse>> UpdateAsync(UpdateEmployeeRequest request)
        {
            return await _employeeService.UpdateAsync(request);
        }
    }
}
