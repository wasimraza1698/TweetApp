using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Common;
using TweetApp.DBSettings;
using TweetApp.Models;
using TweetApp.Models.DataModels;
using TweetApp.Models.Requests;
using TweetApp.Models.Responses;
using TweetApp.Repositories.Interfaces;
using TweetApp.Services.Interfaces;

namespace TweetApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IAuthService authService, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<string> LoginUser(Credentials credentials)
        {
            _logger.LogInformation(Constants.SearchingUserByUserName, credentials.UserName);

            var user = await _userRepository.FindUserByUserName(credentials.UserName);

            if (user == null)
            {
                _logger.LogInformation(Constants.UserNotFoundByUserName, credentials.UserName);
                _logger.LogInformation(Constants.SearchingUserByEmailId, credentials.UserName);

                user = await _userRepository.FindUserByEmail(credentials.UserName);
            }

            if (user != null)
            {
                _logger.LogInformation(Constants.UserFound, credentials.UserName);

                if (user.Password.Equals(credentials.Password))
                {
                    _logger.LogInformation(Constants.GeneratingJWT, credentials.UserName);

                    return _authService.GenerateJWT(user.UserName);
                }

                _logger.LogInformation(Constants.IncorrectPassword, credentials.UserName);
                return null;
            }

            _logger.LogInformation(Constants.UserNotFoundByEmailId, credentials.UserName);
            return null;
        }

        public async Task<string> RegisterUser(UserRequest user)
        {
            var isNotAnExistingUser = (await _userRepository.FindUserByEmail(user.EmailId) == null) && (await _userRepository.FindUserByUserName(user.UserName) == null);

            if (isNotAnExistingUser)
            {
                _logger.LogInformation(Constants.NotAnExistingUser, user.UserName);

                // Map User from request to User Data Model
                var newUser = _mapper.Map<UserRequest, User>(user);

                newUser.CreatedBy = newUser.UserName;
                await _userRepository.AddUser(newUser);

                _logger.LogInformation(Constants.UserCreated, newUser.UserName);
                return newUser.UserId.ToString();
            }

            _logger.LogInformation(Constants.UserExists, user.UserName, user.EmailId);
            return null;
        }

        public async Task<List<UserResponse>> SearchUserByUserName(string userName)
        {
            _logger.LogInformation(Constants.SearchingUserByUserName, userName);

            var users = await _userRepository.LookUpUsers(userName);

            var response = _mapper.Map<List<User>, List<UserResponse>>(users);

            return response;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            _logger.LogInformation(Constants.GetAllUsers);

            var users = await _userRepository.GetAllUsers();

            var response = _mapper.Map<List<User>, List<UserResponse>>(users);

            return response;
        }
    }
}
