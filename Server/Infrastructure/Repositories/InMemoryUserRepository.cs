using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Users;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ISet<User> _users = new HashSet<User>(); 

        public async Task<User> GetAsync(Guid id)
        {
            return await Task.FromResult(_users.FirstOrDefault(user => user.ID == id));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(_users);
        }

        public Task CreateAsync(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            _users.Remove(await GetAsync(id));
        }
    }
}
