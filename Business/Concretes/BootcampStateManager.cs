using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Constants;
using Business.Requests.BootcampStates;
using Business.Responses.BootcampStates;
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
    public class BootcampStateManager :IBootcampStateService
    {
        private readonly IBootcampStateRepository _bootcampStateRepository;
        private readonly IMapper _mapper;
        private readonly BootcampStateBusinessRules _rules;

        public BootcampStateManager(IBootcampStateRepository bootcampStateRepository, IMapper mapper, BootcampStateBusinessRules rules)
        {
            _bootcampStateRepository = bootcampStateRepository;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<IDataResult<List<GetAllBootcampStateResponse>>> GetAllAsync()
        {
            List<BootcampState> bootcampStates = await _bootcampStateRepository.GetAllAsync();
            List<GetAllBootcampStateResponse> responses = _mapper.Map<List<GetAllBootcampStateResponse>>(bootcampStates);
            return new SuccessDataResult<List<GetAllBootcampStateResponse>>(responses, BootcampStateMessages.BootcampStateGetAll);
        }
        public async Task<IDataResult<GetByIdBootcampStateResponse>> GetByIdAsync(int id)
        {
            await _rules.CheckIfIdNotExists(id);
            BootcampState bootcampState = await _bootcampStateRepository.GetAsync(x => x.Id == id);
            GetByIdBootcampStateResponse response = _mapper.Map<GetByIdBootcampStateResponse>(bootcampState);
            return new SuccessDataResult<GetByIdBootcampStateResponse>(response, BootcampStateMessages.BootcampStateGetById);
        }
        public async Task<IDataResult<CreateBootcampStateResponse>> AddAsync(CreateBootcampStateRequest request)
        {
            BootcampState bootcampState = _mapper.Map<BootcampState>(request);
            await _bootcampStateRepository.AddAsync(bootcampState);

            CreateBootcampStateResponse response = _mapper.Map<CreateBootcampStateResponse>(bootcampState);
            return new SuccessDataResult<CreateBootcampStateResponse>(response, BootcampStateMessages.BootcampStateAdded);
        }

        public async Task<IResult> DeleteAsync(DeleteBootcampStateRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            BootcampState bootcampState = await _bootcampStateRepository.GetAsync(x => x.Id == request.Id);
            await _bootcampStateRepository.DeleteAsync(bootcampState);
            return new SuccessResult(BootcampStateMessages.BootcampStateDeleted);
        }
        public async Task<IDataResult<UpdateBootcampStateResponse>> UpdateAsync(UpdateBootcampStateRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            BootcampState bootcampState = await _bootcampStateRepository.GetAsync(x => x.Id == request.Id);
            bootcampState = _mapper.Map(request, bootcampState);
            await _bootcampStateRepository.UpdateAsync(bootcampState);
            UpdateBootcampStateResponse response = _mapper.Map<UpdateBootcampStateResponse>(bootcampState);
            return new SuccessDataResult<UpdateBootcampStateResponse>(response, BootcampStateMessages.BootcampStateUpdated);
        }

        
        

    }
}
