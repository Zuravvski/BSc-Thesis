using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Users;

namespace Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task CreateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
