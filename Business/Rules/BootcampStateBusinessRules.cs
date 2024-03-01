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
    public class BootcampStateBusinessRules :BaseBusinessRules
    {
        private readonly IBootcampStateRepository _repository;

        public BootcampStateBusinessRules(IBootcampStateRepository repository)
        {

            _repository = repository;
        }
        public async Task CheckIfIdNotExists(int bootcampStateId)
        {
            var isExists = await _repository.GetAsync(bootcampState => bootcampState.Id == bootcampStateId);
            if (isExists is null) throw new BusinessException("Id not exists");


        }






    }
}
