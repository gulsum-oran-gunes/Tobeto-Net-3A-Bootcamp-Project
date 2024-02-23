using Business.Abstracts;
using Business.Requests.Instructors;
using Business.Requests.Instructors;
using Business.Responses.Instructors;
using Business.Responses.Instructors;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _instructorService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _instructorService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IDataResult<CreateInstructorResponse>> AddAsync(CreateInstructorRequest request)
        {
            return await _instructorService.AddAsync(request);
        }

        [HttpDelete]
        public async Task<Core.Utilities.Results.IResult> DeleteAsync(DeleteInstructorRequest request)
        {
            return await _instructorService.DeleteAsync(request);
        }

        [HttpPut]
        public async Task<IDataResult<UpdateInstructorResponse>> UpdateAsync(UpdateInstructorRequest request)
        {
            return await _instructorService.UpdateAsync(request);
        }
    }
}
