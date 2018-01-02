using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Repositories;
using Infrastructure.Network.Sockets;

namespace Infrastructure.Repositories
{
    public interface IClientRepository : IRepository
    {
        Task<Client> GetClientAsync(string ip);
        Task<IEnumerable<Client>> BrowseClientsAsync();
        Task CreateClientAsync(Client client);
        Task DeleteClientAsync(string ip);
    }
}
