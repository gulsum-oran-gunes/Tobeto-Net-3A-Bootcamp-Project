
using Business.Responses.Users;
using Core.Utilities.Results;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IUserService
    {
        Task<IDataResult<List<GetAllUserResponse>>> GetAllAsync();
        Task<IDataResult<GetByIdUserResponse>>GetByIdAsync(int id);
       
    }
}
