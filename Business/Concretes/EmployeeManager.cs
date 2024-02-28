using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Requests.Employees;
using Business.Requests.Employees;
using Business.Responses.Employees;
using Business.Responses.Employees;
using Core.Exceptions.Types;
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
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeManager(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<GetAllEmployeeResponse>>> GetAllAsync()
        {
            List<Employee> employees = await _employeeRepository.GetAllAsync();
            List<GetAllEmployeeResponse> responses = _mapper.Map<List<GetAllEmployeeResponse>>(employees);
            return new SuccessDataResult<List<GetAllEmployeeResponse>>(responses, "Listeleme İşlemi Başarılı");
        }
        public async Task<IDataResult<GetByIdEmployeeResponse>> GetByIdAsync(int id)
        {
            await CheckIfIdNotExists(id);
            Employee employee = await _employeeRepository.GetAsync(x => x.Id == id);
            GetByIdEmployeeResponse response = _mapper.Map<GetByIdEmployeeResponse>(employee);
            return new SuccessDataResult<GetByIdEmployeeResponse>(response, "GetById İşlemi Başarılı");
        }
        public async Task<IDataResult<CreateEmployeeResponse>> AddAsync(CreateEmployeeRequest request)
        {
            await CheckIfEmployeeNotExists(request.UserName, request.NationalIdentity);
            Employee employee = _mapper.Map<Employee>(request);
            await _employeeRepository.AddAsync(employee);

            CreateEmployeeResponse response = _mapper.Map<CreateEmployeeResponse>(employee);
            return new SuccessDataResult<CreateEmployeeResponse>(response, "Ekleme İşlemi Başarılı");
        }

        public async Task<IResult> DeleteAsync(DeleteEmployeeRequest request)
        {
            await CheckIfIdNotExists(request.Id);
            Employee employee = await _employeeRepository.GetAsync(x => x.Id == request.Id);
            await _employeeRepository.DeleteAsync(employee);
            return new SuccessResult("Silme İşlemi Başarılı");
        }
        public async Task<IDataResult<UpdateEmployeeResponse>> UpdateAsync(UpdateEmployeeRequest request)
        {
            await CheckIfIdNotExists(request.Id);
            Employee employee = await _employeeRepository.GetAsync(x => x.Id == request.Id);
            employee = _mapper.Map(request, employee);
            await _employeeRepository.UpdateAsync(employee);
            UpdateEmployeeResponse response = _mapper.Map<UpdateEmployeeResponse>(employee);
            return new SuccessDataResult<UpdateEmployeeResponse>(response, "Güncelleme İşlemi Başarılı");
        }
        private async Task CheckIfIdNotExists(int employeeId)
        {
            var isExists = await _employeeRepository.GetAsync(x => x.Id == employeeId);
            if (isExists is null) throw new BusinessException("Id not exists");

        }

        private async Task CheckIfEmployeeNotExists(string userName, string nationalIdentity)
        {
            var isExists = await _employeeRepository.GetAsync(x => x.UserName == userName || x.NationalIdentity == nationalIdentity);
            if (isExists is not null) throw new BusinessException("UserName or National Identity is already exists");

        }
    }
}
