using Business.Abstracts;
using Business.Requests.Instructors;
using Business.Responses.Instructors;
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

        public InstructorManager(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }
        public async Task<List<GetAllInstructorResponse>> GetAll()
        {
            List<GetAllInstructorResponse> instructors = new List<GetAllInstructorResponse>();
            foreach (var instructor in await _instructorRepository.GetAll())
            {
                GetAllInstructorResponse response = new();
                response.Id = instructor.Id;
                response.CompanyName = instructor.CompanyName;
                instructors.Add(response);
            }
            return instructors;
        }

        public async Task<GetByIdInstructorResponse> GetById(int id)
        {
            GetByIdInstructorResponse response = new();
            Instructor instructor = await _instructorRepository.Get(x => x.Id == id);
            response.Id = instructor.Id;
            response.CompanyName = instructor.CompanyName;
            return response;
        }

        public async Task<CreateInstructorResponse> AddAsync(CreateInstructorRequest request)
        {
            Instructor instructor = new();
            instructor.UserName = request.UserName;
            instructor.Password = request.Password;
            instructor.Email = request.Email;
            instructor.DateOfBirth = request.DateOfBirth;
            instructor.FirstName = request.FirstName;
            instructor.LastName = request.LastName;
            request.CompanyName = instructor.CompanyName;
            await _instructorRepository.Add(instructor);

            CreateInstructorResponse response = new();
            response.Id = instructor.Id;
            response.CompanyName = instructor.CompanyName;
            response.CreatedDate = instructor.CreatedDate;
            return response;
        }

        public async Task<DeleteInstructorResponse> DeleteAsync(DeleteInstructorRequest request)
        {
            Instructor instructor = await _instructorRepository.Get(x => x.Id == request.Id);
            instructor.Id = request.Id;
            await _instructorRepository.Delete(instructor);

            DeleteInstructorResponse response = new();
            response.Id = instructor.Id;
            response.DeletedDate = instructor.DeletedDate;
            return response;
        }

        public async Task<UpdateInstructorResponse> UpdateAsync(UpdateInstructorRequest request)
        {
            Instructor instructor = await _instructorRepository.Get(x => x.Id == request.Id);
            instructor.Id = request.Id;
            instructor.CompanyName = request.CompanyName;
            await _instructorRepository.Update(instructor);

            UpdateInstructorResponse response = new();
            response.Id = instructor.Id; 
            response.CompanyName = instructor.CompanyName;
            response.UpdatedDate = instructor.UpdatedDate;
            return response;
        }
    }
}
