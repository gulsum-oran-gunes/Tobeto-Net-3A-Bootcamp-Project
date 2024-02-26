using AutoMapper;
using Business.Abstracts;
using Business.Requests.Blacklists;
using Business.Responses.Applicants;
using Business.Responses.Blacklists;
using Business.Responses.Blacklists;
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
        public BlacklistManager(IBlacklistRepository blacklistRepository, IMapper mapper)
        {
            _blacklistRepository = blacklistRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<GetAllBlacklistResponse>>> GetAllAsync()
        {
            List<Blacklist> blacklists = await _blacklistRepository.GetAllAsync(include: x => x.Include(x => x.Applicant));
            List<GetAllBlacklistResponse> responses = _mapper.Map<List<GetAllBlacklistResponse>>(blacklists);
            return new SuccessDataResult<List<GetAllBlacklistResponse>>(responses, "Listeleme İşlemi Başarılı");
        }
        public async Task<IDataResult<GetByIdBlacklistResponse>> GetByIdAsync(int id)
        {
            Blacklist blacklist = await _blacklistRepository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.Applicant));
            GetByIdBlacklistResponse response = _mapper.Map<GetByIdBlacklistResponse>(blacklist);
            return new SuccessDataResult<GetByIdBlacklistResponse>(response, "GetById İşlemi Başarılı");
        }
        public async Task<IDataResult<CreateBlacklistResponse>> AddAsync(CreateBlacklistRequest request)
        {
            Blacklist blacklist = _mapper.Map<Blacklist>(request);
            await _blacklistRepository.AddAsync(blacklist);

            CreateBlacklistResponse response = _mapper.Map<CreateBlacklistResponse>(blacklist);
            return new SuccessDataResult<CreateBlacklistResponse>(response, "Ekleme İşlemi Başarılı");
        }

        public async Task<IResult> DeleteAsync(DeleteBlacklistRequest request)
        {
            Blacklist blacklist = await _blacklistRepository.GetAsync(x => x.Id == request.Id);
            await _blacklistRepository.DeleteAsync(blacklist);
            return new SuccessResult("Silme İşlemi Başarılı");
        }


        public async Task<IDataResult<UpdateBlacklistResponse>> UpdateAsync(UpdateBlacklistRequest request)
        {
            Blacklist blacklist = await _blacklistRepository.GetAsync(x => x.Id == request.Id, include: x => x.Include(x => x.Applicant));
            blacklist = _mapper.Map(request, blacklist);
            await _blacklistRepository.UpdateAsync(blacklist);
            UpdateBlacklistResponse response = _mapper.Map<UpdateBlacklistResponse>(blacklist);
            return new SuccessDataResult<UpdateBlacklistResponse>(response, "Güncelleme İşlemi Başarılı");
        }
    }



    
}

