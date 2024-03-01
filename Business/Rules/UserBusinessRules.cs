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
    public class UserBusinessRules :BaseBusinessRules
    {
        private readonly IUserRepository _repository;
        public UserBusinessRules(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task CheckIfIdNotExists(int userId)
        {
            var isExists = await _repository.GetAsync(x => x.Id == userId);
            if (isExists is null) throw new BusinessException("Id not exists");

        }


    }
}
