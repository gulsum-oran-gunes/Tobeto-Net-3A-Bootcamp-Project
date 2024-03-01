using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Constants;
using Business.Requests.ApplicationStates;
using Business.Responses.ApplicationStates;
using Business.Rules;
using Core.Exceptions.Types;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.Repositories;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class ApplicationStateManager:IApplicationStateService
    {
        private readonly IApplicationStateRepository _applicationStateRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationStateRules _rules;
        public ApplicationStateManager(IApplicationStateRepository applicationStateRepository, IMapper mapper, ApplicationStateRules rules)
        {
            _applicationStateRepository = applicationStateRepository;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<IDataResult<List<GetAllApplicationStateResponse>>> GetAllAsync()
        {
            List<ApplicationState> applicationStates = await _applicationStateRepository.GetAllAsync();
            List<GetAllApplicationStateResponse> responses = _mapper.Map<List<GetAllApplicationStateResponse>>(applicationStates);
            return new SuccessDataResult<List<GetAllApplicationStateResponse>>(responses, ApplicationStateMessages.ApplicationStateGetAll);
        }
        public async Task<IDataResult<GetByIdApplicationStateResponse>> GetByIdAsync(int id)
        {
            await _rules.CheckIfIdNotExists(id);
            ApplicationState applicationState = await _applicationStateRepository.GetAsync(x => x.Id == id);
            GetByIdApplicationStateResponse response = _mapper.Map<GetByIdApplicationStateResponse>(applicationState);
            return new SuccessDataResult<GetByIdApplicationStateResponse>(response, ApplicationStateMessages.ApplicationStateGetById);
        }
        public async Task<IDataResult<CreateApplicationStateResponse>> AddAsync(CreateApplicationStateRequest request)
        {
            ApplicationState applicationState = _mapper.Map<ApplicationState>(request);
            await _applicationStateRepository.AddAsync(applicationState);

            CreateApplicationStateResponse response = _mapper.Map<CreateApplicationStateResponse>(applicationState);
            return new SuccessDataResult<CreateApplicationStateResponse>(response, ApplicationStateMessages.ApplicationStateAdded);
        }

        public async Task<IResult> DeleteAsync(DeleteApplicationStateRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            ApplicationState applicationState = await _applicationStateRepository.GetAsync(x => x.Id == request.Id);
            await _applicationStateRepository.DeleteAsync(applicationState);
            return new SuccessResult(ApplicationStateMessages.ApplicationStateDeleted);
        }
        public async Task<IDataResult<UpdateApplicationStateResponse>> UpdateAsync(UpdateApplicationStateRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            ApplicationState applicationState = await _applicationStateRepository.GetAsync(x => x.Id == request.Id);
            applicationState = _mapper.Map(request, applicationState);
            await _applicationStateRepository.UpdateAsync(applicationState);
            UpdateApplicationStateResponse response = _mapper.Map<UpdateApplicationStateResponse>(applicationState);
            return new SuccessDataResult<UpdateApplicationStateResponse>(response, ApplicationStateMessages.ApplicationStateUpdated);
        }
        

    }
}
