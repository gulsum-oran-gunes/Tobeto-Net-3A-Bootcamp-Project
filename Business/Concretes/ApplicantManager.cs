using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Requests.Applicants;
using Business.Responses.Applicants;
using Business.Responses.Applications;
using Business.Responses.ApplicationStates;
using Core.DataAccess;
using Core.Exceptions.Types;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class ApplicantManager : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IMapper _mapper;
        public ApplicantManager(IApplicantRepository applicantRepository, IMapper mapper)
        {
            _applicantRepository = applicantRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<GetAllApplicantResponse>>> GetAllAsync()
        {
            List<Applicant> applicants = await _applicantRepository.GetAllAsync();
            List<GetAllApplicantResponse> responses = _mapper.Map<List<GetAllApplicantResponse>>(applicants);
            return new SuccessDataResult<List<GetAllApplicantResponse>>(responses, "Listeleme İşlemi Başarılı");
        }
        public async Task<IDataResult<GetByIdApplicantResponse>> GetByIdAsync(int id)
        {
            await CheckIfIdNotExists(id);
            Applicant applicant = await _applicantRepository.GetAsync(x => x.Id == id);
            GetByIdApplicantResponse response = _mapper.Map<GetByIdApplicantResponse>(applicant);
            return new SuccessDataResult<GetByIdApplicantResponse>(response, "GetById İşlemi Başarılı");
        }
       public async Task<IDataResult<CreateApplicantResponse>> AddAsync(CreateApplicantRequest request)
        {
            await CheckIfApplicantNotExists(request.UserName, request.NationalIdentity);
            Applicant applicant = _mapper.Map<Applicant>(request);
            await _applicantRepository.AddAsync(applicant);

            CreateApplicantResponse response = _mapper.Map<CreateApplicantResponse>(applicant);
            return new SuccessDataResult<CreateApplicantResponse>(response, "Ekleme İşlemi Başarılı");
        }

        public async Task<IResult>DeleteAsync(DeleteApplicantRequest request)
        {
            await CheckIfIdNotExists(request.Id);
            Applicant applicant = await _applicantRepository.GetAsync(x => x.Id == request.Id);
            await _applicantRepository.DeleteAsync(applicant);
            return new SuccessResult("Silme İşlemi Başarılı");
        }

        
        public async Task<IDataResult<UpdateApplicantResponse>> UpdateAsync(UpdateApplicantRequest request)
        {
            await CheckIfIdNotExists(request.Id);
            Applicant applicant = await _applicantRepository.GetAsync(x => x.Id == request.Id);
            applicant = _mapper.Map(request, applicant);
            await _applicantRepository.UpdateAsync(applicant);
            UpdateApplicantResponse response = _mapper.Map<UpdateApplicantResponse>(applicant);
            return new SuccessDataResult<UpdateApplicantResponse>(response, "Güncelleme İşlemi Başarılı");
        }

        private async Task CheckIfIdNotExists(int applicantId)
        {
            var isExists = await _applicantRepository.GetAsync(applicant => applicant.Id == applicantId);
            if (isExists is null) throw new BusinessException("Id not exists");

        }

        private async Task CheckIfApplicantNotExists(string userName, string nationalIdentity)
        {
            var isExists = await _applicantRepository.GetAsync(applicant => applicant.UserName == userName  || applicant.NationalIdentity == nationalIdentity);
            if (isExists is not null) throw new BusinessException("UserName or National Identity is already exists");

        }

    }
   
}
