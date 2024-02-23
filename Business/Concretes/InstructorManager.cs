using AutoMapper;
using Business.Abstracts;
using Business.Requests.Instructors;

using Business.Responses.Instructors;

using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.Repositories;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class InstructorManager : IInstructorService

    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMapper _mapper;
        public InstructorManager(IInstructorRepository instructorRepository, IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<GetAllInstructorResponse>>> GetAllAsync()
        {
            List<Instructor> instructors = await _instructorRepository.GetAllAsync();
            List<GetAllInstructorResponse> responses = _mapper.Map<List<GetAllInstructorResponse>>(instructors);
            return new SuccessDataResult<List<GetAllInstructorResponse>>(responses, "Listeleme İşlemi Başarılı");
        }
        public async Task<IDataResult<GetByIdInstructorResponse>> GetByIdAsync(int id)
        {
            Instructor instructor = await _instructorRepository.GetAsync(x => x.Id == id);
            GetByIdInstructorResponse response = _mapper.Map<GetByIdInstructorResponse>(instructor);
            return new SuccessDataResult<GetByIdInstructorResponse>(response, "GetById İşlemi Başarılı");
        }
        public async Task<IDataResult<CreateInstructorResponse>> AddAsync(CreateInstructorRequest request)
        {
            Instructor instructor = _mapper.Map<Instructor>(request);
            await _instructorRepository.AddAsync(instructor);

            CreateInstructorResponse response = _mapper.Map<CreateInstructorResponse>(instructor);
            return new SuccessDataResult<CreateInstructorResponse>(response, "Ekleme İşlemi Başarılı");
        }

        public async Task<IResult> DeleteAsync(DeleteInstructorRequest request)
        {
            Instructor instructor = await _instructorRepository.GetAsync(x => x.Id == request.Id);
            await _instructorRepository.DeleteAsync(instructor);
            return new SuccessResult("Silme İşlemi Başarılı");
        }
        public async Task<IDataResult<UpdateInstructorResponse>> UpdateAsync(UpdateInstructorRequest request)
        {
            Instructor instructor = await _instructorRepository.GetAsync(x => x.Id == request.Id);
            instructor = _mapper.Map(request, instructor);
            await _instructorRepository.UpdateAsync(instructor);
            UpdateInstructorResponse response = _mapper.Map<UpdateInstructorResponse>(instructor);
            return new SuccessDataResult<UpdateInstructorResponse>(response, "Güncelleme İşlemi Başarılı");
        }



    }


}

