using AutoMapper;
using Business.Abstracts;
using Business.Requests.Applications;
using Business.Responses.Applications;
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
    public class ApplicationManager : IApplicationService
    {
    private readonly IApplicationRepository _applicationRepository;
    private readonly IMapper _mapper;
    public ApplicationManager(IApplicationRepository applicationRepository, IMapper mapper)
    {
        _applicationRepository = applicationRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<GetAllApplicationResponse>>> GetAllAsync()
    {
        List<Application> applications = await _applicationRepository.GetAllAsync(include: x => x.Include(x => x.Bootcamp).Include(x => x.ApplicationState).Include(x => x.Applicant));
            List<GetAllApplicationResponse> responses = _mapper.Map<List<GetAllApplicationResponse>>(applications);
        return new SuccessDataResult<List<GetAllApplicationResponse>>(responses, "Listeleme İşlemi Başarılı");
    }
    public async Task<IDataResult<GetByIdApplicationResponse>> GetByIdAsync(int id)
    {
        Application application = await _applicationRepository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.Bootcamp).Include(x => x.ApplicationState).Include(x => x.Applicant));
        GetByIdApplicationResponse response = _mapper.Map<GetByIdApplicationResponse>(application);
        return new SuccessDataResult<GetByIdApplicationResponse>(response, "GetById İşlemi Başarılı");
    }
    public async Task<IDataResult<CreateApplicationResponse>> AddAsync(CreateApplicationRequest request)
    {
        Application application = _mapper.Map<Application>(request);
        await _applicationRepository.AddAsync(application);

        CreateApplicationResponse response = _mapper.Map<CreateApplicationResponse>(application);
        return new SuccessDataResult<CreateApplicationResponse>(response, "Ekleme İşlemi Başarılı");
    }

    public async Task<IResult<DeleteApplicationResponse>> DeleteAsync(DeleteApplicationRequest request)
    {
        Application application = await _applicationRepository.GetAsync(x => x.Id == request.Id);
        await _applicationRepository.DeleteAsync(application);
        return new SuccessResult<DeleteApplicationResponse>("Silme İşlemi Başarılı");
    }
    public async Task<IDataResult<UpdateApplicationResponse>> UpdateAsync(UpdateApplicationRequest request)
    {
        Application application = await _applicationRepository.GetAsync(x => x.Id == request.Id);
        application = _mapper.Map(request, application);
        await _applicationRepository.UpdateAsync(application);
        UpdateApplicationResponse response = _mapper.Map<UpdateApplicationResponse>(application);
        return new SuccessDataResult<UpdateApplicationResponse>(response, "Güncelleme İşlemi Başarılı");
    }

       
    }
}
