using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Constants;
using Business.Requests.Bootcamps;
using Business.Responses.Bootcamps;
using Business.Responses.Bootcamps;
using Business.Rules;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.DataAccess;
using Core.Exceptions.Types;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.Repositories;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class BootcampManager:IBootcampService
    {
        private readonly IBootcampRepository _bootcampRepository;
        private readonly IMapper _mapper;
        private readonly BootcampBusinessRules _rules;
        public BootcampManager(IBootcampRepository bootcampRepository, IMapper mapper, BootcampBusinessRules rules)
        {
            _bootcampRepository = bootcampRepository;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<IDataResult<List<GetAllBootcampResponse>>> GetAllAsync()
        {
            List<Bootcamp> bootcamps = await _bootcampRepository.GetAllAsync(include: x => x.Include(x => x.BootcampState).Include(x => x.Instructor));
            List<GetAllBootcampResponse> responses = _mapper.Map<List<GetAllBootcampResponse>>(bootcamps);
            return new SuccessDataResult<List<GetAllBootcampResponse>>(responses, BootcampMessages.BootcampGetAll);
        }
        public async Task<IDataResult<GetByIdBootcampResponse>> GetByIdAsync(int id)
        {
            await _rules.CheckIfIdNotExists(id);
            Bootcamp bootcamp = await _bootcampRepository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.BootcampState).Include(x => x.Instructor));
            GetByIdBootcampResponse response = _mapper.Map<GetByIdBootcampResponse>(bootcamp);
            return new SuccessDataResult<GetByIdBootcampResponse>(response, BootcampMessages.BootcampGetById);
        }

        [LogAspect(typeof(MongoDbLogger))]
        public async Task<IDataResult<CreateBootcampResponse>> AddAsync(CreateBootcampRequest request)
        {
            await _rules.CheckIfInstructorIdNotExists(request.InstructorId);
            await _rules.CheckIfBootcampStateIdNotExists(request.BootcampStateId);
            Bootcamp bootcamp = _mapper.Map<Bootcamp>(request);
            await _bootcampRepository.AddAsync(bootcamp);
            CreateBootcampResponse response = _mapper.Map<CreateBootcampResponse>(bootcamp);
            return new SuccessDataResult<CreateBootcampResponse>(response, BootcampMessages.BootcampAdded);
        }

        public async Task<IResult> DeleteAsync(DeleteBootcampRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            Bootcamp bootcamp = await _bootcampRepository.GetAsync(x => x.Id == request.Id);
            await _bootcampRepository.DeleteAsync(bootcamp);
            return new SuccessResult(BootcampMessages.BootcampDeleted);
        }
        public async Task<IDataResult<UpdateBootcampResponse>> UpdateAsync(UpdateBootcampRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            Bootcamp bootcamp = await _bootcampRepository.GetAsync(x => x.Id == request.Id);
            bootcamp = _mapper.Map(request, bootcamp);
            await _bootcampRepository.UpdateAsync(bootcamp);
            UpdateBootcampResponse response = _mapper.Map<UpdateBootcampResponse>(bootcamp);
            return new SuccessDataResult<UpdateBootcampResponse>(response, BootcampMessages.BootcampUpdated);
        }
        



    }
}

