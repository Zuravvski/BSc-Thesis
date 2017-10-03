using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Users;
using Core.Repositories;

namespace Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(Guid guid)
        {
            await _userRepository.CreateAsync(new User(guid));
        }

        public async Task DeleteUser(Guid guid)
        {
            await _userRepository.DeleteAsync(guid);
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<IEnumerable<User>> BrowseAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
