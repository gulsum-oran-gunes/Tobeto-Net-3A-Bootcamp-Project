using Core.CrossCuttingConcerns.Rules;
using Core.Exceptions.Types;
using DataAccess.Abstracts;
using DataAccess.Concretes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules
{
    public class ApplicantBusinessRules : BaseBusinessRules
       
    {
        private readonly IApplicantRepository _repository;
        public ApplicantBusinessRules(IApplicantRepository repository)
        {
            _repository = repository;
        }

        public async Task CheckIfIdNotExists(int applicantId)
        {
            var isExists = await _repository.GetAsync(applicant => applicant.Id == applicantId);
            if (isExists is null) throw new BusinessException("Id not exists");

        }

        public async Task CheckIfApplicantNotExists(string userName, string nationalIdentity)
        {
            var isExists = await _repository.GetAsync(applicant => applicant.UserName == userName || applicant.NationalIdentity == nationalIdentity);
            if (isExists is not null) throw new BusinessException("UserName or National Identity is already exists");

        }






    }
}
