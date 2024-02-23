using AutoMapper;
using Business.Abstracts;
using Business.Requests.Bootcamps;
using Business.Responses.Bootcamps;
using Business.Responses.Bootcamps;
using Core.Utilities.Results;
using DataAccess.Abstracts;
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
        public BootcampManager(IBootcampRepository bootcampRepository, IMapper mapper)
        {
            _bootcampRepository = bootcampRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<GetAllBootcampResponse>>> GetAllAsync()
        {
            List<Bootcamp> bootcamps = await _bootcampRepository.GetAllAsync(include: x => x.Include(x => x.BootcampState).Include(x => x.Instructor));
            List<GetAllBootcampResponse> responses = _mapper.Map<List<GetAllBootcampResponse>>(bootcamps);
            return new SuccessDataResult<List<GetAllBootcampResponse>>(responses, "Listeleme İşlemi Başarılı");
        }
        public async Task<IDataResult<GetByIdBootcampResponse>> GetByIdAsync(int id)
        {
            Bootcamp bootcamp = await _bootcampRepository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.BootcampState).Include(x => x.Instructor));
            GetByIdBootcampResponse response = _mapper.Map<GetByIdBootcampResponse>(bootcamp);
            return new SuccessDataResult<GetByIdBootcampResponse>(response, "GetById İşlemi Başarılı");
        }
        public async Task<IDataResult<CreateBootcampResponse>> AddAsync(CreateBootcampRequest request)
        {
            Bootcamp bootcamp = _mapper.Map<Bootcamp>(request);
            await _bootcampRepository.AddAsync(bootcamp);

            CreateBootcampResponse response = _mapper.Map<CreateBootcampResponse>(bootcamp);
            return new SuccessDataResult<CreateBootcampResponse>(response, "Ekleme İşlemi Başarılı");
        }

        public async Task<IResult> DeleteAsync(DeleteBootcampRequest request)
        {
            Bootcamp bootcamp = await _bootcampRepository.GetAsync(x => x.Id == request.Id);
            await _bootcampRepository.DeleteAsync(bootcamp);
            return new SuccessResult("Silme İşlemi Başarılı");
        }
        public async Task<IDataResult<UpdateBootcampResponse>> UpdateAsync(UpdateBootcampRequest request)
        {
            Bootcamp bootcamp = await _bootcampRepository.GetAsync(x => x.Id == request.Id);
            await _bootcampRepository.UpdateAsync(bootcamp);
            UpdateBootcampResponse response = _mapper.Map<UpdateBootcampResponse>(bootcamp);
            return new SuccessDataResult<UpdateBootcampResponse>(response, "Güncelleme İşlemi Başarılı");
        }


    }
}

