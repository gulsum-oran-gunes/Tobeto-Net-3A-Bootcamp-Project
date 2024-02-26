using Business.Requests.Blacklists;
using Business.Responses.Blacklists;
using Business.Responses.Blacklists;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBlacklistService
    {
        Task<IDataResult<List<GetAllBlacklistResponse>>> GetAllAsync();
        Task<IDataResult<GetByIdBlacklistResponse>> GetByIdAsync(int id);
        Task<IDataResult<CreateBlacklistResponse>> AddAsync(CreateBlacklistRequest request);
        Task<IResult> DeleteAsync(DeleteBlacklistRequest request);
        Task<IDataResult<UpdateBlacklistResponse>> UpdateAsync(UpdateBlacklistRequest request);
    }
}
