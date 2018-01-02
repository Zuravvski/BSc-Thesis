using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Network.Sockets;

namespace Infrastructure.Repositories
{
    public class InMemoryClientRepository : IClientRepository
    {
        private readonly ISet<Client> _clients = new HashSet<Client>();

        public async Task<Client> GetClientAsync(string ip)
        {
            return await Task.FromResult(_clients.FirstOrDefault(client => client.IPAddress.Equals(ip)));
        }

        public async Task<IEnumerable<Client>> BrowseClientsAsync()
        {
            return await Task.FromResult(_clients);
        }

        public async Task CreateClientAsync(Client client)
        {
            _clients.Add(client);
            await Task.CompletedTask;
        }

        public async Task DeleteClientAsync(string ip)
        {
            var client = await GetClientAsync(ip);
            _clients.Remove(client);
            await Task.CompletedTask;
        }
    }
}
