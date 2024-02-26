using Business.Abstracts;
using Business.Requests.Blacklists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlacklistsController : BaseController
    {
        private readonly IBlacklistService _blacklistService;

        public BlacklistsController(IBlacklistService blacklistService)
        {
            _blacklistService = blacklistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return HandleDataResult(await _blacklistService.GetAllAsync());

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return HandleDataResult(await _blacklistService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateBlacklistRequest request)
        {
            return HandleDataResult(await _blacklistService.AddAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DeleteBlacklistRequest request)
        {
            return HandleResult(await _blacklistService.DeleteAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateBlacklistRequest request)
        {
            return HandleDataResult(await _blacklistService.UpdateAsync(request));
        }
    }
}
