using Business.Abstracts;
using Business.Constants;
using Business.Responses.Applicants;
using Business.Responses.Blacklists;
using Business.Responses.Bootcamps;
using Core.CrossCuttingConcerns.Rules;
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
using static System.Net.Mime.MediaTypeNames;

namespace Business.Rules
{
    public class ApplicationBusinessRules :BaseBusinessRules
    {
        private readonly IApplicationRepository _repository;
        private IBlacklistService _blacklistService;
        private IApplicantService _applicantService;
        private IApplicationStateService _applicationStateService;
        private IBootcampService _bootcampService;

        public ApplicationBusinessRules(IApplicationRepository repository, IBlacklistService blacklistService, IApplicantService applicantService, IApplicationStateService applicationStateService, IBootcampService bootcampService)
        {
            _repository = repository;
            _blacklistService = blacklistService;
            _applicantService = applicantService;
            _applicationStateService = applicationStateService;
            _bootcampService = bootcampService;
        }

        public async Task CheckIfIdNotExists(int applicationId)
        {
            var isExists = await _repository.GetAsync(application => application.Id == applicationId);
            if (isExists is null) throw new BusinessException(ApplicationMessages.ApplicationIdNotExist);

        }

        public async Task CheckIfBlacklist(int applicantId)
        {
            var isBlacklisted = await _blacklistService.GetByApplicantIdAsync(applicantId);
            if (isBlacklisted.Data is not null) throw new BusinessException(ApplicationMessages.ApplicantIsBlacklisted); 
        }

        public async Task CheckIfApplicantIdNotExists(int applicantId)
        {
            IDataResult <GetByIdApplicantResponse> isExist = await _applicantService.GetByIdAsync(applicantId);
            if (isExist is null) throw new BusinessException(ApplicantMessages.ApplicantIdNotExist);

        }
        public async Task CheckIfApplicantionStateIdNotExists(int applicationStateId)
        {
            var isExist = await _applicationStateService.GetByIdAsync(applicationStateId);
            if (isExist is null) throw new BusinessException(ApplicationStateMessages.ApplicationStateIdNotExist);

        }
        public async Task CheckIfBootcampIdNotExists(int bootcampId)
        {
            var isExist = await _bootcampService.GetByIdAsync(bootcampId);
            if (isExist is null) throw new BusinessException(BootcampMessages.BootcampIdNotExists);

        }
        public async Task CheckIfApplicantAlreadyAppliedToBootcamp(int applicantId, int bootcampId)
        {
            var existingApplication = await _repository.GetAsync(x=>x.ApplicantId == applicantId && x.BootcampId == bootcampId);
            if (existingApplication is not null) throw new BusinessException(ApplicationMessages.ApplicantAlreadyApplied);

        }



           






        }
}
