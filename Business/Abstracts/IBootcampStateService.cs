using Business.Requests.BootcampStates;

using Business.Responses.BootcampStates;

using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBootcampStateService
    {
        Task<IDataResult<List<GetAllBootcampStateResponse>>> GetAllAsync();
        Task<IDataResult<GetByIdBootcampStateResponse>> GetByIdAsync(int id);
        Task<IDataResult<CreateBootcampStateResponse>>AddAsync(CreateBootcampStateRequest request);
        Task<IResult> DeleteAsync(DeleteBootcampStateRequest request);
        Task<IDataResult<UpdateBootcampStateResponse>>UpdateAsync(UpdateBootcampStateRequest request);
    }
}

