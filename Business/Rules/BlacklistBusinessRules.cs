using Business.Abstracts;
using Business.Constants;
using Core.CrossCuttingConcerns.Rules;
using Core.Exceptions.Types;
using DataAccess.Abstracts;
using DataAccess.Concretes.Repositories;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules
{
    public class BlacklistBusinessRules :BaseBusinessRules
    {
        private readonly IBlacklistRepository _repository;
        private readonly IApplicantService _applicantService;

        public BlacklistBusinessRules(IBlacklistRepository repository, IApplicantService applicantService)
        {

            _repository = repository;
            _applicantService = applicantService;
        }
        public async Task CheckIfIdNotExists(int blacklistId)
        {
            var isExists = await _repository.GetAsync(blacklist => blacklist.Id == blacklistId);
            if (isExists is null) throw new BusinessException(BlacklistMessages.BlacklistIdNotExists);

        }
        public async Task CheckIfApplicantIdNotExists(int applicantId)
        {
            var applicant = await _applicantService.GetByIdAsync(applicantId);
            if (applicant is null) throw new BusinessException(ApplicantMessages.ApplicantIdNotExist);

        }
        public async Task CheckIfApplicantAlreadyBlacklisted(int applicantId)
        {
            var isBlacklisted = await _repository.GetAsync(x => x.ApplicantId == applicantId);
            if (isBlacklisted is not null) throw new BusinessException(BlacklistMessages.ApplicantAlreadyBlacklist);
           
        }


    }
}
