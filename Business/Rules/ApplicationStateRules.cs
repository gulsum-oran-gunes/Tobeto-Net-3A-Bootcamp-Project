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
    public class ApplicationStateRules : BaseBusinessRules
    {
        private IApplicationStateRepository _repository;

        public ApplicationStateRules(IApplicationStateRepository repository)
        {
            _repository = repository;
        }

        public async Task CheckIfIdNotExists(int id)
        {
            var isExists = await _repository.GetAsync(x => x.Id == id);
            if (isExists is null) throw new BusinessException("Id not exists");

        }





    }
}
