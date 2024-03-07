using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Constants;
using Business.Requests.Blacklists;
using Business.Responses.Applicants;
using Business.Responses.Blacklists;
using Business.Responses.Blacklists;
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
    public class BlacklistManager : IBlacklistService
    {
        private readonly IBlacklistRepository _blacklistRepository;
        private readonly IMapper _mapper;
        private readonly BlacklistBusinessRules _rules;
        public BlacklistManager(IBlacklistRepository blacklistRepository, IMapper mapper, BlacklistBusinessRules rules)
        {
            _blacklistRepository = blacklistRepository;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<IDataResult<List<GetAllBlacklistResponse>>> GetAllAsync()
        {
            List<Blacklist> blacklists = await _blacklistRepository.GetAllAsync(include: x => x.Include(x => x.Applicant));
            List<GetAllBlacklistResponse> responses = _mapper.Map<List<GetAllBlacklistResponse>>(blacklists);
            return new SuccessDataResult<List<GetAllBlacklistResponse>>(responses, BlacklistMessages.BlacklistGetAll);
        }
        public async Task<IDataResult<GetByIdBlacklistResponse>> GetByIdAsync(int id)
        {
            await _rules.CheckIfIdNotExists(id);
            Blacklist blacklist = await _blacklistRepository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.Applicant));
            GetByIdBlacklistResponse response = _mapper.Map<GetByIdBlacklistResponse>(blacklist);
            return new SuccessDataResult<GetByIdBlacklistResponse>(response, BlacklistMessages.BlacklistGetById);
        }

        [LogAspect(typeof(MongoDbLogger))]
        public async Task<IDataResult<CreateBlacklistResponse>> AddAsync(CreateBlacklistRequest request)
        {
            await _rules.CheckIfApplicantIdNotExists(request.ApplicantId);
            await _rules.CheckIfApplicantAlreadyBlacklisted(request.ApplicantId);
            Blacklist blacklist = _mapper.Map<Blacklist>(request);
            await _blacklistRepository.AddAsync(blacklist);

            CreateBlacklistResponse response = _mapper.Map<CreateBlacklistResponse>(blacklist);
            return new SuccessDataResult<CreateBlacklistResponse>(response, BlacklistMessages.BlacklistAdded);
        }

        public async Task<IResult> DeleteAsync(DeleteBlacklistRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            Blacklist blacklist = await _blacklistRepository.GetAsync(x => x.Id == request.Id);
            await _blacklistRepository.DeleteAsync(blacklist);
            return new SuccessResult(BlacklistMessages.BlacklistDeleted);
        }


        public async Task<IDataResult<UpdateBlacklistResponse>> UpdateAsync(UpdateBlacklistRequest request)
        {
            await _rules.CheckIfIdNotExists(request.Id);
            Blacklist blacklist = await _blacklistRepository.GetAsync(x => x.Id == request.Id, include: x => x.Include(x => x.Applicant));
            blacklist = _mapper.Map(request, blacklist);
            await _blacklistRepository.UpdateAsync(blacklist);
            UpdateBlacklistResponse response = _mapper.Map<UpdateBlacklistResponse>(blacklist);
            return new SuccessDataResult<UpdateBlacklistResponse>(response, BlacklistMessages.BlacklistUpdated);
        }

        public async Task<IDataResult<GetByApplicantIdResponse>> GetByApplicantIdAsync(int applicantId)
        {
            Blacklist blacklist = await _blacklistRepository.GetAsync(x => x.ApplicantId == applicantId);
            GetByApplicantIdResponse response = _mapper.Map<GetByApplicantIdResponse>(blacklist);
            return new SuccessDataResult<GetByApplicantIdResponse>(response, BlacklistMessages.BlacklistGetByApplicantId);
        }

        
    }



    
}

