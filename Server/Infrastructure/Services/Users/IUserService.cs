using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Users;

namespace Infrastructure.Services.Users
{
    public interface IUserService : IService
    {
        Task CreateUser(Guid guid);
        Task DeleteUser(Guid guid);
        Task<User> GetUser(Guid id);
        Task<IEnumerable<User>> BrowseAsync();
    }
}
