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
    public class EmployeeBusinessRules :BaseBusinessRules
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeBusinessRules(IEmployeeRepository repository)
        {
            _repository = repository;
            
        }
        public async Task CheckIfIdNotExists(int employeeId)
        {
            var isExists = await _repository.GetAsync(x => x.Id == employeeId);
            if (isExists is null) throw new BusinessException("Id not exists");

        }

        public async Task CheckIfEmployeeNotExists(string userName, string nationalIdentity)
        {
            var isExists = await _repository.GetAsync(x => x.UserName == userName || x.NationalIdentity == nationalIdentity);
            if (isExists is not null) throw new BusinessException("UserName or National Identity is already exists");

        }

    }
}
