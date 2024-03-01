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
    public class BlacklistBusinessRules :BaseBusinessRules
    {
        private readonly IBlacklistRepository _repository;

        public BlacklistBusinessRules(IBlacklistRepository repository)
        {

            _repository = repository;
        }
        public async Task CheckIfIdNotExists(int blacklistId)
        {
            var isExists = await _repository.GetAsync(blacklist => blacklist.Id == blacklistId);
            if (isExists is null) throw new BusinessException("Id not exists");

        }



    }
}
