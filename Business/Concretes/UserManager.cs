using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Responses.Users;
using Business.Responses.Users;
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
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<GetAllUserResponse>>> GetAllAsync()
        {
            List<User> users = await _userRepository.GetAllAsync();
            List<GetAllUserResponse> responses = _mapper.Map<List<GetAllUserResponse>>(users);
            return new SuccessDataResult<List<GetAllUserResponse>>(responses, "Listeleme İşlemi Başarılı");
        }
        public async Task<IDataResult<GetByIdUserResponse>> GetByIdAsync(int id)
        {
            await CheckIfIdNotExists(id);
            User user = await _userRepository.GetAsync(x => x.Id == id);
            GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);
            return new SuccessDataResult<GetByIdUserResponse>(response, "GetById İşlemi Başarılı");
        }
        private async Task CheckIfIdNotExists(int userId)
        {
            var isExists = await _userRepository.GetAsync(x => x.Id == userId);
            if (isExists is null) throw new BusinessException("Id not exists");

        }

    }
}
