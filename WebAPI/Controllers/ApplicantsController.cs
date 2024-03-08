﻿using Business.Abstracts;
using Business.Requests.Applicants;
using Business.Responses.Applicants;
using Business.Responses.Applications;
using Business.Responses.ApplicationStates;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : BaseController
    {
        private readonly IApplicantService _applicantService;

        public ApplicantsController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [Authorize(Roles = "applicant.list")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return HandleDataResult(await _applicantService.GetAllAsync());
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return HandleDataResult(await _applicantService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult>AddAsync(CreateApplicantRequest request)
        {
            return HandleDataResult (await _applicantService.AddAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteAsync(DeleteApplicantRequest request)
        {
            return HandleResult(await _applicantService.DeleteAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateApplicantRequest request)
        {
            return HandleDataResult(await _applicantService.UpdateAsync(request));
        }

    }
}
    
