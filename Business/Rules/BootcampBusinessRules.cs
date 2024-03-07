using Business.Abstracts;
using Business.Constants;
using Business.Responses.Applicants;
using Business.Responses.Instructors;
using Core.CrossCuttingConcerns.Rules;
using Core.Exceptions.Types;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules
{
    public class BootcampBusinessRules :BaseBusinessRules
    {
        private readonly IBootcampRepository _repository;
        private IBootcampStateService _bootcampStateService;
        private IInstructorService _instructorService;

        public BootcampBusinessRules(IBootcampRepository repository, IBootcampStateService bootcampStateService, IInstructorService instructorService)
        {
            _repository = repository;
            _bootcampStateService = bootcampStateService;
            _instructorService = instructorService;

        }
        public async Task CheckIfIdNotExists(int bootcampId)
        {
            var isExists = await _repository.GetAsync(bootcamp => bootcamp.Id == bootcampId);
            if (isExists is null) throw new BusinessException(BootcampMessages.BootcampIdNotExists);

        }
        public async Task CheckIfInstructorIdNotExists(int instructorId)
        {
            var isExist = await _instructorService.GetByIdAsync(instructorId);
            if (isExist is null) throw new BusinessException(InstructorMessages.InstructorIdNotExists);

        }
        public async Task CheckIfBootcampStateIdNotExists(int bootcampStateId)
        {
            var isExist = await _bootcampStateService.GetByIdAsync(bootcampStateId);
            if (isExist is null) throw new BusinessException(BootcampStateMessages.BootcampStateIdNotExists);

        }

    }
}
