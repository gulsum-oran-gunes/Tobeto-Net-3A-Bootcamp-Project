using Business.Abstracts;
using Business.Responses.Blacklists;
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

namespace Business.Rules
{
    public class ApplicationBusinessRules :BaseBusinessRules
    {
        private readonly IApplicationRepository _repository;
        private IBlacklistService _blacklistService;

        public ApplicationBusinessRules(IApplicationRepository repository, IBlacklistService blacklistService)
        {
            _repository = repository;
            _blacklistService = blacklistService;
        }

        public async Task CheckIfIdNotExists(int applicationId)
        {
            var isExists = await _repository.GetAsync(application => application.Id == applicationId);
            if (isExists is null) throw new BusinessException("Id not exists");

        }
        public async Task CheckIfBlacklist(int id)
        {
            try
            {
                IDataResult<GetByIdBlacklistResponse> isBlacklisted = await _blacklistService.GetByIdAsync(id);
            } 
            catch (BusinessException ex) 
            {
                return;
            }
            
            throw new BusinessException("Application could not be created because this applicant is blacklisted.");
        }

       






    }
}
