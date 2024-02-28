using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Requests.Applications;
using Business.Responses.Applications;
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
        private readonly IBlacklistRepository _blacklistRepository;
        public ApplicationManager(IApplicationRepository applicationRepository, IMapper mapper, IBlacklistRepository blacklistRepository)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _blacklistRepository = blacklistRepository;
        }

        public async Task<IDataResult<List<GetAllApplicationResponse>>> GetAllAsync()
        {
            List<Application> applications = await _applicationRepository.GetAllAsync(include: x => x.Include(x => x.Bootcamp).Include(x => x.ApplicationState).Include(x => x.Applicant));
            List<GetAllApplicationResponse> responses = _mapper.Map<List<GetAllApplicationResponse>>(applications);
            return new SuccessDataResult<List<GetAllApplicationResponse>>(responses, "Listeleme İşlemi Başarılı");
        }
        public async Task<IDataResult<GetByIdApplicationResponse>> GetByIdAsync(int id)
        {
            await CheckIfIdNotExists(id);
            Application application = await _applicationRepository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.Bootcamp).Include(x => x.ApplicationState).Include(x => x.Applicant));
            GetByIdApplicationResponse response = _mapper.Map<GetByIdApplicationResponse>(application);
            return new SuccessDataResult<GetByIdApplicationResponse>(response, "GetById İşlemi Başarılı");
        }
        public async Task<IDataResult<CreateApplicationResponse>> AddAsync(CreateApplicationRequest request)
        {
            await CheckIfBlacklist(request.ApplicantId);
            Application application = _mapper.Map<Application>(request);
            await _applicationRepository.AddAsync(application);

            CreateApplicationResponse response = _mapper.Map<CreateApplicationResponse>(application);
            return new SuccessDataResult<CreateApplicationResponse>(response, "Ekleme İşlemi Başarılı");
        }

        public async Task<IResult> DeleteAsync(DeleteApplicationRequest request)
        {
            await CheckIfIdNotExists(request.Id);
            Application application = await _applicationRepository.GetAsync(x => x.Id == request.Id);
            await _applicationRepository.DeleteAsync(application);
            return new SuccessResult("Silme İşlemi Başarılı");
        }
        public async Task<IDataResult<UpdateApplicationResponse>> UpdateAsync(UpdateApplicationRequest request)
        {
            await CheckIfIdNotExists(request.Id);
            Application application = await _applicationRepository.GetAsync(x => x.Id == request.Id);
            application = _mapper.Map(request, application);
            await _applicationRepository.UpdateAsync(application);
            UpdateApplicationResponse response = _mapper.Map<UpdateApplicationResponse>(application);
            return new SuccessDataResult<UpdateApplicationResponse>(response, "Güncelleme İşlemi Başarılı");
        }

        private async Task CheckIfIdNotExists(int applicationId)
        {
            var isExists = await _applicationRepository.GetAsync(application => application.Id == applicationId);
            if (isExists is null) throw new BusinessException("Id not exists");

        }
        private async Task CheckIfBlacklist(int id)
        {
            var isBlacklisted = await _blacklistRepository.GetAsync(x => x.ApplicantId == id);
            if (isBlacklisted is not null) throw new BusinessException("Bu başvuru sahibi kara listede olduğu için başvuru oluşturulamaz.");

        }


       
    }
}
