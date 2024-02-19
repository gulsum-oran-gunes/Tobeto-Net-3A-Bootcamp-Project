using Business.Abstracts;
using Business.Requests.Employees;
using Business.Responses.Employees;
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

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<GetAllEmployeeResponse>> GetAll()
        {
            List<GetAllEmployeeResponse> employees = new List<GetAllEmployeeResponse>();
            foreach (var employee in await _employeeRepository.GetAll())
            {
                GetAllEmployeeResponse response = new();
                response.Id = employee.Id;
                response.Position = employee.Position;
                employees.Add(response);
            }
            return employees;
        }

        public async Task<GetByIdEmployeeResponse> GetById(int id)
        {
            GetByIdEmployeeResponse response = new();
            Employee employee = await _employeeRepository.Get(x => x.Id == id);
            response.Id = employee.Id;
            response.Position = employee.Position;
            return response;
        }

        public async Task<CreateEmployeeResponse> AddAsync(CreateEmployeeRequest request)
        {
            Employee employee = new();
            employee.UserName = request.UserName;
            employee.Password = request.Password;
            employee.Email = request.Email;
            employee.DateOfBirth = request.DateOfBirth;
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Position = request.Position;
            await _employeeRepository.Add(employee);

            CreateEmployeeResponse response = new();
            response.Id = employee.Id;
            response.Position = employee.Position;
            response.CreatedDate = employee.CreatedDate;
            
            return response;
        }

        public async Task<DeleteEmployeeResponse> DeleteAsync(DeleteEmployeeRequest request)
        {
            Employee employee = await _employeeRepository.Get(x => x.Id == request.Id);
            employee.Id = request.Id;
            await _employeeRepository.Delete(employee);

            DeleteEmployeeResponse response = new DeleteEmployeeResponse();
            response.Id = employee.Id;
            response.DeletedDate = employee.DeletedDate;
            return response;
        }

        public async Task<UpdateEmployeeResponse> UpdateAsync(UpdateEmployeeRequest request)
        {
            Employee employee = await _employeeRepository.Get(x => x.Id == request.Id);
            employee.Id = request.Id;
            employee.Position = request.Position;
            await _employeeRepository.Update(employee);

            UpdateEmployeeResponse response = new();
            response.Id = employee.Id;
            response.Position = employee.Position;
            response.UpdatedDate = employee.UpdatedDate;
            return response;
        }
    }

}
