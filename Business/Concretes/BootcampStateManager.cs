using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Requests.BootcampStates;
using Business.Responses.BootcampStates;
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
        public BootcampStateManager(IBootcampStateRepository bootcampStateRepository, IMapper mapper)
        {
            _bootcampStateRepository = bootcampStateRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<GetAllBootcampStateResponse>>> GetAllAsync()
        {
            List<BootcampState> bootcampStates = await _bootcampStateRepository.GetAllAsync();
            List<GetAllBootcampStateResponse> responses = _mapper.Map<List<GetAllBootcampStateResponse>>(bootcampStates);
            return new SuccessDataResult<List<GetAllBootcampStateResponse>>(responses, "Listeleme İşlemi Başarılı");
        }
        public async Task<IDataResult<GetByIdBootcampStateResponse>> GetByIdAsync(int id)
        {
            await CheckIfIdNotExists(id);
            BootcampState bootcampState = await _bootcampStateRepository.GetAsync(x => x.Id == id);
            GetByIdBootcampStateResponse response = _mapper.Map<GetByIdBootcampStateResponse>(bootcampState);
            return new SuccessDataResult<GetByIdBootcampStateResponse>(response, "GetById İşlemi Başarılı");
        }
        public async Task<IDataResult<CreateBootcampStateResponse>> AddAsync(CreateBootcampStateRequest request)
        {
            BootcampState bootcampState = _mapper.Map<BootcampState>(request);
            await _bootcampStateRepository.AddAsync(bootcampState);

            CreateBootcampStateResponse response = _mapper.Map<CreateBootcampStateResponse>(bootcampState);
            return new SuccessDataResult<CreateBootcampStateResponse>(response, "Ekleme İşlemi Başarılı");
        }

        public async Task<IResult> DeleteAsync(DeleteBootcampStateRequest request)
        {
            await CheckIfIdNotExists(request.Id);
            BootcampState bootcampState = await _bootcampStateRepository.GetAsync(x => x.Id == request.Id);
            await _bootcampStateRepository.DeleteAsync(bootcampState);
            return new SuccessResult("Silme İşlemi Başarılı");
        }
        public async Task<IDataResult<UpdateBootcampStateResponse>> UpdateAsync(UpdateBootcampStateRequest request)
        {
            await CheckIfIdNotExists(request.Id);
            BootcampState bootcampState = await _bootcampStateRepository.GetAsync(x => x.Id == request.Id);
            bootcampState = _mapper.Map(request, bootcampState);
            await _bootcampStateRepository.UpdateAsync(bootcampState);
            UpdateBootcampStateResponse response = _mapper.Map<UpdateBootcampStateResponse>(bootcampState);
            return new SuccessDataResult<UpdateBootcampStateResponse>(response, "Güncelleme İşlemi Başarılı");
        }

        private async Task CheckIfIdNotExists(int bootcampStateId)
        {
            var isExists = await _bootcampStateRepository.GetAsync(bootcampState => bootcampState.Id == bootcampStateId);
            if (isExists is null) throw new BusinessException("Id not exists");
            

        }
        

    }
}
