using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Constants;
using Business.Responses.Users;
using Business.Responses.Users;
using Business.Rules;
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
        private readonly UserBusinessRules _rules;
        public UserManager(IUserRepository userRepository, IMapper mapper, UserBusinessRules rules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<IDataResult<List<GetAllUserResponse>>> GetAllAsync()
        {
            List<User> users = await _userRepository.GetAllAsync();
            List<GetAllUserResponse> responses = _mapper.Map<List<GetAllUserResponse>>(users);
            return new SuccessDataResult<List<GetAllUserResponse>>(responses, UserMessages.UserGetAll);
        }
        public async Task<IDataResult<GetByIdUserResponse>> GetByIdAsync(int id)
        {
            await _rules.CheckIfIdNotExists(id);
            User user = await _userRepository.GetAsync(x => x.Id == id);
            GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);
            return new SuccessDataResult<GetByIdUserResponse>(response, UserMessages.UserGetById);
        }
       

    }
}
