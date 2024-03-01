using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Constants;
using Business.Requests.Applications;
using Business.Responses.Applications;
using Business.Rules;
using Core.Exceptions.Types;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.Repositories;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class ApplicationManager : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationBusinessRules _rules;
        public ApplicationManager(IApplicationRepository applicationRepository, IMapper mapper, ApplicationBusinessRules rules)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<IDataResult<List<GetAllApplicationResponse>>> GetAllAsync()
        {
            List<Application> applications = await _applicationRepository.GetAllAsync(include: x => x.Include(x => x.Bootcamp).Include(x => x.ApplicationState).Include(x => x.Applicant));
            List<GetAllApplicationResponse> responses = _mapper.Map<List<GetAllApplicationResponse>>(applications);
            return new SuccessDataResult<List<GetAllApplicationResponse>>(responses, ApplicationMessages.ApplicationGetAll);
        }
        public async Task<IDataResult<GetByIdApplicationResponse>> GetByIdAsync(int id)
        {
            await _rules.CheckIfIdNotExists(id);
            Application application = await _applicationRepository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.Bootcamp).Include(x => x.ApplicationState).Include(x => x.Applicant));
            GetByIdApplicationResponse response = _mapper.Map<GetByIdApplicationResponse>(application);
            return new SuccessDataResult<GetByIdApplicationResponse>(response,ApplicationMessages.ApplicationGetById);
        }
        public async Task<IDataResult<CreateApplicationResponse>> AddAsync(CreateApplicationRequest request)
        {
            await _rules.CheckIfBlacklist(request.ApplicantId);
            Application application = _mapper.Map<Application>(request);
            await _applicationRepository.AddAsync(application);

            CreateApplicationResponse response = _mapper.Map<CreateApplicationResponse>(application);
            return new SuccessDataResult<CreateApplicationResponse>(response, ApplicationMessages.ApplicationAdded);
        }

        public async Task<IResult> DeleteAsync(DeleteApplicationRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            Application application = await _applicationRepository.GetAsync(x => x.Id == request.Id);
            await _applicationRepository.DeleteAsync(application);
            return new SuccessResult(ApplicationMessages.ApplicationDeleted);
        }
        public async Task<IDataResult<UpdateApplicationResponse>> UpdateAsync(UpdateApplicationRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            Application application = await _applicationRepository.GetAsync(x => x.Id == request.Id);
            application = _mapper.Map(request, application);
            await _applicationRepository.UpdateAsync(application);
            UpdateApplicationResponse response = _mapper.Map<UpdateApplicationResponse>(application);
            return new SuccessDataResult<UpdateApplicationResponse>(response, ApplicationMessages.ApplicationUpdated);
        }

        


       
    }
}
