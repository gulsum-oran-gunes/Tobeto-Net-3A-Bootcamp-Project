using Business.Abstracts;
using Business.Requests.ApplicationStates;
using Business.Responses.ApplicationStates;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationStatesController : ControllerBase
    {
        private readonly IApplicationStateService _applicationStateService;

        public ApplicationStatesController(IApplicationStateService applicationStateService)
        {
            _applicationStateService = applicationStateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _applicationStateService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _applicationStateService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IDataResult<CreateApplicationStateResponse>> AddAsync(CreateApplicationStateRequest request)
        {
            return await _applicationStateService.AddAsync(request);
        }

        [HttpDelete]
        public async Task<Core.Utilities.Results.IResult> DeleteAsync(DeleteApplicationStateRequest request)
        {
            return await _applicationStateService.DeleteAsync(request);
        }

        [HttpPut]
        public async Task<IDataResult<UpdateApplicationStateResponse>> UpdateAsync(UpdateApplicationStateRequest request)
        {
            return await _applicationStateService.UpdateAsync(request);
        }
    }
}
